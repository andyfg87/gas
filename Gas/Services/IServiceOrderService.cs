using Gas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gas.Services
{
    public interface IServiceOrderService
    {
        Task<List<ServiceOrderModel>> GetServicesOrders();

        Task<ServiceOrderModel> GetServiceOrder(ServiceOrderModel serviceOrderModel);
        Task<ServiceOrderModel> Insert(ServiceOrderModel serviceOrderModel);
        Task<bool> Update(ServiceOrderModel serviceOrderModel);

    }
}
