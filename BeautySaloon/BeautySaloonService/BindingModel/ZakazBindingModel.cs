using System.Collections.Generic;

namespace BeautySaloonService.BindingModel
{
    public class ZakazBindingModel
    {
        public int Id { get; set; }

        public string ZakazName { get; set; }

        public int KlientID { get; set; }

        public decimal Price { get; set; }

        public List<ZakazProcedureBindingModel> ZakazProcedures { get; set; }
    }
}
