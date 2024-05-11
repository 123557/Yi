using Mapster;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.Users;
using Yi.Abp.Application.Contracts.Dtos.User;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Extensions;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Abp.Application.Services
{
    public class UserService:ApplicationService
    {
        
        public UserService(ISqlSugarRepository<UserEntity> UsersqlSugarRepository, ICurrentUser currentUser)
        {
            _currentUser =currentUser;
            _UsersqlSugarRepository = UsersqlSugarRepository;
        }
        public ISqlSugarRepository<UserEntity> _UsersqlSugarRepository { get; set; }
        private ICurrentUser _currentUser { get; set; }
        [HttpGet("CurrentUser")]
        public string GetCurrentUser()
        {
            if(_currentUser.Id is not null)
            {
                string name = _UsersqlSugarRepository._DbQueryable.Where(it => it.Id == _currentUser.Id).First().Name;
                return name;
            }
            //当token鉴权之后，可以直接获取
            return "";
        }
        [HttpGet("UserList")]
        public List<UserListOutput> GetUserList()
        {
            var entities = _UsersqlSugarRepository._DbQueryable.ToList();
            return entities.Adapt<List<UserListOutput>>();

        }
    }
}
