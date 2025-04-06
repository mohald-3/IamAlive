using AutoMapper;
using IamAlive.DTOs.CheckInDtos;
using IamAlive.DTOs.FriendshipDtos;
using IamAlive.DTOs.UserDtos;
using IamAlive.Models;

namespace IamAlive.Mapping
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<User, UserDto>();
            CreateMap<UserCreateDto, User>();


            CreateMap<CheckIn, CheckInDto>();
            CreateMap<CheckInCreateDto, CheckIn>();

            CreateMap<Friendship, FriendshipDto>();
            CreateMap<FriendshipCreateDto, Friendship>();
        }
    }
}
