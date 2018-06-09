using System;

namespace BeautySaloonModels
{
    public class Request
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public int ZakazId { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }

        public PaymentState Status { get; set; }

        public DateTime DateCreate { get; set; }

        public string DateVisit { get; set; }
        
        public virtual Klient Klient { get; set; }

        public virtual Zakaz Zakaz { get; set; }

    }
}
