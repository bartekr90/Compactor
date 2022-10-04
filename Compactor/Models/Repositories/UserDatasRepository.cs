using Compactor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Compactor.Models.Repositories
{
    public class UserDatasRepository
    {
        public List<UserData> GetListOfData(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.UserDatas.Include(x => x.Address)
                    .Where(x => x.UserID == userId)
                    .ToList();
            }
        }
    }
}