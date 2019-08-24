
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Web.Extensions.Models;
using FseProjectManagement.Shared.Helper;
using System.Linq;

namespace FseProjectManagement.Web.Extensions.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            AutoMapper.Mapper.Initialize((config) =>
            {
                config.CreateMap<UserDetails, UserModel>();

                config.CreateMap<TaskModel, TaskDetails>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));
            });
        }
    }
}