﻿using Compactor.Models.Domain;
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

        public Reservation GetReservation(string userId, int id)
        {
            using (var context = new ApplicationDbContext())
            {
                return context.Reservations
                    .Include(x => x.ReservationPositions)
                    .Include(x => x.ReservationPositions.Select(y => y.Type))
                    .Single(x => x.UserID == userId && x.ID == id); 
            }
        }

    }
}