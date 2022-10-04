using Compactor.Models;
using Compactor.Models.Domain;
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
    }
}