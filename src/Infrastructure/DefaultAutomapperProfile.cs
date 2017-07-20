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
                    new GetConversationByIdParameters { ConversationId = src.Id })))
                .ForMember(dest => dest.Comments, opt => opt.MapFrom(src => Link.ToCollection(
                    nameof(ConversationsController.GetConversationCommentsByIdAsync),
                    new GetConversationByIdParameters { ConversationId = src.Id })));

            CreateMap<CommentEntity, CommentResource>()
                .ForMember(dest => dest.Self, opt => opt.MapFrom(src => Link.To(
                    nameof(CommentsController.GetCommentByIdAsync),
                    new GetCommentByIdParameters { CommentId = src.Id })))
                .ForMember(dest => dest.Conversation, opt => opt.MapFrom(src => Link.To(
                    nameof(ConversationsController.GetConversationByIdAsync),
                    new GetConversationByIdParameters { ConversationId = src.Conversation.Id })));
        }
    }
}
