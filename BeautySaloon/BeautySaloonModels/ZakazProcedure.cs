namespace BeautySaloonModels
{
    public class ZakazProcedure
    {
        public int Id { get; set; }

        public int ZakazId { get; set; }

        public int ProcedureId { get; set; }

        public int Count { get; set; }

        public decimal Price { get; set; }

        public virtual Zakaz Zakaz { get; set; }

        public virtual Procedure Procedure { get; set; }
    }
}
