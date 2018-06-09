using BeautySaloonModels;
using System.Data.Entity;

namespace BeautySaloonService
{
    public class SaloonDbContext : DbContext
    {
        public SaloonDbContext() : base("BeautySaloon")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Klient> Klients { get; set; }

        public virtual DbSet<Procedure> Procedures { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Zakaz> Zakazs { get; set; }

        public virtual DbSet<ZakazProcedure> ZakazProcedures { get; set; }

    }
}
