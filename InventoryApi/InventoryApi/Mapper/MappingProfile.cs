using AutoMapper;
using InventoryApi.DTOs;
using InventoryApi.Entities;

namespace InventoryApi.Mapper
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            // Add your object-object mapping configurations here
            // Example:
            // CreateMap<SourceModel, DestinationModel>();

            CreateMap<Product, ProductDto>().ReverseMap();
            CreateMap<Order, OrderDto>().ReverseMap();
            // Exclude navigation properties
            CreateMap<OrderItem, OrderItemDto>().ReverseMap();
            CreateMap<User, UserDto>().ReverseMap();
        }
    }
}
