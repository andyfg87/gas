using Gas.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Gas.Services
{
    public interface IClientService
    {
        Task<List<ClientModel>> GetClients();
        Task<ClientModel> GetClient(int idClient);
        Task<bool> InsertClient(ClientModel client);
        Task<bool> UpdateClient(ClientModel client);
        Task<bool> DeleteClient(ClientModel client);
        Task<bool> DeleteAllClient();
    }
}
