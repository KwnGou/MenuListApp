using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class Menu_GridDTO
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public DateTimeOffset Date { get; set; }

        public int Plate { get; set; }
        public string? MenuPlateName { get; set; }
        public byte[]? Rowversion { get; set; }
    }
}
