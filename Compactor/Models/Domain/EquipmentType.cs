﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;

namespace Compactor.Models.Domain
{
    public class EquipmentType
    {
        public EquipmentType()
        {
            Equipments = new Collection<Equipment>();
            ReservationPositions = new Collection<ReservationPosition>();
        }
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        public int BorrowedNumber { get; set; }
        public int TotalNumber { get; set; }
        public decimal Price { get; set; }
        public ICollection<Equipment> Equipments { get; set; }
        public ICollection<ReservationPosition> ReservationPositions { get; set; }
    }
}