using PM2P2T2.Controllers;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2P2T2
{
    public partial class App : Application
    {
        static FirmaController BaseDatos;

        public static FirmaController BaseDatosObject
        {
            get
            {
                if (BaseDatos == null)
                {
                    BaseDatos = new FirmaController(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "FirmasDB.db3"));
                }
                return BaseDatos;
            }
        }

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
