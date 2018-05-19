namespace BeautySaloonService.BindingModel
{
    public class RequestBindingModel
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public int ProcedureId { get; set; }

        public int? MasterId { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

    }
}
