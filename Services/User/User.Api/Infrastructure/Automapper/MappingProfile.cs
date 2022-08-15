using AutoMapper;
using User.Domain;
using User.Api.Models.v1;
using User.Domain.Entities;

namespace User.Api.Infrastructure.Automapper
{
	public class MappingProfile : Profile
	{
		public MappingProfile()
		{
			CreateMap<CreateUserModel, UserEntity>().ForMember(x => x.Id, opt => opt.Ignore());

			CreateMap<UpdateUserModel, UserEntity>().ForAllMembers(
	opt => opt.Condition((src, dest, sourceMember) => sourceMember != null));
			CreateMap<UserEntity, UserModel>();

		}
	}
}