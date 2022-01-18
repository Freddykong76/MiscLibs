
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace MiscLibs
{
    abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion
        #region Properties
        #endregion
        #region Methods
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        protected void OnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, e);
        }
        protected virtual bool SetProperty<T>(ref T member, T val,
         [CallerMemberName] string propertyName = null)
        {
            if (Equals(member, val))
            {
                return false;
            }

            member = val;
            this.OnPropertyChanged(propertyName);
            return true;

        }
        public void Attach(IObserver observer)
        {
            _observers.Add(observer);
        }

        public void Detach(IObserver observer)
        {
            _observers.Remove(observer);
        }

        public virtual void Notify()
        {
            foreach (IObserver element in _observers)
            {
                element.Update(this);
            }
        }
        public virtual void Update(ISubject subject)
        {
            if (subject is BaseViewModel)
            {
                IsChanged = (subject as BaseViewModel).IsChanged;
                //Notify();
            }
        }
        #endregion
    }
}
