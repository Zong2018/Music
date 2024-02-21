using System;
using System.ComponentModel;
using System.Reflection;

namespace Music.Infrastructure.Commands
{

	/// <summary>
	/// Represents each node of nested properties expression and takes care of 
	/// subscribing/unsubscribing INotifyPropertyChanged.PropertyChanged listeners on it.
	/// </summary>
	internal class PropertyObserverNode
	{
		private readonly Action _action;

		private INotifyPropertyChanged _inpcObject;

		public string PropertyName { get; }

		public PropertyObserverNode Next { get; set; }

		public PropertyObserverNode(string propertyName, Action action)
		{
			PropertyObserverNode propertyObserverNode = this;
			PropertyName = propertyName;
			_action = delegate
			{
				action?.Invoke();
				if (propertyObserverNode.Next != null)
				{
					propertyObserverNode.Next.UnsubscribeListener();
					propertyObserverNode.GenerateNextNode();
				}
			};
		}

		public void SubscribeListenerFor(INotifyPropertyChanged inpcObject)
		{
			_inpcObject = inpcObject;
			_inpcObject.PropertyChanged += OnPropertyChanged;
			if (Next != null)
			{
				GenerateNextNode();
			}
		}

		private void GenerateNextNode()
		{
			object nextProperty = _inpcObject.GetType().GetRuntimeProperty(PropertyName).GetValue(_inpcObject);
			if (nextProperty != null)
			{
				if (!(nextProperty is INotifyPropertyChanged nextInpcObject))
				{
					throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that owns '" + Next.PropertyName + "' property, but the object does not implements INotifyPropertyChanged.");
				}
				Next.SubscribeListenerFor(nextInpcObject);
			}
		}

		private void UnsubscribeListener()
		{
			if (_inpcObject != null)
			{
				_inpcObject.PropertyChanged -= OnPropertyChanged;
			}
			Next?.UnsubscribeListener();
		}

		private void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e?.PropertyName == PropertyName || e == null || e.PropertyName == null)
			{
				_action?.Invoke();
			}
		}
	}
}
