using BeautySaloon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautySaloonModel
{
    public class Payment
    {
        public int Id { get; set; }

        public decimal Summ { get; set; }

        [ForeignKey("PaymentId")]
        public virtual List<Request> Requests { get; set; }
    }
}
