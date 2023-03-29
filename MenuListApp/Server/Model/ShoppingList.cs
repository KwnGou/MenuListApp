using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class ShoppingList
    {
        public ShoppingList()
        {
            ShoppingListDetails = new HashSet<ShoppingListDetail>();
        }

        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Comments { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual ICollection<ShoppingListDetail> ShoppingListDetails { get; set; }
    }
}
