using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class IngredientsCategory
    {
        public IngredientsCategory()
        {
            Ingredients = new HashSet<Ingredient>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? Rowversion { get; set; }

        public virtual ICollection<Ingredient> Ingredients { get; set; }
    }
}
