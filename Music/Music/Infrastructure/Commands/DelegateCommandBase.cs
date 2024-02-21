using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Music.Infrastructure.Commands
{
	/// <summary>
	///命令抽象类
	/// </summary>
	public abstract class DelegateCommandBase : ICommand
	{
		private bool _isActive;
		/// <summary>
		/// 同步上下文
		/// </summary>
		private SynchronizationContext _synchronizationContext;

		private readonly HashSet<string> _observedPropertiesExpressions = new HashSet<string>();

		/// <summary>
		/// Gets or sets a value indicating whether the object is active.
		/// </summary>
		/// <value><see langword="true" /> if the object is active; otherwise <see langword="false" />.</value>
		public bool IsActive
		{
			get
			{
				return _isActive;
			}
			set
			{
				if (_isActive != value)
				{
					_isActive = value;
					OnIsActiveChanged();
				}
			}
		}

		/// <summary>
		/// 当出执行条件发生更改时触发
		/// </summary>
		public virtual event EventHandler CanExecuteChanged;

		/// <summary>
		/// Fired if the <see cref="P:Prism.Commands.DelegateCommandBase.IsActive" /> property changes.
		/// </summary>
		public virtual event EventHandler IsActiveChanged;

		/// <summary>
		/// Creates a new instance of a <see cref="T:Prism.Commands.DelegateCommandBase" />, specifying both the execute action and the can execute function.
		/// </summary>
		protected DelegateCommandBase()
		{
			_synchronizationContext = SynchronizationContext.Current;
		}

		/// <summary>
		/// Raises <see cref="E:System.Windows.Input.ICommand.CanExecuteChanged" /> so every 
		/// command invoker can requery <see cref="M:System.Windows.Input.ICommand.CanExecute(System.Object)" />.
		/// </summary>
		protected virtual void OnCanExecuteChanged()
		{
			EventHandler handler = this.CanExecuteChanged;
			if (handler == null)
			{
				return;
			}
			if (_synchronizationContext != null && _synchronizationContext != SynchronizationContext.Current)
			{
				_synchronizationContext.Post(delegate
				{
					handler(this, EventArgs.Empty);
				}, null);
			}
			else
			{
				handler(this, EventArgs.Empty);
			}
		}

		/// <summary>
		/// 发出RaiseCanExecuteChanged事件
		/// </summary>
		public void RaiseCanExecuteChanged()
		{
			OnCanExecuteChanged();
		}

		void ICommand.Execute(object parameter)
		{
			Execute(parameter);
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute(parameter);
		}

		/// <summary>
		/// Handle the internal invocation of <see cref="M:System.Windows.Input.ICommand.Execute(System.Object)" />
		/// </summary>
		/// <param name="parameter">Command Parameter</param>
		protected abstract void Execute(object parameter);

		/// <summary>
		/// Handle the internal invocation of <see cref="M:System.Windows.Input.ICommand.CanExecute(System.Object)" />
		/// </summary>
		/// <param name="parameter"></param>
		/// <returns><see langword="true" /> if the Command Can Execute, otherwise <see langword="false" /></returns>
		protected abstract bool CanExecute(object parameter);

		/// <summary>
		/// Observes a property that implements INotifyPropertyChanged, and automatically calls DelegateCommandBase.RaiseCanExecuteChanged on property changed notifications.
		/// </summary>
		/// <typeparam name="T">The object type containing the property specified in the expression.</typeparam>
		/// <param name="propertyExpression">The property expression. Example: ObservesProperty(() =&gt; PropertyName).</param>
		protected internal void ObservesPropertyInternal<T>(Expression<Func<T>> propertyExpression)
		{
			if (_observedPropertiesExpressions.Contains(propertyExpression.ToString()))
			{
				throw new ArgumentException(propertyExpression.ToString() + " is already being observed.", "propertyExpression");
			}
			_observedPropertiesExpressions.Add(propertyExpression.ToString());
			PropertyObserver.Observes(propertyExpression, RaiseCanExecuteChanged);
		}

		/// <summary>
		/// This raises the <see cref="E:Prism.Commands.DelegateCommandBase.IsActiveChanged" /> event.
		/// </summary>
		protected virtual void OnIsActiveChanged()
		{
			this.IsActiveChanged?.Invoke(this, EventArgs.Empty);
		}
	}
}
