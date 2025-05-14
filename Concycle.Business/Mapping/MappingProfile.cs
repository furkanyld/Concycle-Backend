using AutoMapper;
using Concycle.Core.Dtos;
using Concycle.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concycle.Business.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<Transaction, TransactionDto>().ReverseMap();
            CreateMap<CreateUserDto, User>();

            CreateMap<Post, PostDto>()
                .ForMember(dest => dest.OwnerName, opt => opt.MapFrom(src => src.Owner.Name))
                    .ReverseMap();

            CreateMap<ConRequest, ConRequestDto>()
                .ForMember(dest => dest.PostTitle,
                    opt => opt.MapFrom(src => src.Post != null ? src.Post.Title : null))
                        .ReverseMap();
        }
    }
}
