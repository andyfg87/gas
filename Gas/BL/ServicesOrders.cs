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
            using (GasContext gas = new GasContext())
            {
                return await gas.serviceOders.FirstOrDefaultAsync(s => s.Id == serviceOrderModel.Id);
            }
        }

        public async Task<List<ServiceOrderModel>> GetServicesOrders()
        {
            using (GasContext gas = new GasContext())
            {
                return await gas.serviceOders.ToListAsync();
            }
        }

        public Task<ServiceOrderModel> GetServicesOrdersInADay(DateTimeOffset date)
        {
            using (GasContext gas = new GasContext())
            {
                return Task.FromResult(gas.serviceOders.ToList().FirstOrDefault(s => s.DateProccess.Day == date.Day &&
                 s.DateProccess.Month == date.Month && s.DateProccess.Year == date.Year));               
            }
        }

        public   List<ServiceOrderModel> GetServicesOrdersInADate(DateTimeOffset date)
        {
            List<ServiceOrderModel> temp = new List<ServiceOrderModel>();
            using (GasContext gas = new GasContext())
            {
                temp.Clear();
                foreach(var elemt in gas.serviceOders.ToList())
                {
                    if (elemt.DateProccess.Year == date.Year && elemt.DateProccess.Month == date.Month && elemt.DateProccess.Day == date.Day)
                    {
                        temp.Add(elemt);
                    }
                }
                return  temp;
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
        }

        public async Task<bool> Delete(ServiceOrderModel serviceOrderModel)
        {
            using (GasContext gas = new GasContext())
            {
                ClientModel client = await gas.clients.FirstOrDefaultAsync(c => c.Id == serviceOrderModel.IdClient);

                if (client != null)
                {
                    client.IsProcess = false;
                    gas.Update(client);
                }                
                gas.serviceOders.Remove(serviceOrderModel);             
                await gas.SaveChangesAsync();               
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
