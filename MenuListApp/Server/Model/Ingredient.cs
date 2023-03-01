using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class Ingredient
    {
        public Ingredient()
        {
            PlateIngredients = new HashSet<PlateIngredient>();
        }

        public int Id { get; set; }
        public int IngredientCategory { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? Rowversion { get; set; }

        public virtual IngredientsCategory IngredientCategoryNavigation { get; set; } = null!;
        public virtual ICollection<PlateIngredient> PlateIngredients { get; set; }
    }
}
