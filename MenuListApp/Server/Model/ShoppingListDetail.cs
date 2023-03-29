using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class ShoppingListDetail
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public int RelatedObjectType { get; set; }
        public int RelatedObjectId { get; set; }
        public string? Remarks { get; set; }
        public byte[]? Rowversion { get; set; }

        public virtual ShoppingList ShoppingList { get; set; } = null!;
    }
}
