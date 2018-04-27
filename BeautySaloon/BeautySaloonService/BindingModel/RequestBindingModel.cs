using System;

namespace BeautySaloonService.BindingModel
{
    public class RequestBindingModel
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public int RequestProcedureId { get; set; }

        public int PaymentId { get; set; }

        public int Count { get; set; }

        public decimal Summ { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
    }
}
