namespace BeautySaloonService.ViewModel
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public string KlientFIO { get; set; }

        public int ZakazId { get; set; }

        public string ZakazName { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }

        public string Status { get; set; }

        public string DateCreate { get; set; }

        public string DateVisit { get; set; }
    }
}
