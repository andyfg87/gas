using Gas.Models;
using Gas.ViewsModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Gas.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ClientPage : ContentPage
    {
        private ClientViewModel _clientViewModel;
        public ClientPage()
        {
            InitializeComponent();
            BindingContext = _clientViewModel = new ClientViewModel();
        }

        private async void UpdateRow_Tapped(object sender, EventArgs e)
        {
            var container = ((Frame)sender).GestureRecognizers[0];

            ClientModel clientModel = ((TapGestureRecognizer)container).CommandParameter as ClientModel;

            string Text = await DisplayPromptAsync("Cliente", clientModel.Text);
            if (String.IsNullOrEmpty(Text))
            {
                Text = clientModel.Text;
            }
            clientModel.Text = Text;
            

            string dateString = await DisplayPromptAsync("Fecha(MM/dd/yyyy)", clientModel.DateCreated.ToString());
            if (String.IsNullOrEmpty(dateString))
            {
                dateString = clientModel.DateCreated.ToString();
               
            }
            clientModel.DateCreated = DateTimeOffset.Parse(dateString);

            _clientViewModel.UpdateCommand.Execute(clientModel);
        }

        private async void DeleteRow_Swiped(object sender, SwipedEventArgs e)
        {
            bool result = await DisplayAlert("Eliminar", "Seguro que desea elminar?", "Si","No");

            if (result)
            {
                var container = ((Frame)sender).GestureRecognizers[0];
                ClientModel clientModel = ((TapGestureRecognizer)container).CommandParameter as ClientModel;
                _clientViewModel.DeleteCommand.Execute(clientModel);
            }
        }

        private async void Proccess_Tapped(object sender, EventArgs e)
        {            
            var container = ((Frame)sender).GestureRecognizers[0];
            ClientModel clientModel = ((TapGestureRecognizer)container).CommandParameter as ClientModel;           
            
            await _clientViewModel.InsertServiceOrder(clientModel);
           
        }

        private void searchClientBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            var searchBar = _clientViewModel.ListClient.Where(c => c.Text.ToLower().Contains(searchClientBar.Text.ToLower()));
            clientsList.ItemsSource = searchBar;
        }
    }
}