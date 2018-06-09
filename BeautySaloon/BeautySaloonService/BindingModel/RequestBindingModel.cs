namespace BeautySaloonService.BindingModel
{
    public class RequestBindingModel
    {
        public int Id { get; set; }

        public int KlientId { get; set; }

        public int ZakazId { get; set; }

        public decimal Sum { get; set; }

        public decimal SumPay { get; set; }

        public string DataVisit { get; set; }

    }
}
