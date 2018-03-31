using BeautySaloon.Models;
using BeautySaloonModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;

namespace BeautySaloonService
{
    [Table("BeautySaloon")]
    class AbstractSaloonDbContext: DbContext
    {
        public AbstractSaloonDbContext()
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
            var ensureDLLIsCopied = System.Data.Entity.SqlServer.SqlProviderServices.Instance;
        }

        public virtual DbSet<Klient> Klients { get; set; }

        public virtual DbSet<Request> Requests { get; set; }

        public virtual DbSet<Procedure> Procedures { get; set; }

        public virtual DbSet<RequestProcedure> RequestProcedures { get; set; }

        public virtual DbSet<Payment> Payments { get; set; }
    }
}

