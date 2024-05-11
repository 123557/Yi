using Microsoft.Extensions.Options;
using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Shared.Enums;
using Yi.Framework.Rbac.Domain.Shared.Options;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.SqlSugarCore.DataSeeds
{
    public class UserDataSeed : IDataSeedContributor, ITransientDependency
    {
        private ISqlSugarRepository<UserEntity> _repository;
        private RbacOptions _options;
        public UserDataSeed(ISqlSugarRepository<UserEntity> repository, IOptions<RbacOptions> options)
        {
            _repository = repository;
            _options = options.Value;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (!await _repository.IsAnyAsync(x => true))
            {
                var entities = new List<UserEntity>();
                UserEntity user1 = new UserEntity()
                {
                    Name = "超级管理员",
                    UserName = "admin",
                    Nick = "超级管理员",
                    Password = _options.AdminPassword,
                    Sex = SexEnum.Male,
                    OrderNum = 999,
                    State = true
                };
                user1.BuildPassword();
                entities.Add(user1);

                UserEntity user2 = new UserEntity()
                {

                    Name = "大测试",
                    UserName = "test",
                    Nick = "测试",
                    Password = "123456",
                    OrderNum = 1,
                    State = true

                };
                user2.BuildPassword();
                entities.Add(user2);

                


                await _repository.InsertManyAsync(entities);
            }
        }
    }
}
