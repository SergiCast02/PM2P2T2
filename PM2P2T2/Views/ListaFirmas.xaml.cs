using PM2P2T2.Controllers;
using PM2P2T2.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace PM2P2T2.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListaFirmas : ContentPage
    {
        public ListaFirmas()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            listaFirmas.ItemsSource = await App.BaseDatosObject.GetFirmasList();
        }
    }
}