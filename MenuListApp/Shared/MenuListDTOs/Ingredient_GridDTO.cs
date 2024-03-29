﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class Ingredient_GridDTO
    {
        [Key]
        public int Id { get; set; }

        public int IngredientCategory { get; set; }

        public string? IngredientCategoryName { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; } = null!;

        public byte[]? Rowversion { get; set; }

        public override string ToString()
        {
            return $"{Name} ({IngredientCategoryName})";
        }
    }
}
