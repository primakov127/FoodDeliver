using System;
using System.Windows.Input;

namespace FoodDelivery.DesktopUI.Helpers
{
    public class Command : ICommand
    {
        private Action<object> executeMethod { get; set; }
        private Func<object, bool> canExecuteMethod { get; set; }

        public Command(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
        {
            this.executeMethod = executeMethod;
            this.canExecuteMethod = canExecuteMethod;
        }

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
            }
        }

        public bool CanExecute(object parameter)
        {
            return canExecuteMethod == null ? true : canExecuteMethod.Invoke(parameter);
        }

        public void Execute(object parameter)
        {
            executeMethod.Invoke(parameter);
        }
    }
}
