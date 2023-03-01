using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class ItemsCategories_GridDTO
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(25)]
        public string Name { get; set; } = null!;
        public byte[]? Rowversion { get; set; }

    }
}
