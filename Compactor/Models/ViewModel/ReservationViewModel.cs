using Compactor.Models.Domain;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Compactor.Models.ViewModel
{
    public class ReservationViewModel
    {
        public ReservationViewModel()
        {
            UserDataList = new Collection<UserData>();
        }
        public ReservationViewModel(Reservation reservation, ICollection<UserData> addressList)
        {
            Reservation = reservation;
            UserDataList = addressList;
        }
        public Reservation Reservation { get; set; }
        public ICollection<UserData> UserDataList { get; set; }
    }
}