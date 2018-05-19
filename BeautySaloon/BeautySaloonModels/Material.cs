using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloonModels
{
    public class Material
    {
        public int Id { get; set; }

        [Required]
        public string MaterialName { get; set; }

        [ForeignKey("MaterialId")]
        public virtual List<ProcedureMaterial> ProcedureMaterials { get; set; }
    }
}
