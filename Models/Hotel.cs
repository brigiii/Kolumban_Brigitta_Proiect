using System.ComponentModel.DataAnnotations;

namespace Kolumban_Brigitta_Proiect.Models
{
    public class Hotel
    {
        public int ID { get; set; }
        [Display(Name = "Hotel Name")]
        public string Name { get; set; }
        public string City { get; set; }
        public string Address { get; set; }

        public ICollection<Room>? Rooms { get; set; }
    }
}
