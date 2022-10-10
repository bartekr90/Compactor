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

        public UserData PrepareFirstEntity(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                var user = context.Users.Single(x => x.Id == userId);
               
                var data = new UserData
                {
                    ID = 0,
                    AddressID = user.AddressID,
                    Email = user.Email,
                    UserID = user.Id,
                    Name = user.Name
                };
                Add(data);
                return data;
            }
        }

        public void Add(UserData data)
        {
            using (var context = new ApplicationDbContext())
            {
                context.UserDatas.Add(data);
                context.SaveChanges();
            }
        }
    }
}