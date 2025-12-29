using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Kolumban_Brigitta_Proiect.Models
{
    public class Guest
    {
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        [DisplayName("Phone Number")]
        public string Phone { get; set; }

        public ICollection<Reservation>? Reservations { get; set; }
    }
}
