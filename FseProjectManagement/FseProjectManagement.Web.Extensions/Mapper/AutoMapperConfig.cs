
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Web.Extensions.Models;
using FseProjectManagement.Shared.Helper;
using System.Linq;
using FseProjectManagement.Shared;

namespace FseProjectManagement.Web.Extensions.Mapper
{
    public static class AutoMapperConfig
    {
        public static void Initialize()
        {
            //AutoMapper.Mapper.Initialize((config) =>
            //{
            //    config.CreateMap<UserDetails, UserModel>();

            //    config.CreateMap<TaskModel, TaskDetails>()
            //   .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
            //   .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));
            //});

            AutoMapper.Mapper.Initialize((config) =>
            {
                config.CreateMap<UserDetails, UserModel>();
                config.CreateMap<UserModel, UserDetails>();

                config.CreateMap<ProjectDetails, ProjectModel>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()))
                .ForMember(x => x.ManagerDisplayName, opt => opt.MapFrom(x => x.Manager.FirstName))
                .ForMember(x => x.TotalTasks, opt => opt.MapFrom(x => x.Tasks.Count))
                .ForMember(x => x.TotalCompletedTasks, opt => opt.MapFrom(x => x.Tasks.Where(t => t.StatusId == (int)TaskStatusEnum.Completed).Count()));

                config.CreateMap<ProjectModel, ProjectDetails>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));

                config.CreateMap<TaskDetails, TaskModel>()
                .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.DateToYYYYMMDD()))
                .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.DateToYYYYMMDD()))
                .ForMember(x => x.ParentTaskName, opt => opt.MapFrom(x => x.ParentTask.Name))
                .ForMember(x => x.OwnerName, opt => opt.MapFrom(x => string.Join(" ", x.Owner.FirstName, x.Owner.LastName)))
                .ForMember(x => x.ProjectName, opt => opt.MapFrom(x => x.Project.Name));

                config.CreateMap<TaskModel, TaskDetails>()
               .ForMember(x => x.StartDate, opt => opt.MapFrom(x => x.StartDate.YYYYMMDDToDate()))
               .ForMember(x => x.EndDate, opt => opt.MapFrom(x => x.EndDate.YYYYMMDDToDate()));

                config.CreateMap<ParentTaskDetails, ParentTaskModel>();
                config.CreateMap<ParentTaskModel, ParentTaskDetails>();
            });
        }
    }
}
