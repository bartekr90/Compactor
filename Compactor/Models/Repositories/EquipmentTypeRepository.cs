using Compactor.Models;
using Compactor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Compactor.Controllers
{
    public class EquipmentTypeRepository
    {
        public List<EquipmentType> GetListOfTypes()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EquipmentTypes.ToList();
            }
        }

        public void UpdateBorrowedNr(ICollection<ReservationPosition> reservationPositions)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var item in reservationPositions)
                {
                    var typeToUpdate = context.EquipmentTypes
                        .Single(x => x.ID == item.TypeID);

                    typeToUpdate.BorrowedNumber = item.RentQuantity;
                }
                context.SaveChanges();
            }
        }
    }
}