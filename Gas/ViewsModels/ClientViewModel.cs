using Gas.BL;
using Gas.Models;
using Gas.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gas.ViewsModels
{
    public class ClientViewModel: BaseViewModel
    {
        public Clients clientsDB = new Clients();
        public ServicesOrders servicesOrdersDB = new ServicesOrders();
        private ObservableCollection<ClientModel> listClient;
        private string _text;
        private DateTime dateCreated = DateTime.Now;       

        public ObservableCollection<ClientModel> ListClient
        {
            get => listClient;
            set => SetProperty(ref listClient, value);
        }

        public ClientViewModel()
        {            
            Task.WaitAny( LoadListClient());            
        }

        public string Text
        {
            get => _text;
            set => SetProperty(ref _text, value);
        }

        public DateTime DateCreated
        {
            get => dateCreated;
            set => SetValue(ref dateCreated, value);
        }

        #region PROCESS
        public async Task AddClient()
        {
            string dateString = DateCreated.ToString();
            await clientsDB.InsertClient(new ClientModel { Text = Text, DateCreated = DateTimeOffset.Parse(dateString)});
            Text = "";
            await LoadListClient();
        }

        public async Task DeleteClient(ClientModel clientModel)
        {
           await clientsDB.DeleteClient(clientModel);
           await LoadListClient();
        }

        public async Task UpdateClient(ClientModel client)
        {
            await clientsDB.UpdateClient(client);
            await LoadListClient();
        }

        public async Task DeleteAll()
        {
            await clientsDB.DeleteAllClient();
            await LoadListClient();
        }

        public async Task InsertServiceOrder(ClientModel client)
        {           
            string dateProccessString = DateTime.Now.ToString();
            var serviceOrder= await servicesOrdersDB.Insert(new ServiceOrderModel { DateProccess = DateTimeOffset.Parse(dateProccessString), Text= client.Text, IdClient = client.Id });
                        
            _ = servicesOrdersDB.Update(serviceOrder);
            client.IsProcess = true;           
            _ = clientsDB.UpdateClient(client);
            await LoadListClient();
        }
        

        public async Task<bool> existServiceOrder(ClientModel client)
        {
            var serviceOrder = await servicesOrdersDB.GetServicesOrdersInADay(client.DateCreated);
            if (serviceOrder!=null)
            {
                return true;
            }
            return false;
        }

        public async Task LoadListClient()
        {
            ListClient = new ObservableCollection<ClientModel>(await clientsDB.GetClients());
        }
        
        #endregion

        #region COMMANDS

        public ICommand InsertCommand => new Command(async () => await AddClient());
        public ICommand UpdateCommand => new Command<ClientModel>( async (clientModel) => await UpdateClient(clientModel));
        public ICommand DeleteCommand => new Command<ClientModel>(async (clientModel) => await DeleteClient(clientModel));
        public ICommand DeleteAllCommand => new Command(async () => await DeleteAll());
        public ICommand ProccessCommand => new Command<ClientModel>(async (clientModel) => await InsertServiceOrder(clientModel));        
        #endregion
    }
}
