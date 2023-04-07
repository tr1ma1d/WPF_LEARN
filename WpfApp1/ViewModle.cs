using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Runtime.CompilerServices;

namespace WpfApp1
{
    abstract class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName]string PropertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(PropertyName));
        }

        protected bool Set<T>(ref T field, T value, [CallerMemberName] string Propery = null)
        {
            if(Equals(field,value))
            {
                return false;
            }
            else
            {
                field = value;
                OnPropertyChanged(Propery);
                return true;
            }
            
        }
    }
}
