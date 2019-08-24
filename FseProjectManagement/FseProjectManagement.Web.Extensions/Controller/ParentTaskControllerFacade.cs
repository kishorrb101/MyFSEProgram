using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Models;
using FseProjectManagement.Shared.Models;

namespace FseProjectManagement.Web.Extensions.Controller
{
    public class ParentTaskControllerFacade : IParentTaskControllerFacade
    {
        private readonly IParentTaskRepository _taskRepository;
        public ParentTaskControllerFacade(IParentTaskRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        /// <summary>
        /// get parent task
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>task for the given id</returns>
        public ParentTaskModel Get(int id)
        {
            var task = _taskRepository.Get(id);
            if (task == null)
            {
                throw new InvalidOperationException("task does not exists");
            }

            var taskDto = AutoMapper.Mapper.Map<ParentTaskModel>(task);
            return taskDto;
        }


        /// <summary>
        /// Get all parent tasks
        /// </summary>
        /// <returns>tasks list</returns>
        public List<ParentTaskModel> GetAll()
        {
            var parentTasks = _taskRepository.GetAll()
                        .OrderByDescending(p => p.ParentTaskId);
            var taskDtos = AutoMapper.Mapper.Map<List<ParentTaskModel>>(parentTasks);

            return taskDtos;
        }

        /// <summary>
        /// either create or update provided task
        /// </summary>
        /// <param name="parentTaskDto"></param>
        /// <returns></returns>
        public ParentTaskModel Update(ParentTaskModel parentTaskDto)
        {
            var task = _taskRepository.Get(parentTaskDto.ParentTaskId);
            if (task == null)
            {
                //create Task
                task = AutoMapper.Mapper.Map<ParentTaskDetails>(parentTaskDto);
                _taskRepository.Add(task);
            }
            else
            {
                //update task
                task.Name = parentTaskDto.Name;
            }
            _taskRepository.SaveChanges();

            return parentTaskDto;
        }
    }
}
