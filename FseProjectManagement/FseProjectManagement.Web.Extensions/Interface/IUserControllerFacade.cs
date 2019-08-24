using FseProjectManagement.Shared.SearchFilters.FilterCriteria;
using FseProjectManagement.Web.Extensions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FseProjectManagement.Web.Extensions.Interface
{
    public interface IUserControllerFacade
    {
        UserModel Get(int Id);

        List<UserModel> GetAll();

        UserModel Update(UserModel user);

        bool Delete(int id);
        FilterReturnResult<UserModel> Query(FilterConditions filterCondition);
    }
}
