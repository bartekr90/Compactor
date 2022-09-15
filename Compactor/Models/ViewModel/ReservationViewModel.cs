using Compactor.Models.Domain;
using System.Collections.Generic;

namespace Compactor.Models.ViewModel
{
    public class ReservationViewModel
    {
        public Reservation Reservation { get; set; }
        public ICollection<UserData> UserDataList { get; set; }
    }
}