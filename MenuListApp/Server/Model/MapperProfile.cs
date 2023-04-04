using AutoMapper;
using MenuListApp.Shared.MenuListDTOs;

namespace MenuListApp.Server.Model
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            CreateMap<ItemsCategory, ItemsCategories_GridDTO>()
                .ReverseMap();

            CreateMap<Item, Items_GridDTO>()
                .ForMember(dest => dest.ItemCategoryName, opt => opt.MapFrom(src => src.ItemCategoryNavigation.Name));

            CreateMap<Items_GridDTO, Item>();

            CreateMap<IngredientsCategory, IngredientCategory_GridDTO>()
                .ReverseMap();

            CreateMap<Ingredient, Ingredient_GridDTO>()
                .ForMember(dest => dest.IngredientCategoryName, opt => opt.MapFrom(src => src.IngredientCategoryNavigation.Name));

            CreateMap<Ingredient_GridDTO, Ingredient>();

            CreateMap<PlateCategory, PlateCategory_GridDTO>()
                .ReverseMap();

            CreateMap<Plate, Plate_EditDTO>()
                .ForMember(dest => dest.PlateCategoryName, opt => opt.MapFrom(src => src.PlateCategoryNavigation.Name));

            CreateMap<Plate_EditDTO, Plate>()
                .ForMember(dest => dest.PlateIngredients, opt => opt.MapFrom(src =>
                    src.Ingredients.Select(i => new PlateIngredient { PlateId = i.PlateId, IngredientId = i.IngredientId })));

            CreateMap<Plate, Plate_DetailsDTO>()
                .ForMember(dest => dest.PlateCategoryName, opt => opt.MapFrom(src => src.PlateCategoryNavigation.Name))
                .ForMember(dest => dest.Ingredients, opt => opt.MapFrom(src =>
                    src.PlateIngredients.Select(i => new Ingredient_GridDTO
                    {
                        Id = i.Ingredient.Id,
                        Name = i.Ingredient.Name,
                        IngredientCategory = i.Ingredient.IngredientCategory,
                        IngredientCategoryName = i.Ingredient.IngredientCategoryNavigation.Name
                    })));

            CreateMap<Menu, Menu_GridDTO>()
                .ForMember(dest => dest.MenuPlateName, opt => opt.MapFrom(src => src.PlateNavigation.Name));

            CreateMap<Menu_GridDTO, Menu>();

            CreateMap<ShoppingList, ShoppingList_GridDTO>()
                .ReverseMap();

            CreateMap<ShoppingListDetail, ShoppingListDetails_ListDTO>();
            CreateMap<ShoppingList, ShoppingList_DetailsDTO>();
        }
    }

}
