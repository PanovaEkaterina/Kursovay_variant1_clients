using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
{
    public class Payment
    {
        public int Id { get; set; }

        public decimal Summ { get; set; }

        [ForeignKey("PaymentId")]
        public virtual List<Request> Requests { get; set; }
    }
}
