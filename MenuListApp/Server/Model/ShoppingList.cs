using System;
using System.Collections.Generic;

namespace MenuListApp.Server.Model
{
    public partial class ShoppingList
    {
        public int Id { get; set; }
        public int RelatedObjectType { get; set; }
        public int RelatedObjectId { get; set; }
        public string? Remarks { get; set; }
        public byte[]? Rowversion { get; set; }
    }
}
