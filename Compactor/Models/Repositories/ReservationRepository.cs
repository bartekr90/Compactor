using Compactor.Models.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace Compactor.Models.Repositories
{
    public class ReservationRepository
    {
        public List<Reservation> GetListofReservations(string userId)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Reservations.Where(x => x.UserID == userId).ToList();
            }
        }

        public void Add(Reservation reservation)
        {
            using (var context = new ApplicationDbContext())
            {
                context.Reservations.Add(reservation);
                context.SaveChanges();
            }
        }

        public int GetIdOfEmptyRes(string userID)
        {
            using (var context = new ApplicationDbContext())
            {
                //var reservations = context.Reservations
                //    .Include(x => x.ReservationPositions)
                //    .Where(x => x.UserID == userID)
                //    .ToList();

                //var reservation = reservations
                //    .FirstOrDefault(x => Utils.IsAny(x.ReservationPositions));

                var reservation = context.Reservations
                    .Include(x => x.ReservationPositions)
                    .Where(x => x.UserID == userID && Utils.IsAny(x.ReservationPositions))
                    .Single();

                return reservation.ID;
            }
        }

        public void AddPositionsToRes(ICollection<ReservationPosition> reservationPositions, string userID)
        {
            using (var context = new ApplicationDbContext())
            {                
                var reservation = context.Reservations.Single(x =>
                       x.ID == reservationPositions.FirstOrDefault().ReservationID &&
                       x.UserID == userID);

                foreach (var item in reservationPositions)
                {
                    context.ReservationPositions.Add(item);
                }
                context.SaveChanges();
            }
        }
    }
}