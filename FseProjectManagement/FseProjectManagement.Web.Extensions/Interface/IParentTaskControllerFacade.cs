using System;
using System.Collections.Generic;
using System.Linq;
using FseProjectManagement.Web.Extensions.Models;

namespace FseProjectManagement.Web.Extensions.Interface
{
    public interface IParentTaskControllerFacade
    {
        ParentTaskModel Get(int Id);

        List<ParentTaskModel> GetAll();

        ParentTaskModel Update(ParentTaskModel task);
    }
}
