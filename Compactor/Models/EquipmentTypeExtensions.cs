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
                goto End;

            var list = positionsToCheck.Where(x => x.TypeID == type.ID);

            foreach (var position in list)
            {
                i += position.RentQuantity;
            }

            End:
            return type.TotalNumber >= type.BorrowedNumber+i;
        }               
    }
}