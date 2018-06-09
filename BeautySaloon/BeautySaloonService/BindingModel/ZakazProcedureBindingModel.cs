namespace BeautySaloonService.BindingModel
{
    public class ZakazProcedureBindingModel
    {
        public int Id { get; set; }

        public int ZakazId { get; set; }

        public int ProcedureId { get; set; }

        public decimal Price { get; set; }
    }
}
