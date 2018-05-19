using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
{
    public class Master
    {
        public int Id { get; set; }

        [Required]
        public string MasterFIO { get; set; }

        [ForeignKey("MasterId")]
        public virtual List<Request> Requests { get; set; }
    }
}
