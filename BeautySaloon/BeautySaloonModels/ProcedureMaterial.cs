namespace BeautySaloonModels
{
    public class ProcedureMaterial
    {
        public int Id { get; set; }

        public int ProcedureId { get; set; }

        public int MaterialId { get; set; }

        public decimal Count { get; set; }

        public virtual Procedure Procedure { get; set; }

        public virtual Material Material { get; set; }
    }
}
