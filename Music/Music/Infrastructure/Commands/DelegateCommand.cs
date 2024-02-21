using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Music.Infrastructure.Commands
{
    public class DelegateCommand : DelegateCommandBase
    {
        public DelegateCommand(Func<object, bool> delegateCommandCanExecute, Action<object> delegateCommandExecute)
        {
            DelegateCommandCanExecute = delegateCommandCanExecute;
            DelegateCommandExecute = delegateCommandExecute;
        }

        public Func<object, bool> DelegateCommandCanExecute;


        public Action<object> DelegateCommandExecute;

        protected override bool CanExecute(object parameter)
        {
            return this.DelegateCommandCanExecute?.Invoke(parameter)??true;
        }

        protected override void Execute(object parameter)
        {
            DelegateCommandExecute?.Invoke(parameter);
        }
    }
}
