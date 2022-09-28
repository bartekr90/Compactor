using Compactor.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Compactor.Models
{
    public static class EquipmentTypeExtensions
    {
        public static bool IsInStock(this EquipmentType data)
        {
            return data.TotalNumber - data.BorrowedNumber > 0;
        }

        public static ReservationPosition ConvertToReservationPos<T>(this EquipmentType eqType, IEnumerable<T> list)
        {
            return new ReservationPosition
            {
                ID = 0,
                RentQuantity = 1,
                EquipmentID = 0,
                TypeID = eqType.ID,
                Type = eqType,
                ReservationID = 0,
                IsActiv = false,
                SequenceNumber = list.Count() + 1
            };
        }
    }
}