using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using Prism;

namespace GroupmeAnalytics.Viewmodels {
    class ViewModelBase : Prism.Mvvm.BindableBase {
        private static ObservableCollection<MenuItem> menu = new ObservableCollection<MenuItem>();

        public ViewModelBase() { }

        public ObservableCollection<MenuItem> Menu {
            get { return menu; }
        }
    }
}
