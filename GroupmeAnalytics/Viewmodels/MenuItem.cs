using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GroupmeAnalytics.Viewmodels {

    class MenuItem : Prism.Mvvm.BindableBase {
        private string source;
        private string text;
        private string id;
        private Prism.Commands.DelegateCommand command;
        private Type navigationDestination;

        public string Source {
            get { return source; }
            set { SetProperty(ref source, value); }
        }

        public string Text {
            get { return text; }
            set { SetProperty(ref text, value); }
        }

        public string ID {
            get { return id; }
            set { SetProperty(ref id, value); }
        }

        public System.Windows.Input.ICommand Command {
            get { return command; }
            set { SetProperty(ref command, (Prism.Commands.DelegateCommand)value); }
        }
    }
}
