using System.ComponentModel.DataAnnotations;

namespace Kolumban_Brigitta_Proiect.Models
{
    public class Reservation
    {
        public int ID { get; set; }

        [Required]
        [Display(Name = "Check-in Date")]
        public DateTime CheckInDate { get; set; }

        [Required]
        [Display(Name = "Check-out Date")]
        public DateTime CheckOutDate { get; set; }

        [Display(Name = "Room")]
        public int RoomId { get; set; }
        public Room? Room { get; set; }

        [Display(Name = "Guest")]
        public int GuestId { get; set; }
        public Guest? Guest { get; set; }
    }
}
