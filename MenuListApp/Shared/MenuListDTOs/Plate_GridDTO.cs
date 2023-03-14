﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class Plate_GridDTO
    {
        [Key]
        public int Id { get; set; }
        public int PlateCategory { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? PlateCategoryName { get; set; }
        public byte[]? Rowversion { get; set; }

    }
}