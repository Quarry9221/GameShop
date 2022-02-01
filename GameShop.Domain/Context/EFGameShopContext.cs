using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using GameShop.Domain.Entities;
namespace GameShop.Domain.Context
{
    class EFGameShopContext : DbContext
    {
        public DbSet<Game> Games { get; set; }
    }
}
