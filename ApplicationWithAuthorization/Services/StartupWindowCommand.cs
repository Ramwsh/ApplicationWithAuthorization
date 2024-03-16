using System.Windows.Input;

namespace ApplicationWithAuthorization.Services
{
    public class StartupWindowCommand : ICommand
    {
        private Action _executeAction;

        public StartupWindowCommand(Action executeAction)
        {
            _executeAction = executeAction;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _executeAction?.Invoke();
        }
    }
}
