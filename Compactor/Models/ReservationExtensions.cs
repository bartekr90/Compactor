using Compactor.Models.Domain;
using System;

namespace Compactor.Models
{
    public static class ReservationExtensions
    {
        public static int GetHours(this Reservation reservation)
        {
            var result = reservation.ReturnDate.CompareTo(reservation.RentDate);
            if (result <= 0)
                return result;

            var tSpan = reservation.ReturnDate.Subtract(reservation.RentDate);
            var hours = tSpan.TotalHours;

            if (hours - Math.Truncate(hours) > 0.1)
            {
                hours = Math.Ceiling(hours);
                return Convert.ToInt32(hours);
            }

            hours = Math.Floor(hours);
            return Convert.ToInt32(hours);
        }

        public static bool AssignValue(this Reservation reservation, decimal hours)
        {
            reservation.Value = 0m;

            if (hours <= 0)
                return false;

            foreach (var pos in reservation.ReservationPositions)
            {
                if (pos.Type.Price <= 0)
                    return false;

                reservation.Value += pos.Type.Price * hours;
            }
            return true;
        }

        public static bool PreparePositionsToSave(this Reservation reservation)
        {
            foreach (var position in reservation.ReservationPositions)
            {
                if (position.ID != 0)
                    return false;
                position.Type = null;
                position.Equipment = null;
                position.IsActiv = true;
            }
            return true;
        }
    }
}