using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.SqlSugarCore.DataSeeds
{

    public class DeptDataSeed : IDataSeedContributor, ITransientDependency
    {
        private ISqlSugarRepository<DeptEntity> _repository;
        private IGuidGenerator _guidGenerator;
        public DeptDataSeed(ISqlSugarRepository<DeptEntity> repository, IGuidGenerator guidGenerator)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
        }
        public async Task SeedAsync(DataSeedContext context)
        {
            if (!await _repository.IsAnyAsync(x => true))
            {
                await _repository.InsertManyAsync(GetSeedData());
            }
        }
        public List<DeptEntity> GetSeedData()
        {
            var entities = new List<DeptEntity>();

            DeptEntity Dept = new DeptEntity(_guidGenerator.Create())
            {
                DeptName = "本田汽车零部件制造有限公司",
                DeptCode = "CHAM",
                OrderNum = 100,
                IsDeleted = false,
            };
            entities.Add(Dept);


            /*DeptEntity shenzhenDept = new DeptEntity(_guidGenerator.Create())
            {

                DeptName = "深圳总公司",
                OrderNum = 100,
                IsDeleted = false,
                ParentId = chengziDept.Id
            };
            entities.Add(shenzhenDept);*/

            return entities;
        }
    }
}
