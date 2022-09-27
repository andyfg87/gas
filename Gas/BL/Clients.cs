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
    public class Clients : IClientService
    {
        public async Task<bool> DeleteAllClient()
        {
            using (GasContext gas = new GasContext())
            {
                gas.RemoveRange(gas.clients);
                await gas.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> DeleteClient(ClientModel client)
        {
            using (GasContext gas = new GasContext())
            {
               var tracking = gas.Remove(client);

               await gas.SaveChangesAsync();

                var isDeleted = tracking.State == EntityState.Deleted;
                return isDeleted;
            }
        }

        public async Task<ClientModel> GetClient(int idClient)
        {
            using (GasContext gas = new GasContext())
            {
                return await gas.clients.FirstOrDefaultAsync(c => c.Id == idClient);
            }
        }

        public async Task<List<ClientModel>> GetClients()
        {
            using (GasContext gas = new GasContext() )
            {
                return await gas.clients.Where(c=> c.IsProcess== false).ToListAsync();
            }
        }

        

        public async Task<bool> InsertClient(ClientModel client)
        {
            using (GasContext gas = new GasContext()) 
            {
                gas.Add(client);
                await gas.SaveChangesAsync();
            }
            return true;
        }

        public async Task<bool> UpdateClient(ClientModel client)
        {
            using (GasContext gas = new GasContext())
            {
                var tracking = gas.Update(client);
                await gas.SaveChangesAsync();

                var isModified = tracking.State == EntityState.Modified;
                return isModified;
            }
            
        }
    }
}
