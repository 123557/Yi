using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.SqlSugarCore.DataSeeds
{
    public class PostDataSeed : IDataSeedContributor, ITransientDependency
    {
        private ISqlSugarRepository<PostEntity> _repository;
        public PostDataSeed(ISqlSugarRepository<PostEntity> repository)
        {
            _repository = repository;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (!await _repository.IsAnyAsync(x => true))
            {
                await _repository.InsertManyAsync(GetSeedData());
            }
        }
        public List<PostEntity> GetSeedData()
        {
            var entites = new List<PostEntity>();

            PostEntity Post1 = new PostEntity()
            {

                PostName = "普通员工",
                PostCode = "user",
                OrderNum = 100,
                IsDeleted = false
            };

            entites.Add(Post1);
            return entites;
        }
    }


}
