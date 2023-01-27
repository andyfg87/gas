using Gas.BL;
using Gas.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace Gas.ViewsModels
{
    public class ServiceOrderViewModel: BaseViewModel
    {
        public ServicesOrders servicesOrders = new ServicesOrders();
        public ServiceOrderModel oldServiceOrder { get; set; }
        public Clients clients = new Clients();
        private ObservableCollection<ServiceOrderModel> listServiceOrder;
        private ObservableCollection<ClientModel> listClientModel;
        public string DeliveredWithDate { get; set; }
        

        public ServiceOrderViewModel()
        {            
            Task.WaitAny( LoadList());
        }

        public ObservableCollection<ServiceOrderModel> ListServiceOrder
        {
            get => listServiceOrder;
            set => SetValue(ref listServiceOrder, value);
        }
        public async Task LoadList()
        {
           ListServiceOrder = new ObservableCollection<ServiceOrderModel>(await servicesOrders.GetServicesOrders());           
        }

        

        public ObservableCollection<ClientModel> ListClientModel
        {
            get => listClientModel;
            set => SetValue(ref listClientModel, value);
        }

        public async Task ShowHideButtonOfList(ServiceOrderModel serviceOrderModel)
        {
            if (oldServiceOrder == null)
            {
                oldServiceOrder = serviceOrderModel;
                await servicesOrders.Update(serviceOrderModel);
            }
            else
            {
                oldServiceOrder.isVisible = false;
                await servicesOrders.Update(oldServiceOrder);
                await servicesOrders.Update(serviceOrderModel);
                oldServiceOrder = serviceOrderModel;
            }
            
            await LoadList();
        }

        public async Task Delivered(ServiceOrderModel serviceOrderModel)
        {
            serviceOrderModel.Delivered = true;
            serviceOrderModel.isVisible = false;
            serviceOrderModel.IsStored = false;            
            await servicesOrders.Update(serviceOrderModel);
            oldServiceOrder = serviceOrderModel;
            await LoadList();
        }

        public async Task Stored(ServiceOrderModel serviceOrderModel)
        {
            serviceOrderModel.IsStored = true;
            serviceOrderModel.Delivered = false;
            serviceOrderModel.isVisible = false;
            await servicesOrders.Update(serviceOrderModel);
            oldServiceOrder = serviceOrderModel;
            await LoadList();
        }

        public async Task CleanServiceOrderInADate()
        {
            string dateString = await App.Current.MainPage.DisplayPromptAsync("Fecha(MM/dd/yyyy)", "");
            if (String.IsNullOrEmpty(dateString))
            {
               await App.Current.MainPage.DisplayAlert("ERROR","Debe insertar una fecha en el formato MM/dd/yyyy", "Ok");
            }
            else
            {
                DateTimeOffset date = DateTimeOffset.Parse(dateString);
                List<ServiceOrderModel> sericeOrdersTemp =  servicesOrders.GetServicesOrdersInADate(date);
                
                foreach (var elemet in sericeOrdersTemp)
                {
                    elemet.Delivered = true;
                    await Task.WhenAny(servicesOrders.Update(elemet));
                }
            }
            await LoadList();
        }

        public async Task Delete(ServiceOrderModel serviceOrderModel)
        {        
            
            await servicesOrders.Delete(serviceOrderModel);
            await LoadList();
        }

        #region COMMANS
        public ICommand ShowHideButtonCommand => new Command<ServiceOrderModel> (async (serviceOrderModel) => await ShowHideButtonOfList(serviceOrderModel));
        public ICommand DeleteCommand => new Command<ServiceOrderModel>(async (serviceOrderModel) => await Delete(serviceOrderModel));
        public ICommand DeliveredCommad => new Command<ServiceOrderModel>( async (serviceOrderModel) => await Delivered(serviceOrderModel));
        public ICommand CleanServiceOrderInADateCommand => new Command(async () => await CleanServiceOrderInADate());
        #endregion
    }
}
