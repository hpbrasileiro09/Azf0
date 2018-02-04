using System.Linq;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using FunctionApp1.Totvs;

namespace FunctionApp1
{

    public class SMContext : DbContext
    {

        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<PedidoItem> PedidoItens { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<SB2200H> EstoquesT { get; set; }
        public DbSet<SC5200H> PedidosT { get; set; }
        public DbSet<SB1200H> PrecosT { get; set; }

        public virtual DbSet<Cliente> Cliente { get; set; }
        public virtual DbSet<Pedido> Pedido { get; set; }
        public virtual DbSet<PedidoItem> PedidoItem { get; set; }
        public virtual DbSet<Produto> Produto { get; set; }
        public virtual DbSet<SB2200H> EstoqueT { get; set; }
        public virtual DbSet<SC5200H> PedidoT { get; set; }
        public virtual DbSet<SB1200H> PrecoT { get; set; }

        public SMContext() : base("Server=insity01.database.windows.net;Database=hub01;User Id=insity;Password=hub#141215")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }

}