using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
{
    public class Zakaz
    {
        public int Id { get; set; }

        [Required]
        public string ZakazName { get; set; }

        [Required]
        public int KlientID { get; set; }

        [Required]
        public decimal Price{ get; set; }

        [ForeignKey("ZakazId")]
        public virtual List<Request> Requests { get; set; }

        [ForeignKey("ZakazId")]
        public virtual List<ZakazProcedure> ZakazProcedures { get; set; }
    }
}
