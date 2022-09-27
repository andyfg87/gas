using Gas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xamarin.Essentials;

namespace Gas.Data
{
    public class GasContext: DbContext
    {
        public DbSet<ClientModel> clients { get; set; }
        public DbSet<ServiceOrderModel> serviceOders { get; set; }

        public GasContext()
        {
            SQLitePCL.Batteries_V2.Init();
            //this.Database.EnsureDeleted();
            this.Database.EnsureCreated();
        }        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dbPath = Path.Combine(FileSystem.AppDataDirectory, "gas10.db3");
            optionsBuilder.UseSqlite($"filename={dbPath}");
        }
    }
}
