using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class ShoppingList_GridDTO
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Comments { get; set; }
        public byte[]? Rowversion { get; set; }

    }
}
