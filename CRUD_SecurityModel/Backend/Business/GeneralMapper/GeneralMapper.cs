using AutoMapper;
using Entity.DTOs;
using Entity.DTOs.FormModuleDTOs;
using Entity.DTOs.RolFormPermissionDTOs;
using Entity.DTOs.RolUserDTOs;
using Entity.DTOs.UserDTOs;
using Entity.Model;

namespace Business.GeneralMapper
{
    public class GeneralMapper : Profile
    {
        public GeneralMapper()
        {
            CreateMap<Form, FormDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<Module, ModuleDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<Permission,PermissionDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<Person, PersonDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<Rol, RolDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<RolFormPermission, RolFormPermissionDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));

            CreateMap<RolFormPermission, RolFormPermissionOptionsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<FormModule, FormModuleDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));

            CreateMap<FormModule, FormModuleOptionsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<RolUser, RolUserDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Username)) 
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));

            CreateMap<RolUser, RolUserOptionsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));


            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ForMember(dest => dest.Password, opt => opt.MapFrom(src => "🤡"))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));
                

            CreateMap<User, UserOptionsDTO>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Active))
                .ReverseMap()
                .ForMember(dest => dest.Active, opt => opt.MapFrom(src => src.Status));
        }
    }
}
