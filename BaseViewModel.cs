
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
        #endregion
    }
}
