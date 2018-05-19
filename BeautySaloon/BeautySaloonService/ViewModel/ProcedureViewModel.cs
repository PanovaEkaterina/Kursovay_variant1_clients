using System.Collections.Generic;

namespace BeautySaloonService.ViewModel
{
    public class ProcedureViewModel
    {
        public int Id { get; set; }

        public string ProcedureName { get; set; }

        public decimal Price { get; set; }

        public List<ProcedureMaterialViewModel> ProcedureMasters { get; set; }
    }
}
