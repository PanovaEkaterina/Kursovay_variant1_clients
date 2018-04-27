using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
{
    public class Procedure
    {
        public int Id { get; set; }

        [Required]
        public string ProcedureName { get; set; }

        [Required]
        public decimal Price { get; set; }

        [ForeignKey("ProcedureId")]
        public virtual List<RequestProcedure> RequestProcedures { get; set; }
    }
}
