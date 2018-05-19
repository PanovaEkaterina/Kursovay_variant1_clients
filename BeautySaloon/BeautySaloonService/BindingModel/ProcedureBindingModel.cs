using System.Collections.Generic;

namespace BeautySaloonService.BindingModel
{
    public class ProcedureBindingModel
    {
        public int Id { get; set; }

        public string ProcedureName { get; set; }

        public decimal Price { get; set; }

        public List<ProcedureMaterialBindingModel> ProcedureMaterials { get; set; }
    }
}
