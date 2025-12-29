using System.ComponentModel.DataAnnotations;

namespace Kolumban_Brigitta_Proiect.Models
{
    public class Room
    {
        public int Id { get; set; }
        public int HotelId { get; set; }
        public Hotel? Hotel { get; set; }
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }
        public int Capacity { get; set; }
        [Display(Name = "Price/Night")]
        public decimal PricePerNight { get; set; }
        public bool Availability { get; set; }
        public ICollection<Reservation>? Reservations { get; set; }

    }
}
