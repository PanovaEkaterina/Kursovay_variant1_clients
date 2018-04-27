using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
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
