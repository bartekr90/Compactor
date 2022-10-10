using Compactor.Models;
using Compactor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Compactor.Controllers
{
    public enum UpdateMode
    {
        Add, Subtract
    }

    public class EquipmentTypeRepository
    {
        public List<EquipmentType> GetListOfTypes()
        {
            using (var context = new ApplicationDbContext())
            {
                return context.EquipmentTypes.ToList();
            }
        }

        public void UpdateBorrowedNr(ICollection<ReservationPosition> reservationPositions, UpdateMode mode)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var item in reservationPositions)
                {
                    var typeToUpdate = context.EquipmentTypes
                        .Single(x => x.ID == item.TypeID);

                    if (typeToUpdate.BorrowedNumber <= 0)
                        continue;
                    
                    switch (mode)
                    {
                        case UpdateMode.Add:
                            typeToUpdate.BorrowedNumber += item.RentQuantity;
                            break;
                        case UpdateMode.Subtract:
                            typeToUpdate.BorrowedNumber -= item.RentQuantity;
                            break;                        
                    }

                }
                context.SaveChanges();
            }
        }
    }
}