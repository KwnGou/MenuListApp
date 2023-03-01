using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class Items_GridDTO
    {
        [Key]
        public int Id { get; set; }

        public int ItemCategory { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; } = null!;

        public string ItemCategoryName { get; set; }

        public byte[]? Rowversion { get; set; }
    }
}
