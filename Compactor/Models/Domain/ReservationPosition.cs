using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compactor.Models.Domain
{
    public class ReservationPosition
    {

        public int Id { get; set; }
        public bool IsActiv { get; set; }
        [Required(ErrorMessage = "Pole lp jest wymagane!")]
        public int SequenceNumber { get; set; }

        [Required(ErrorMessage = "Pole Ilość jest wymagane!")]
        [Display(Name = "Ilość:")]
        public int RentQuantity { get; set; }

        [Required]
        [ForeignKey("Equipment")]
        public int IdEquipment { get; set; }
        public Equipment Equipment { get; set; }

        [Required]
        [ForeignKey("Group")]
        public int IdGroup { get; set; }
        public EquipmentGroup Group { get; set; }

        [Required]
        [ForeignKey("Reservation")]
        public int IdReservation { get; set; }
        public Reservation Reservation { get; set; }

    }
}