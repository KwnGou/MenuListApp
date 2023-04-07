using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class ShoppingList_EditDTO
    {
        [Key]
        public int Id { get; set; }
        public DateTimeOffset Date { get; set; }
        public string? Comments { get; set; }
        public byte[]? Rowversion { get; set; }

        public List<ShoppingListDetails_GridDTO> ShoppingListDetails { get; set; }

        public static ShoppingList_EditDTO CreateFromDetailsDTO(ShoppingList_DetailsDTO dto)
        {
            var result = new ShoppingList_EditDTO
            {
                Id = dto.Id,
                Date = dto.Date,
                Comments = dto.Comments,
                Rowversion = dto.Rowversion,
                ShoppingListDetails = new List<ShoppingListDetails_GridDTO>()
            };

            result.ShoppingListDetails.AddRange(dto.ShoppingListDetails);

            return result;
        }
    }
}
