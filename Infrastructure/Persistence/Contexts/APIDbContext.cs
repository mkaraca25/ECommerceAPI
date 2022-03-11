using Domain.Entities;
using Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class APIDbContext : DbContext
    {
        public APIDbContext(DbContextOptions options) : base(options)
        { }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            //ChangeTracker Entityler uzerinde yapılan degisiklerin ya da eklenen verileri yakalanmasini saglayan propertydir.Update operasyonların da Track edilen verileri yakalayip elde etmemizi saglar.
            var datas=ChangeTracker.Entries<BaseEntity>();
            foreach (var data in datas)
            {
                _= data.State switch
                {
                    EntityState.Added=>data.Entity.CreatedDate=DateTime.UtcNow,
                    EntityState.Modified => data.Entity.UpdatedDate = DateTime.UtcNow

                };
            }
            return base.SaveChangesAsync(cancellationToken);
        }

    }
}
