using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class ShoppingListDetails_EditDTO
    {
        [Key]
        public int Id { get; set; }
        public int ShoppingListId { get; set; }
        [Required]
        public int RelatedObjectType { get; set; }
        [Required]
        public int RelatedObjectId { get; set; }
        [MaxLength(100)]
        public string? Remarks { get; set; }
        public byte[]? Rowversion { get; set; }

    }
}
