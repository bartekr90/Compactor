using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Compactor.Models.Domain
{
    public class Address
    {
        public Address()
        {
            UserDatas = new Collection<UserData>();
            Users = new Collection<ApplicationUser>();
        }

        public int Id { get; set; }

        [Required]
        [Display(Name = "Ulica")]
        public string Street { get; set; }

        [Required]
        [Display(Name = "Numer")]
        public string Number { get; set; }

        [Required]
        [Display(Name = "Miasto")]
        public string City { get; set; }

        [Required]
        [Display(Name = "Kod pocztowy")]
        public string PostalCode { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }

        public ICollection<UserData> UserDatas { get; set; }
    }
}