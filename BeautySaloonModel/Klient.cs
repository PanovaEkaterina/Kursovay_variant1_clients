using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeautySaloon.Models
{
    public class Klient
    {
        public int Id { get; set; }

        [Required]
        public string KlientFIO { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Login { get; set; }

        [Required]
        public string Password { get; set; }

        [ForeignKey("KlientId")]
        public virtual List<Request> Requests { get; set; }
    }
}
