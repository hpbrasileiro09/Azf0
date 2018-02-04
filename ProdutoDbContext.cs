using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public partial class ProdutoDbContext : System.Data.Entity.DbContext
    {
        public ProdutoDbContext(string cs) : base(cs) { }
        public DbSet<Produto> Produtos { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}
