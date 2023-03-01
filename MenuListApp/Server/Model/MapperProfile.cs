using AutoMapper;
using MenuListApp.Shared.MenuListDTOs;

namespace MenuListApp.Server.Model
{
    public class MapperProfile : Profile 
    {
        public  MapperProfile()
        {
            CreateMap<ItemsCategory, ItemsCategories_GridDTO>()
                .ReverseMap();

            CreateMap<Item, Items_GridDTO>()
                .ForMember(dest => dest.ItemCategoryName, opt => opt.MapFrom(src => src.ItemCategoryNavigation.Name));

            CreateMap<Items_GridDTO, Item>();
        }

    }

}
