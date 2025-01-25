using AutoMapper;
using Microsoft.AspNetCore.Identity;
using SocialMedia.Domain.DTOs;
using SocialMedia.Domain.Models;
using SocialMedia.Domain.Requests;

namespace SocialMedia.Domain.Mappers
{
    public class MapperProfile : Profile
    {
        public MapperProfile()
        {
            AuthMapper();
            SocialMediaMapper();
        }

        private void AuthMapper()
        {
        }

        private void SocialMediaMapper()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.Type, opt => opt.MapFrom(src => src.PostType.ToString()));

            CreateMap<UserComment, CommentDTO>();

            CreateMap<UserReaction, ReactionDTO>()
                .ForMember(dest => dest.ReactionType, opt => opt.MapFrom(src => src.ReactionType.ToString()));

        }
    }
}
