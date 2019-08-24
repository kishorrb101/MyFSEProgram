using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Models;
using FseProjectManagement.DataAccessLayer.Interfaces;
using FseProjectManagement.DataAccessLayer.Repositories;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared;
using FseProjectManagement.Shared.Helper;

namespace FseProjectManagement.Web.Extensions.Controller
{
    public class TaskControllerFacade : ITaskControllerFacade
    {
        private readonly ITaskDetailsRepository _taskRepository;

        public TaskControllerFacade(ITaskDetailsRepository taskRepository)
        {
            _taskRepository = taskRepository;
        }

        public FilterReturnResult<TaskModel> Query(FilterConditions filterState)
        {
            var filterResult = _taskRepository.Query(filterState);

            if (filterResult != null)
            {
                var tasks = AutoMapper.Mapper.Map<List<TaskModel>>(filterResult.Data);
                return new FilterReturnResult<TaskModel>
                {
                    Total = filterResult.Total,
                    Data = tasks
                };
            }

            return null;
        }

        /// <summary>
        /// get task
        /// </summary>
        /// <param name="id">task id</param>
        /// <returns>task for the given id</returns>
        public TaskModel Get(int id)
        {
            var task = _taskRepository.Get(id);
            if (task == null)
            {
                throw new InvalidOperationException("task does not exists");
            }

            var taskDto = AutoMapper.Mapper.Map<TaskModel>(task);
            return taskDto;
        }

        /// <summary>
        /// delete task
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flag to know if deleted</returns>
        public bool Delete(int id)
        {
            var task = _taskRepository.Get(id);
            if (task == null)
            {
                throw new InvalidOperationException("task does not exists so could not be deleted");
            }

            _taskRepository.Remove(task);
            _taskRepository.SaveChanges();

            return true;
        }

        /// <summary>
        /// Get all tasks
        /// </summary>
        /// <returns>tasks list</returns>
        public List<TaskModel> GetAll()
        {
            var tasks = _taskRepository.GetAll()
                                       .OrderByDescending(p => p.TaskId);

            var taskDtos = AutoMapper.Mapper.Map<List<TaskModel>>(tasks);

            return taskDtos;
        }

        /// <summary>
        /// either create or update provided task
        /// </summary>
        /// <param name="taskModel"></param>
        /// <returns></returns>
        public TaskModel Update(TaskModel taskModel)
        {
            var task = _taskRepository.Get(taskModel.TaskId);
            if (task == null)
            {
                //create task
                task = AutoMapper.Mapper.Map<TaskDetails>(taskModel);
                task.StatusId = (int)TaskStatusEnum.InProgress;
                _taskRepository.Add(task);
            }
            else
            {
                //update task
                task.Name = taskModel.Name;
                task.StartDate = taskModel.StartDate.YYYYMMDDToDate();
                task.EndDate = taskModel.EndDate.YYYYMMDDToDate();
                task.ParentTaskId = taskModel.ParentTaskId;
                task.Priority = taskModel.Priority;
            }
            _taskRepository.SaveChanges();

            return taskModel;
        }

        /// <summary>
        /// update task state
        /// </summary>
        /// <param name="taskId"></param>
        /// <param name="statusId"></param>
        /// <returns></returns>
        public bool UpdateTaskState(int taskId, int statusId)
        {
            var task = _taskRepository.Get(taskId);
            if (task == null)
            {
                throw new InvalidOperationException("Task does not exists");
            }
            else
            {
                if (task.StatusId == statusId) throw new InvalidOperationException("Task state is already up to date");
                //update project
                Enum.TryParse<TaskStatusEnum>(statusId.ToString(), out TaskStatusEnum taskStatusEnum);
                task.StatusId = (int)taskStatusEnum;
            }
            _taskRepository.SaveChanges();
            return true;
        }
    }
}