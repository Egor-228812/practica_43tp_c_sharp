using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Petrov_Tema_14
{
    public class RelayCommand : ICommand
    {
        private readonly Func<Task> executeAsync;
        private readonly Func<bool> canExecute;

        public RelayCommand(Func<Task> executeAsync, Func<bool> canExecute = null)
        {
            this.executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            this.canExecute = canExecute;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter) => canExecute == null || canExecute();
        public async void Execute(object parameter) => await executeAsync();
    }
}