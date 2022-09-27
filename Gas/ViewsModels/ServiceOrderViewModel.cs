﻿using Gas.BL;
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
        

        public ServiceOrderViewModel(INavigation navigation)
        {
            Navigation = navigation;
            LoadList();
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
            await servicesOrders.Delivered(serviceOrderModel);
            LoadList();
        }

        public async Task Delete(ServiceOrderModel serviceOrderModel)
        {            
            await servicesOrders.Delete(serviceOrderModel);
            LoadList();
        }

        #region COMMANS
        public ICommand ShowHideButtonCommand => new Command<ServiceOrderModel> (async (serviceOrderModel) => await ShowHideButtonOfList(serviceOrderModel));
        public ICommand DeleteCommand => new Command<ServiceOrderModel>(async (serviceOrderModel) => await Delete(serviceOrderModel));
        public ICommand DeliveredCommad => new Command<ServiceOrderModel>( async (serviceOrderModel) => await Delivered(serviceOrderModel)); 
        #endregion
    }
}
