using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class ShoppingList_DetailsDTO
    {
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Comments { get; set; }
        public byte[]? Rowversion { get; set; }

        public List<ShoppingListDetails_ListDTO> ShoppingListDetails { get; set; }

        public ShoppingList_DetailsDTO() => ShoppingListDetails = new List<ShoppingListDetails_ListDTO>();
    }
}
