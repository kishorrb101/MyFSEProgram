using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.Shared.Helper;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Extensions.Controller
{
    public class ProjectControllerFacade : IProjectControllerFacade
    {
        private readonly IProjectDetailsRepository _projectRepository;

        public ProjectControllerFacade(IProjectDetailsRepository projectRepository)
        {
            _projectRepository = projectRepository;
        }

        public FilterReturnResult<ProjectModel> Query(FilterConditions filterState)
        {
            var filterResult = _projectRepository.Query(filterState);

            if (filterResult != null)
            {
                var projects = AutoMapper.Mapper.Map<List<ProjectModel>>(filterResult.Data);
                return new FilterReturnResult<ProjectModel>
                {
                    Total = filterResult.Total,
                    Data = projects
                };
            }

            return null;
        }

        /// <summary>
        /// get project
        /// </summary>
        /// <param name="id">project id</param>
        /// <returns>project for the given id</returns>
        public ProjectModel Get(int id)
        {
            var project = _projectRepository.Get(id);
            if (project == null)
            {
                throw new InvalidOperationException("user does not exists");
            }

            var projectDto = AutoMapper.Mapper.Map<ProjectModel>(project);
            return projectDto;
        }


        /// <summary>
        /// delete project
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flag to know if deleted</returns>
        public bool Delete(int id)
        {
            var project = _projectRepository.Get(id);
            if (project == null)
            {
                throw new InvalidOperationException("project does not exists");
            }

            if (project.Tasks?.Count>0)
            {
                throw new InvalidOperationException("project has associated tasks so could not be deleted.");
            }

            _projectRepository.Remove(project);
            _projectRepository.SaveChanges();

            return true;
        }


        /// <summary>
        /// Get all projects
        /// </summary>
        /// <returns>projects list</returns>
        public List<ProjectModel> GetAll()
        {
            var projects = _projectRepository.GetAll()
                                             .Where(p=>!p.IsSuspended)
                                             .OrderByDescending(p=>p.ProjectId)
                                             .ToList();

            var projectDtos = AutoMapper.Mapper.Map<List<ProjectModel>>(projects);

            return projectDtos;
        }

        /// <summary>
        /// either create or update provided project
        /// </summary>
        /// <param name="projectModel"></param>
        /// <returns></returns>
        public ProjectModel Update(ProjectModel projectModel)
        {
            var project = _projectRepository.Get(projectModel.ProjectId);
            if (project == null)
            {
                //create project
                project = AutoMapper.Mapper.Map<ProjectDetails>(projectModel);
                _projectRepository.Add(project);
            }
            else
            {
                //update project
                project.Name = projectModel.Name;
                project.StartDate = projectModel.StartDate.YYYYMMDDToDate();
                project.EndDate = projectModel.EndDate.YYYYMMDDToDate();
                project.Priority = projectModel.Priority;
            }
            _projectRepository.SaveChanges();

            return projectModel;
        }

       /// <summary>
       /// update project status
       /// </summary>
       /// <param name="projectId"></param>
       /// <param name="isSuspended"></param>
       /// <returns></returns>
        public bool UpdateProjectState(int projectId, bool isSuspended)
        {
            var project = _projectRepository.Get(projectId);
            if (project == null)
            {
                throw new InvalidOperationException("Project does not exists");
            }
            else
            {
                if (project.IsSuspended == isSuspended) throw new InvalidOperationException($"Project state is already updated");
                //update project
                project.IsSuspended = isSuspended;
            }
            _projectRepository.SaveChanges();
            
            return true;
        }
    }
}
