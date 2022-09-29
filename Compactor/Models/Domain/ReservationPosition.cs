using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Compactor.Models.Domain
{
    public class ReservationPosition
    {
        public ReservationPosition()
        {

        }
        public ReservationPosition(EquipmentType eqType, List<ReservationPosition> list)
        {
            ID = 0;
            RentQuantity = 1;
            EquipmentID = 0;
            TypeID = eqType.ID;
            Type = eqType;
            ReservationID = 0;
            IsActiv = false;
            SequenceNumber = list.Count() + 1;            
        }

        public int ID { get; set; }
        public bool IsActiv { get; set; }
        [Required(ErrorMessage = "Pole lp jest wymagane!")]
        public int SequenceNumber { get; set; }

        [Required(ErrorMessage = "Pole Ilość jest wymagane!")]
        [Display(Name = "Ilość:")]
        public int RentQuantity { get; set; }

        [Required]
        [ForeignKey("Equipment")]
        public int EquipmentID { get; set; }
        public Equipment Equipment { get; set; }

        [Required]
        [ForeignKey("Type")]
        public int TypeID { get; set; }
        public EquipmentType Type { get; set; }

        [Required]
        [ForeignKey("Reservation")]
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; }

    }
}