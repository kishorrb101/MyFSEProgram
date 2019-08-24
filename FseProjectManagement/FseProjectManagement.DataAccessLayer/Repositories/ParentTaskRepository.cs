using System;
using DataAccess.Repositories.Intefaces;
using FseProjectManagement.DataAccessLayer.Repositories;
using FseProjectManagement.Shared.Models;

namespace DataAccess.Repositories
{
    public class ParentTaskRepository : BaseRepository<ParentTaskDetails>, IParentTaskRepository
    {
       
    }
}
