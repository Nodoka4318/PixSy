using PixSy.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PixSy {
    internal class PixSyApp {
        public static MainView MainView { get; private set; }

        public PixSyApp() { 
            MainView = new MainView();
        }

        public void Run() {
            Application.Run(MainView);
        }
    }
}
