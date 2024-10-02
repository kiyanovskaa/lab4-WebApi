using AutoMapper;
using kpz_test4.Models;
using kpz_test4.ViewModel;

namespace kpz_test4
{
    public class Mapping: Profile
    {
        public Mapping()
        {
            CreateMap<Order, OrderViewModel>().
                ForMember(dest => dest.customer_id_view, opt => opt.MapFrom(src => src.customer_id)).
                ForMember(dest => dest.jewelry_id_view, opt => opt.MapFrom(src => src.jewelry_id)).
                ForMember(dest => dest.status_view, opt => opt.MapFrom(src => src.status)).
                ForMember(dest => dest.id_view, opt => opt.MapFrom(src => src.id)).
                ForMember(dest => dest.saler_id_view, opt => opt.MapFrom(src => src.saler_id));

            CreateMap<OrderViewModel, Order>()
            .ForMember(dest => dest.customer_id, opt => opt.MapFrom(src => src.customer_id_view))
            .ForMember(dest => dest.jewelry_id, opt => opt.MapFrom(src => src.jewelry_id_view))
            .ForMember(dest => dest.status, opt => opt.MapFrom(src => src.status_view))
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.id_view))
            .ForMember(dest => dest.saler_id, opt => opt.MapFrom(src => src.saler_id_view));
        }
    }
}
