﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Compactor.Models.Domain
{
    public class Equipment
    {
        public Equipment()
        {
            ReservationPositions = new Collection<ReservationPosition>();
        }
        public int Id { get; set; }

        [Required]
        [Display(Name = "Nazwa")]
        public string Name { get; set; }

        [Required]
        [Display(Name = "Numer seryjny")]
        public string SerialNr { get; set; }
        public bool IsRented { get; set; }

        [Required]
        [ForeignKey("Group")]
        public int IdGroup { get; set; }
        public EquipmentGroup Group { get; set; }
        public ICollection<ReservationPosition> ReservationPositions { get; set; }


    }
}