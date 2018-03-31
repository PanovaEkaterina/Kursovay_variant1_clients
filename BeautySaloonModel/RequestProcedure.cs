
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySaloon.Models
{
    public class RequestProcedure
    {
        public int Id { get; set; }

        public int ProcedureId { get; set; }

        public int Count { get; set; }
                
        public virtual Procedure Procedure { get; set; }

        [ForeignKey("RequestProcedureId")]
        public virtual List<Request> Requests { get; set; }
    }
}
