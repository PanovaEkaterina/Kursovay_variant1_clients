namespace BeautySaloonService.ViewModel
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public string KlientFIO { get; set; }

        public int ProcedureId { get; set; }

        public string ProcedureName { get; set; }

        public int? MasterId { get; set; }

        public string MasterFIO { get; set; }

        public int Count { get; set; }

        public decimal Sum { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateImplement { get; set; }
    }
}
