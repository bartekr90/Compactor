using Compactor.Models.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Compactor.Models.ViewModel
{
    public class ReservationViewModel
    {
        public ReservationViewModel(Reservation reservation, ICollection<UserData> list)
        {
            Reservation = reservation;
            UserDataList = list;
        }
        public Reservation Reservation { get; set; }
        public ICollection<UserData> UserDataList { get; set; }
    }
}