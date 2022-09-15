using Compactor.Models.Domain;
using System.Collections.Generic;

namespace Compactor.Models.ViewModel
{
    public class InitialViewModel
    {
        public string Heading { get; set; }
        public ReservationViewModel ReservationVM { get; set; }
        public ICollection<EquipmentType> GroupList { get; set; }
        public ICollection<ReservationPosition> PositionList { get; set; }
    }
}