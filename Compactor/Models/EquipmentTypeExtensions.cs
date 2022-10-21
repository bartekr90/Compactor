using Compactor.Models.Domain;
using System.Collections.Generic;
using System.Linq;

namespace Compactor.Models
{
    public static class EquipmentTypeExtensions
    {
        public static bool IsInStock(this EquipmentType type, ICollection<ReservationPosition> positionsToCheck)
        {
            if (type == null)
                return false;

            int i = 0;

            if (!Utils.IsAny(positionsToCheck))
                return ElementAvailable(type, i);

            var list = positionsToCheck.Where(x => x.TypeID == type.ID);

            foreach (var position in list)
            {
                i += position.RentQuantity;
            }

            return ElementAvailable(type, i);
        }

        private static bool ElementAvailable(EquipmentType type, int i)
        {
            return type.TotalNumber >= type.BorrowedNumber + i;
        }
    }
}