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
    }
}