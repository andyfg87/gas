using Gas.Data;
using Gas.Models;
using Gas.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;


namespace Gas.BL
{
    public class ServicesOrders : IServiceOrderService
    {
        public async Task<ServiceOrderModel> GetServiceOrder(ServiceOrderModel serviceOrderModel)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ServiceOrderModel>> GetServicesOrders()
        {
            using (GasContext gas = new GasContext())
            {
                return await gas.serviceOders.ToListAsync();
            }
        }

        public async Task<ServiceOrderModel> GetServicesOrdersInADay(DateTimeOffset date)
        {
            using (GasContext gas = new GasContext())
            {
                return gas.serviceOders.ToList().FirstOrDefault(s => s.DateProccess.Day == date.Day &&
                 s.DateProccess.Month == date.Month && s.DateProccess.Year == date.Year);               
            }
        }        

        public async Task<ServiceOrderModel> Insert(ServiceOrderModel serviceOrderModel)
        {
            using (GasContext gas = new GasContext())
            {
                gas.Add(serviceOrderModel);
                var  aa= await gas.SaveChangesAsync();
               
                return serviceOrderModel;
            }
        }

        public async Task<bool> Update(ServiceOrderModel serviceOrderModel)
        {
            using (GasContext gas = new GasContext())
            {
                var tracking = gas.Update(serviceOrderModel);
                await gas.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
                return true;
        }

        public async Task<bool> Delete(ServiceOrderModel serviceOrderModel)
        {
            using (GasContext gas = new GasContext())
            {
                gas.serviceOders.Remove(serviceOrderModel);
                ClientModel client = await gas.clients.FirstOrDefaultAsync(c => c.Id == serviceOrderModel.IdClient);
                client.IsProcess = false;
                gas.Update(client);
                await gas.SaveChangesAsync();
                await gas.DisposeAsync();
            }            
                return true;
        }

        public async Task<bool> Delivered(ServiceOrderModel serviceOrderModel)
        {
            using (GasContext gas = new GasContext())
            {
                serviceOrderModel.Delivered = true;
                return await Update(serviceOrderModel);
            }
        }
    }
}
