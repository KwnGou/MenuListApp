using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class Plate
    {
        public Plate()
        {
            Menus = new HashSet<Menu>();
            PlateIngredients = new HashSet<PlateIngredient>();
        }

        public int Id { get; set; }
        public int PlateCategory { get; set; }
        public string Name { get; set; } = null!;
        public string? Recipe { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual PlateCategory PlateCategoryNavigation { get; set; } = null!;
        public virtual ICollection<Menu> Menus { get; set; }
        public virtual ICollection<PlateIngredient> PlateIngredients { get; set; }
    }
}
