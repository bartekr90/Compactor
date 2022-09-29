using Compactor.Models.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Compactor.Models.ViewModel
{
    public class InitialViewModel
    {
        public InitialViewModel()
        {
            GroupList = new Collection<EquipmentType>();
        }
        public InitialViewModel(ReservationViewModel vm, ICollection<EquipmentType> list)
        {
            Heading = vm.Reservation.ID == 0 ? "Zarezerwój" : "Rezerwacja";
            GroupList = list;
            ReservationVM = vm;
        }
        public string Heading { get; set; }
        public ReservationViewModel ReservationVM { get; set; }
        public ICollection<EquipmentType> GroupList { get; set; }
    }
}