using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class ShoppingListDetails_ListDTO
    {
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        public int RelatedObjectType { get; set; }
        public string ObjectTypeName { get; set; }
        public int RelatedObjectId { get; set; }
        public string ObjectName { get; set; }
        public string? Remarks { get; set; }
        public byte[]? Rowversion { get; set; }

    }
}
