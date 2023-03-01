using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class PlateIngredient
    {
        public int Id { get; set; }
        public int PlateId { get; set; }
        public int IngredientId { get; set; }

        public virtual Ingredient Ingredient { get; set; } = null!;
        public virtual Plate Plate { get; set; } = null!;
    }
}
