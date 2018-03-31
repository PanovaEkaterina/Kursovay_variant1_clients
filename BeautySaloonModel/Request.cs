using BeautySaloonModel;
using System;


namespace BeautySaloon.Models
{
    public class Request
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public int RequestProcedureId { get; set; }

        public int PaymentId { get; set; }

        public int Count { get; set; }

        public decimal Summ { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }

        public virtual Klient Klient { get; set; }

        public virtual RequestProcedure RequestProcedure { get; set; }

        public virtual Payment Payment { get; set; }

    }
}
