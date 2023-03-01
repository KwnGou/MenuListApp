using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class Item
    {
        public int Id { get; set; }
        public int ItemCategory { get; set; }
        public string Name { get; set; } = null!;
        public byte[]? Rowversion { get; set; }

        public virtual ItemsCategory ItemCategoryNavigation { get; set; } = null!;
    }
}
