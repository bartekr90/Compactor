using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compactor.Models.Domain
{
    public class UserData
    {
        public UserData()
        {
            Reservations = new Collection<Reservation>();
        }
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }

        [Required]
        [ForeignKey("User")]
        public string IdUser { get; set; }
        public ApplicationUser User { get; set; }

        [ForeignKey("Address")]
        public int IdAddress { get; set; }
        public Address Address { get; set; }

        public ICollection<Reservation> Reservations { get; set; }
       
    }
}