using AutoMapper;
using BeautifulRestApi.Controllers;
using BeautifulRestApi.Models;

namespace BeautifulRestApi.Infrastructure
{
    public class DefaultAutomapperProfile : Profile
    {
        public DefaultAutomapperProfile()
        {
            CreateMap<ConversationEntity, ConversationResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(
                    nameof(ConversationsController.GetConversationByIdAsync),
                    new GetConversationByIdOptions { Id = src.Id })));
        }
    }
}
