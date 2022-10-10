using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compactor.Models.Domain
{
    public class Device
    {
        public Device()
        {
            ReservationPositions = new Collection<ReservationPosition>();
        }
        
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Numer seryjny")]
        public string SerialNr { get; set; }
        public bool IsRented { get; set; }

        [Required]
        [ForeignKey("Type")]
        public int TypeID { get; set; }
        public EquipmentType Type { get; set; }
        public ICollection<ReservationPosition> ReservationPositions { get; set; }


    }
}