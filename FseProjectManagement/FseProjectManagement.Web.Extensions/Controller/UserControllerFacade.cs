
using FseProjectManagement.DataAccessLayer.Interfaces;
using FseProjectManagement.Shared.Models;
using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Interface;
using FseProjectManagement.Web.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Web.Extensions.Controller
{
    public class UserControllerFacade: IUserControllerFacade
    {
        private readonly IUserDetailsRepository _userRepository;
        public UserControllerFacade(IUserDetailsRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public FilterReturnResult<UserModel> Query(FilterConditions filterState)
        {
            var filterReturnResult = _userRepository.Query(filterState);

            if (filterReturnResult != null)
            {
                var users = AutoMapper.Mapper.Map<List<UserModel>>(filterReturnResult.Data);
                return new FilterReturnResult<UserModel>
                {
                    Total = filterReturnResult.Total,
                    Data = users
                };
            }

            return null;
        }

        /// <summary>
        /// get user
        /// </summary>
        /// <param name="id">user id</param>
        /// <returns>user for the given id</returns>
        public UserModel Get(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new InvalidOperationException("user does not exists");
            }

            var userModel = AutoMapper.Mapper.Map<UserModel>(user);
            return userModel;
        }


        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns>flag to know if deleted</returns>
        public bool Delete(int id)
        {
            var user = _userRepository.Get(id);
            if (user == null)
            {
                throw new InvalidOperationException("user does not exists so could not be deleted");
            }

            if (user.Projects.Count > 0)
            {
                throw new InvalidOperationException("user has associated projects, so could not be deleted");
            }

            if (user.Tasks.Count > 0)
            {
                throw new InvalidOperationException("user has associated tasks, so could not be deleted");
            }

            _userRepository.Remove(user);
            _userRepository.SaveChanges();

            return true;
        }


        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns>users list</returns>
        public List<UserModel> GetAll()
        {
            var usersList = _userRepository.GetAll()
                                       .OrderByDescending(p => p.UserId);

            var userModels = AutoMapper.Mapper.Map<List<UserModel>>(usersList);

            return userModels;
        }

        /// <summary>
        /// either create or update provided user
        /// </summary>
        /// <param name="UserModel"></param>
        /// <returns></returns>
        public UserModel Update(UserModel UserModel)
        {
            if (string.IsNullOrWhiteSpace(UserModel.FirstName) && string.IsNullOrWhiteSpace(UserModel.LastName))
            {
                throw new InvalidOperationException("Either First Name or Last Name required");
            }
            var userDetails = _userRepository.Get(UserModel.UserId);
            if (userDetails == null)
            {
                //create user
                userDetails = new UserDetails()
                {
                    FirstName = UserModel.FirstName,
                    LastName = UserModel.LastName,
                    EmployeeId = UserModel.EmployeeId
                };
                _userRepository.Add(userDetails);
            }
            else
            {
                //update user
                userDetails.FirstName = UserModel.FirstName;
                userDetails.LastName = UserModel.LastName;
                userDetails.EmployeeId = UserModel.EmployeeId;
            }
            _userRepository.SaveChanges();

            return UserModel;
        }
    }
}
