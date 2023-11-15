using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace EventAndStateViewer.Mvvm
{
    /// <summary>
    /// Base class for view models containing common functionality.
    /// </summary>
    public abstract class ViewModelBase : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void SetProperty<T>(ref T field, T value, [CallerMemberName] string propertyName = "")
        {
            field = value;
            InvokePropertyChanged(propertyName);
        }

        protected void InvokePropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Returns result of lookupTask immediately, if it is complete.
        /// Otherwise returns "Loading..." and invokes PropertyChanged once task is complete.
        /// This is used to load properties asynchronously without blocking the UI.
        /// </summary>
        protected string LoadProperty(Task<string> lookupTask, [CallerMemberName] string propertyName = "")
        {
            // If task is already complete
            if (lookupTask.IsFaulted)
                return "(Unknown)";
            if (lookupTask.IsCompleted)
                return lookupTask.Result;

            // Reload the property after task is completed and return "Loading..." for now
            UpdateAfterCompletion(lookupTask, propertyName);

            return "Loading...";
        }

        private async void UpdateAfterCompletion(Task task, string propertyName)
        {
            try { await task; }
            catch { }
            InvokePropertyChanged(propertyName);
        }
    }
}
