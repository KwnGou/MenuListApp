using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuListApp.Shared.MenuListDTOs
{
    public class Plate_EditDTO
    {
        [Key]
        public int Id { get; set; }
        public int PlateCategory { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
        public string? Recipe { get; set; }
        public string? PlateCategoryName { get; set; }
        public byte[]? Rowversion { get; set; }

        public List<PlateIngredient_GridDTO> Ingredients { get; set; }

        public static Plate_EditDTO CreateFromDetailsDTO(Plate_DetailsDTO dto)
        {
            var editDto = new Plate_EditDTO
            {
                Id = dto.Id,
                PlateCategory = dto.PlateCategory,
                Name = dto.Name,
                Recipe = dto.Recipe,
                Rowversion = dto.Rowversion,
                Ingredients = dto.Ingredients.Select(i => new PlateIngredient_GridDTO { PlateId = dto.Id, IngredientId = i.Id }).ToList()
            };
            return editDto;
        }

    }

}
