using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Compactor.Models.Domain
{
    public class Reservation
    {       
        public Reservation()
        {
            ReservationPositions = new Collection<ReservationPosition>();
        }
        public Reservation(string userID, ICollection<ReservationPosition> list)
        {
            ID = 0;
            UserID = userID;
            Value = 0;
            Title = GetTitle(DateTime.Now);
            RentDate = DateTime.Now;
            ReturnDate = new DateTime();
            ReservationPositions = list;
        }

        public int ID { get; set; }

        [Required(ErrorMessage = "Pole tytuł jest wymagane!")]
        [Display(Name = "Tytuł:")]
        public string Title { get; set; }

        [Display(Name = "Uwagi:")]
        public string Comments { get; set; }

        [Display(Name = "Wartość:")]
        public decimal Value { get; set; }

        [Display(Name = "Aktywna:")]
        public bool IsActiv { get; set; }

        [Required(ErrorMessage = "Pole data wypożycznenia jest wymagane!")]
        [Display(Name = "Data wypożycznenia:")]
        public DateTime RentDate { get; set; }

        [Required(ErrorMessage = "Pole data zwrotu jest wymagane!")]
        [Display(Name = "Data zwrotu:")]
        public DateTime ReturnDate { get; set; }

        [Required]
        [ForeignKey("User")]
        public string UserID { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [ForeignKey("UserData")]
        public int UserDataID { get; set; }
        public UserData UserData { get; set; }

        public ICollection<ReservationPosition> ReservationPositions { get; set; }

        private static string GetTitle(DateTime now)
        {
            var data = now.ToString().ToCharArray();

            StringBuilder sb = new StringBuilder();
            sb.Append(new char[] { 'R', '_' });
            sb.Append(data, 0, 6);
            sb.Append(data, 8, 8);

            sb.Replace('.', '/');
            sb.Replace(' ', '_');
            sb.Replace(':', '-');

            return sb.ToString();
        }
    }
}