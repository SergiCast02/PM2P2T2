﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PM2P2T2.Models;
using SignaturePad.Forms;
using PM2P2T2.Views;
using Xamarin.Essentials;

namespace PM2P2T2
{
    public partial class MainPage : ContentPage
    {
        byte[] ImageBytes;
        public MainPage()
        {
            InitializeComponent();
        }

        private async void btnGuardar_Clicked(object sender, EventArgs e)
        {
            var status = await Permissions.RequestAsync<Permissions.StorageRead>();
            var status2 = await Permissions.RequestAsync<Permissions.StorageWrite>();
            if (status != PermissionStatus.Granted || status2 != PermissionStatus.Granted)
            {
                return; // si no tiene los permisos no avanza
            }

            try
            {
                //obtenemos la firma
                var image = await PadView.GetImageStreamAsync(SignatureImageFormat.Png);

                //Guardamos localmente en el dispositivo
                SaveToDevice(image);

                //Pasamos la firma a imagen a base 64
                var mStream = (MemoryStream)image;
                byte[] data = mStream.ToArray();
                string base64Val = Convert.ToBase64String(data);
                ImageBytes = Convert.FromBase64String(base64Val);
            }

            catch (Exception ex)
            {
                await DisplayAlert("Error", "firma vacia", "Ok");
            }

            //seteamos los valores
            firma firma = new firma();
            firma.Nombre = txtnombre.Text;
            firma.Descripcion = txtdescripcion.Text;
            firma.Firma = ImageBytes;

            if (String.IsNullOrEmpty(txtnombre.Text) || String.IsNullOrEmpty(txtdescripcion.Text))
            {
                await DisplayAlert("Aviso", "Favor no dejar campos vacios", "Ok");
            }
            else
            {
                try
                {
                    await App.BaseDatosObject.GuadarFirma(firma);
                    await DisplayAlert("Aviso", "firma guardada", "Ok");
                    txtnombre.Text = "";
                    txtdescripcion.Text = "";
                    PadView.Clear();
                }
                catch
                {
                    await DisplayAlert("Advertencia", " Error al guardar firma", "Ok");
                }
            }

        }

        private async void btnListarFirma_Clicked(object sender, EventArgs e)
        {

            await Navigation.PushAsync(new ListaFirmas());
        }


        private void SaveToDevice(Stream img)
        {
            try
            {
                var filename = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyPictures).ToString(), "Firmas");

                if (!Directory.Exists(filename))
                {
                    Directory.CreateDirectory(filename);
                }

                string nameFile = DateTime.Now.ToString("yyyyMMddhhmmss") + ".png";
                filename = Path.Combine(filename, nameFile);

                var mStream = (MemoryStream)img;
                Byte[] bytes = mStream.ToArray();
                File.WriteAllBytes(filename, bytes);

                // DisplayAlert("Notificación", "firma guardada en la ruta: " + filename, "OK");
            }
            catch (Exception ex)
            {
                DisplayAlert("Error", ex.Message, "Ok");
            }

        }
    }
}
