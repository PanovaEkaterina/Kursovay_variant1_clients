using System.Collections.Generic;

namespace BeautySaloonService.ViewModel
{
    public class ZakazViewModel
    {
        public int Id { get; set; }

        public string ZakazName { get; set; }

        public decimal Price { get; set; }

        public List<ZakazProcedureViewModel> ZakazProcedures { get; set; }
    }
}
