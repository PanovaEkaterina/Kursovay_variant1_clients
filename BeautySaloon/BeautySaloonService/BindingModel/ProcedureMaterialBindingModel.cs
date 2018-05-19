namespace BeautySaloonService.BindingModel
{
    public class ProcedureMaterialBindingModel
    {
        public int Id { get; set; }

        public int ProcedureId { get; set; }

        public int MaterialId { get; set; }

        public int Count { get; set; }
    }
}
