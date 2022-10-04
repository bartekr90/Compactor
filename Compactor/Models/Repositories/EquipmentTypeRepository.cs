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
    }
}