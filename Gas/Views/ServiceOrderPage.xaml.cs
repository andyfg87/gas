using Gas.Models;
using Gas.ViewsModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ServiceOrderPage : ContentPage
    {
        private ServiceOrderViewModel vm;
        public ServiceOrderPage()
        {
            InitializeComponent();
            BindingContext = vm= new ServiceOrderViewModel();
        }

        private  void ShowOrHideButtons_Tapped(object sender, EventArgs e)
        {
            var container = ((Frame)sender).GestureRecognizers[0];
            ServiceOrderModel serviceOrderModel = ((TapGestureRecognizer)container).CommandParameter as ServiceOrderModel;

            serviceOrderModel.isVisible = !serviceOrderModel.isVisible;
            vm.ShowHideButtonCommand.Execute(serviceOrderModel);            
        }

        private async void DeleteElement_Tapped(object sender, EventArgs e)
        {
            var container = ((Frame)sender).GestureRecognizers[0];
            bool result = await DisplayAlert("Eliminar", "Seguro que desea elminar? Esto pasará visible a clintes ", "Si", "No");
            if (result)
            {
                ServiceOrderModel serviceOrderModel = ((TapGestureRecognizer)container).CommandParameter as ServiceOrderModel;
                await vm.Delete(serviceOrderModel);
            }           

        }

        private async void Delivered_Clicked(object sender, EventArgs e)
        {
            var serviceOrderModel = ((Button)sender).BindingContext as ServiceOrderModel;
            await vm.Delivered(serviceOrderModel);
        }       
    }
}