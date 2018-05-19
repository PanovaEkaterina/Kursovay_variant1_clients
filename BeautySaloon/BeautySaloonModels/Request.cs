using System;

namespace BeautySaloonModels
{
    public class Request
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public int ProcedureId { get; set; }

        public int? MasterId { get; set; }

        public int AdminId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public PaymentState Status { get; set; }

        public DateTime DateCreate { get; set; }

        public DateTime? DateImplement { get; set; }

        public virtual Klient Klient { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Master Master { get; set; }

        public virtual Admin Admin { get; set; }

    }
}
