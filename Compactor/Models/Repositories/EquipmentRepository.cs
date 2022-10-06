using Compactor.Models;
using Compactor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Compactor.Controllers
{
    public class EquipmentRepository
    {
        public Equipment GetFirstEqWithIdOf(int typeId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Equipments
                    .Include(eq => eq.Type)
                    .FirstOrDefault(eq => eq.TypeID == typeId && eq.IsRented == false);
            }
        }

        public void UpdateStates(ICollection<ReservationPosition> reservationPositions)
        {
            using (var context = new ApplicationDbContext())
            {
                foreach (var item in reservationPositions)
                {
                    var eqToUpdate = context.Equipments
                        .Single(x => x.ID == item.EquipmentID);

                    eqToUpdate.IsRented = item.IsActiv;
                }
                context.SaveChanges();
            }
        }
    }
}