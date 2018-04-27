using System;

namespace BeautySaloonService.ViewModel
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public string KlientFIO { get; set; }

        public string ProcedureName { get; set; }

        public int RequestProcedureId { get; set; }

        public int PaymentId { get; set; }

        public int Count { get; set; }

        public string Status { get; set; }

        public decimal Summ { get; set; }

        public DateTime Date { get; set; }

        public DateTime Time { get; set; }
    }
}
