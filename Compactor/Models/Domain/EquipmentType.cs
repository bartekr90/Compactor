using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Compactor.Models.Domain
{
    public class EquipmentType
    {
        public EquipmentType()
        {
            Devices = new Collection<Device>();
            ReservationPositions = new Collection<ReservationPosition>();
        }

        public int ID { get; set; }

        [Required]
        [Display(Name = "Nazwa:")]
        public string Name { get; set; }

        public int BorrowedNumber { get; set; }

        public int TotalNumber { get; set; }

        [Display(Name = "Cena za godzinę:")]
        public decimal Price { get; set; }

        public ICollection<Device> Devices { get; set; }

        public ICollection<ReservationPosition> ReservationPositions { get; set; }

    }
}