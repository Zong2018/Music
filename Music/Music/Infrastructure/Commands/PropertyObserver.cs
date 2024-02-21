using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;

namespace Music.Infrastructure.Commands
{
	/// <summary>
	/// Provide a way to observe property changes of INotifyPropertyChanged objects and invokes a 
	/// custom action when the PropertyChanged event is fired.
	/// </summary>
	internal class PropertyObserver
	{
		private readonly Action _action;

		private PropertyObserver(System.Linq.Expressions.Expression propertyExpression, Action action)
		{
			_action = action;
			SubscribeListeners(propertyExpression);
		}

		private void SubscribeListeners(System.Linq.Expressions.Expression propertyExpression)
		{
			Stack<string> propNameStack = new Stack<string>();
			while (propertyExpression is MemberExpression temp)
			{
				propertyExpression = temp.Expression;
				propNameStack.Push(temp.Member.Name);
			}
			if (!(propertyExpression is ConstantExpression constantExpression))
			{
				throw new NotSupportedException("Operation not supported for the given expression type. Only MemberExpression and ConstantExpression are currently supported.");
			}
			PropertyObserverNode propObserverNodeRoot = new PropertyObserverNode(propNameStack.Pop(), _action);
			PropertyObserverNode previousNode = propObserverNodeRoot;
			using (Stack<string>.Enumerator enumerator = propNameStack.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					PropertyObserverNode currentNode = (previousNode.Next = new PropertyObserverNode(enumerator.Current, _action));
					previousNode = currentNode;
				}
			}
			if (!(constantExpression.Value is INotifyPropertyChanged inpcObject))
			{
				throw new InvalidOperationException("Trying to subscribe PropertyChanged listener in object that owns '" + propObserverNodeRoot.PropertyName + "' property, but the object does not implements INotifyPropertyChanged.");
			}
			propObserverNodeRoot.SubscribeListenerFor(inpcObject);
		}

		/// <summary>
		/// Observes a property that implements INotifyPropertyChanged, and automatically calls a custom action on 
		/// property changed notifications. The given expression must be in this form: "() =&gt; Prop.NestedProp.PropToObserve".
		/// </summary>
		/// <param name="propertyExpression">Expression representing property to be observed. Ex.: "() =&gt; Prop.NestedProp.PropToObserve".</param>
		/// <param name="action">Action to be invoked when PropertyChanged event occours.</param>
		internal static PropertyObserver Observes<T>(Expression<Func<T>> propertyExpression, Action action)
		{
			return new PropertyObserver(propertyExpression.Body, action);
		}
	}

}
