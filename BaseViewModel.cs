using ODIN.DesignPattern.Observer;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
namespace MiscLibs
{
    abstract class BaseViewModel : INotifyPropertyChanged, ISubject, IObserver, IChangeTracking
    {
        #region Fields
        public event PropertyChangedEventHandler PropertyChanged;
        private List<IObserver> _observers = new List<IObserver>();
        protected bool _IsChanged = false;
        #endregion
        #region Properties
        public virtual bool IsChanged
        {
            get
            {
                return _IsChanged;
            }
            set
            {
                if (_IsChanged != value)
                {
                    _IsChanged = value;
                    Notify();
                }
            }
        }
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
        public virtual void AcceptChanges()
        {
            IsChanged = false;
        }
        #endregion
    }
}
