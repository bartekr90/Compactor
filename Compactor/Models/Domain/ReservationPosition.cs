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
        public ReservationPosition(Device device, List<ReservationPosition> list, string userId)
        {
            ID = 0;
            RentQuantity = 1;
            DeviceID = device.ID;
            Device = device;
            TypeID = device.TypeID;
            Type = device.Type;
            IsActiv = false;
            UserID = userId;
            SequenceNumber = list.Count() + 1;                   
        }

        public int ID { get; set; }
        public bool IsActiv { get; set; }

        [Required(ErrorMessage = "Pole lp jest wymagane!")]
        [Display(Name = "Lp:")]
        public int SequenceNumber { get; set; }

        [Required(ErrorMessage = "Pole Ilość jest wymagane!")]
        [Display(Name = "Ilość:")]
        public int RentQuantity { get; set; }

        [Required]
        [ForeignKey("Device")]
        public int DeviceID { get; set; }
        public Device Device { get; set; }

        [Required]
        [ForeignKey("Type")]
        public int TypeID { get; set; }
        public EquipmentType Type { get; set; }

        [Required]
        [ForeignKey("Reservation")]
        public int ReservationID { get; set; }
        public Reservation Reservation { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }
    }
}