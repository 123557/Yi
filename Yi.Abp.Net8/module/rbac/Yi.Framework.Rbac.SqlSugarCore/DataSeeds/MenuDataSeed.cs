﻿using Volo.Abp.Data;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Guids;
using Yi.Framework.Rbac.Domain.Entities;
using Yi.Framework.Rbac.Domain.Shared.Enums;
using Yi.Framework.SqlSugarCore.Abstractions;

namespace Yi.Framework.Rbac.SqlSugarCore.DataSeeds
{
    public class MenuDataSeed : IDataSeedContributor, ITransientDependency
    {
        private ISqlSugarRepository<MenuEntity> _repository;
        private IGuidGenerator _guidGenerator;
        public MenuDataSeed(ISqlSugarRepository<MenuEntity> repository, IGuidGenerator guidGenerator)
        {
            _repository = repository;
            _guidGenerator = guidGenerator;
        }

        public async Task SeedAsync(DataSeedContext context)
        {
            if (!await _repository.IsAnyAsync(x => x.MenuName == "系统管理"))
            {
                await _repository.InsertManyAsync(GetSeedData());
            }
        }
        public List<MenuEntity> GetSeedData()
        {
            List<MenuEntity> entities = new List<MenuEntity>();

            //系统管理
            MenuEntity system = new MenuEntity(_guidGenerator.Create(), Guid.Empty)
            {
                MenuName = "系统管理",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/system",
                IsShow = true,
                IsLink = false,
                MenuIcon = "system",
                OrderNum = 100,
                IsDeleted = false
            };
            entities.Add(system);

            //代码生成
            MenuEntity code = new MenuEntity(_guidGenerator.Create(), Guid.Empty)
            {
                MenuName = "代码生成",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/code",
                IsShow = true,
                IsLink = false,
                MenuIcon = "build",
                OrderNum = 91,
                IsDeleted = false
            };
            entities.Add(code);

            //数据表管理
            MenuEntity table = new MenuEntity(_guidGenerator.Create(), code.Id)
            {
                MenuName = "数据表管理",
                PermissionCode = "code:table:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "table",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "code/table/index",
                MenuIcon = "online",
                OrderNum = 100,
                IsDeleted = false
            };
            entities.Add(table);

            //字段管理
            MenuEntity field = new MenuEntity(_guidGenerator.Create(), code.Id)
            {
                MenuName = "字段管理",
                PermissionCode = "code:field:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "field",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "code/field/index",
                MenuIcon = "number",
                OrderNum = 99,
                ParentId = code.Id,
                IsDeleted = false
            };
            entities.Add(field);


            //模板管理
            MenuEntity template = new MenuEntity(_guidGenerator.Create(), code.Id)
            {
                MenuName = "模板管理",
                PermissionCode = "code:template:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "template",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "code/template/index",
                MenuIcon = "documentation",
                OrderNum = 98,
                IsDeleted = false
            };
            entities.Add(template);







            //系统监控
            MenuEntity monitoring = new MenuEntity(_guidGenerator.Create())
            {
                MenuName = "系统监控",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/monitor",
                IsShow = true,
                IsLink = false,
                MenuIcon = "monitor",
                OrderNum = 99,
                IsDeleted = false
            };
            entities.Add(monitoring);


            //在线用户
            MenuEntity online = new MenuEntity(_guidGenerator.Create(), monitoring.Id)
            {
                MenuName = "在线用户",
                PermissionCode = "monitor:online:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "online",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/online/index",
                MenuIcon = "online",
                OrderNum = 100,
                IsDeleted = false
            };
            entities.Add(online);

            //缓存列表
            MenuEntity cache = new MenuEntity(_guidGenerator.Create(), monitoring.Id)
            {
                MenuName = "缓存列表",
                PermissionCode = "monitor:cache:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "cacheList",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/cache/list",
                MenuIcon = "redis-list",
                OrderNum = 99,
                IsDeleted = false
            };
            entities.Add(cache);

            //服务监控
            MenuEntity server = new MenuEntity(_guidGenerator.Create(), monitoring.Id)
            {
                MenuName = "服务监控",
                PermissionCode = "monitor:server:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "server",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/server/index",
                MenuIcon = "server",
                OrderNum = 98,
                IsDeleted = false
            };
            entities.Add(server);

            //定时任务
            MenuEntity task = new MenuEntity(_guidGenerator.Create(), monitoring.Id)
            {
                MenuName = "定时任务",
                PermissionCode = "monitor:job:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "job",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/job/index",
                MenuIcon = "job",
                OrderNum = 97,
                IsDeleted = false
            };
            entities.Add(task);


            //系统工具
            MenuEntity tool = new MenuEntity(_guidGenerator.Create())
            {
                MenuName = "系统工具",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/tool",
                IsShow = true,
                IsLink = false,
                MenuIcon = "tool",
                OrderNum = 98,
                IsDeleted = false
            };
            entities.Add(tool);
            //swagger文档
            MenuEntity swagger = new MenuEntity(_guidGenerator.Create(), tool.Id)
            {
                MenuName = "接口文档",
                MenuType = MenuTypeEnum.Menu,
                Router = "http://localhost:19001/swagger",
                IsShow = true,
                IsLink = true,
                MenuIcon = "list",
                OrderNum = 100,
                IsDeleted = false,
            };
            entities.Add(swagger);


            //租户管理
            MenuEntity tenant = new MenuEntity(_guidGenerator.Create())
            {
                MenuName = "租户管理",
                PermissionCode = "system:tenant:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "tenant",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/tenant/index",
                MenuIcon = "list",
                OrderNum = 101,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(tenant);

            MenuEntity tenantQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "租户查询",
                PermissionCode = "system:tenant:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = tenant.Id,
                IsDeleted = false
            };
            entities.Add(tenantQuery);

            MenuEntity tenantAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "租户新增",
                PermissionCode = "system:tenant:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = tenant.Id,
                IsDeleted = false
            };
            entities.Add(tenantAdd);

            MenuEntity tenantEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "租户修改",
                PermissionCode = "system:tenant:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = tenant.Id,
                IsDeleted = false
            };
            entities.Add(tenantEdit);

            MenuEntity tenantRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "租户删除",
                PermissionCode = "system:tenant:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = tenant.Id,
                IsDeleted = false
            };
            entities.Add(tenantRemove);












            //用户管理
            MenuEntity user = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "用户管理",
                PermissionCode = "system:user:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "user",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/user/index",
                MenuIcon = "user",
                OrderNum = 100,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(user);

            MenuEntity userQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "用户查询",
                PermissionCode = "system:user:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userQuery);

            MenuEntity userAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "用户新增",
                PermissionCode = "system:user:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userAdd);

            MenuEntity userEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "用户修改",
                PermissionCode = "system:user:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userEdit);

            MenuEntity userRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "用户删除",
                PermissionCode = "system:user:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = user.Id,
                IsDeleted = false
            };
            entities.Add(userRemove);


            //角色管理
            MenuEntity role = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "角色管理",
                PermissionCode = "system:role:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "role",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/role/index",
                MenuIcon = "peoples",
                OrderNum = 99,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(role);

            MenuEntity roleQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "角色查询",
                PermissionCode = "system:role:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleQuery);

            MenuEntity roleAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "角色新增",
                PermissionCode = "system:role:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleAdd);

            MenuEntity roleEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "角色修改",
                PermissionCode = "system:role:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleEdit);

            MenuEntity roleRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "角色删除",
                PermissionCode = "system:role:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = role.Id,
                IsDeleted = false
            };
            entities.Add(roleRemove);


            //菜单管理
            MenuEntity menu = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "菜单管理",
                PermissionCode = "system:menu:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "menu",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/menu/index",
                MenuIcon = "tree-table",
                OrderNum = 98,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(menu);

            MenuEntity menuQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "菜单查询",
                PermissionCode = "system:menu:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuQuery);

            MenuEntity menuAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "菜单新增",
                PermissionCode = "system:menu:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuAdd);

            MenuEntity menuEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "菜单修改",
                PermissionCode = "system:menu:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuEdit);

            MenuEntity menuRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "菜单删除",
                PermissionCode = "system:menu:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = menu.Id,
                IsDeleted = false
            };
            entities.Add(menuRemove);

            //部门管理
            MenuEntity dept = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "部门管理",
                PermissionCode = "system:dept:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "dept",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/dept/index",
                MenuIcon = "tree",
                OrderNum = 97,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(dept);

            MenuEntity deptQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "部门查询",
                PermissionCode = "system:dept:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptQuery);

            MenuEntity deptAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "部门新增",
                PermissionCode = "system:dept:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptAdd);

            MenuEntity deptEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "部门修改",
                PermissionCode = "system:dept:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptEdit);

            MenuEntity deptRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "部门删除",
                PermissionCode = "system:dept:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dept.Id,
                IsDeleted = false
            };
            entities.Add(deptRemove);



            //岗位管理
            MenuEntity post = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "岗位管理",
                PermissionCode = "system:post:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "post",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/post/index",
                MenuIcon = "post",
                OrderNum = 96,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(post);

            MenuEntity postQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "岗位查询",
                PermissionCode = "system:post:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postQuery);

            MenuEntity postAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "岗位新增",
                PermissionCode = "system:post:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postAdd);

            MenuEntity postEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "岗位修改",
                PermissionCode = "system:post:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postEdit);

            MenuEntity postRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "岗位删除",
                PermissionCode = "system:post:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = post.Id,
                IsDeleted = false
            };
            entities.Add(postRemove);

            //字典管理
            MenuEntity dict = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "字典管理",
                PermissionCode = "system:dict:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "dict",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/dict/index",
                MenuIcon = "dict",
                OrderNum = 95,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(dict);

            MenuEntity dictQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "字典查询",
                PermissionCode = "system:dict:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictQuery);

            MenuEntity dictAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "字典新增",
                PermissionCode = "system:dict:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictAdd);

            MenuEntity dictEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "字典修改",
                PermissionCode = "system:dict:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictEdit);

            MenuEntity dictRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "字典删除",
                PermissionCode = "system:dict:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = dict.Id,
                IsDeleted = false
            };
            entities.Add(dictRemove);


            //参数设置
            MenuEntity config = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "参数设置",
                PermissionCode = "system:config:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "config",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/config/index",
                MenuIcon = "edit",
                OrderNum = 94,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(config);

            MenuEntity configQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "参数查询",
                PermissionCode = "system:config:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configQuery);

            MenuEntity configAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "参数新增",
                PermissionCode = "system:config:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configAdd);

            MenuEntity configEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "参数修改",
                PermissionCode = "system:config:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configEdit);

            MenuEntity configRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "参数删除",
                PermissionCode = "system:config:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = config.Id,
                IsDeleted = false
            };
            entities.Add(configRemove);




            //通知公告
            MenuEntity notice = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "通知公告",
                PermissionCode = "system:notice:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "notice",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "system/notice/index",
                MenuIcon = "message",
                OrderNum = 93,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(notice);

            MenuEntity noticeQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "通知查询",
                PermissionCode = "system:notice:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = notice.Id,
                IsDeleted = false
            };
            entities.Add(noticeQuery);

            MenuEntity noticeAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "通知新增",
                PermissionCode = "system:notice:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = notice.Id,
                IsDeleted = false
            };
            entities.Add(noticeAdd);

            MenuEntity noticeEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "通知修改",
                PermissionCode = "system:notice:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = notice.Id,
                IsDeleted = false
            };
            entities.Add(noticeEdit);

            MenuEntity noticeRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "通知删除",
                PermissionCode = "system:notice:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = notice.Id,
                IsDeleted = false
            };
            entities.Add(noticeRemove);



            //日志管理
            MenuEntity log = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "日志管理",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "log",
                IsShow = true,
                IsLink = false,
                MenuIcon = "log",
                OrderNum = 92,
                ParentId = system.Id,
                IsDeleted = false
            };
            entities.Add(log);

            //操作日志
            MenuEntity operationLog = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "操作日志",
                PermissionCode = "monitor:operlog:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "operlog",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/operlog/index",
                MenuIcon = "form",
                OrderNum = 100,
                ParentId = log.Id,
                IsDeleted = false
            };
            entities.Add(operationLog);

            MenuEntity operationLogQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "操作查询",
                PermissionCode = "monitor:operlog:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = operationLog.Id,
                IsDeleted = false
            };
            entities.Add(operationLogQuery);

            MenuEntity operationLogRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "操作删除",
                PermissionCode = "monitor:operlog:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = operationLog.Id,
                IsDeleted = false
            };
            entities.Add(operationLogRemove);


            //登录日志
            MenuEntity loginLog = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "登录日志",
                PermissionCode = "monitor:logininfor:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "logininfor",
                IsShow = true,
                IsLink = false,
                IsCache = true,
                Component = "monitor/logininfor/index",
                MenuIcon = "logininfor",
                OrderNum = 100,
                ParentId = log.Id,
                IsDeleted = false
            };
            entities.Add(loginLog);

            MenuEntity loginLogQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "登录查询",
                PermissionCode = "monitor:logininfor:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = loginLog.Id,
                IsDeleted = false
            };
            entities.Add(loginLogQuery);

            MenuEntity loginLogRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "登录删除",
                PermissionCode = "monitor:logininfor:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = loginLog.Id,
                IsDeleted = false
            };
            entities.Add(loginLogRemove);

            MenuEntity question = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "问题管理",
                MenuType = MenuTypeEnum.Catalogue,
                Router = "/question",
                IsShow = true,
                IsLink = false,
                MenuIcon = "bug",
                OrderNum = 101,
                IsDeleted = false
            };
            entities.Add(question);

            MenuEntity questionlist = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "问题列表",
                PermissionCode = "question:questiones:list",
                MenuType = MenuTypeEnum.Menu,
                Router = "questiones",
                IsShow = true,
                IsLink = false,
                IsCache = false,
                Component = "question/questiones/index",
                MenuIcon = "list",
                OrderNum = 100,
                ParentId = question.Id,
                IsDeleted = false
            };
            entities.Add(questionlist);

            MenuEntity questionlistQuery = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "问题查询",
                PermissionCode = "question:questiones:query",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = questionlist.Id,
                IsDeleted = false
            };
            entities.Add(questionlistQuery);

            MenuEntity questionlistAdd = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "问题添加",
                PermissionCode = "question:questiones:add",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = questionlist.Id,
                IsDeleted = false
            };
            entities.Add(questionlistAdd);

            MenuEntity questionlistEdit = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "问题修改",
                PermissionCode = "question:questiones:edit",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = questionlist.Id,
                IsDeleted = false
            };
            entities.Add(questionlistEdit);

            MenuEntity questionlistRemove = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "问题删除",
                PermissionCode = "question:questiones:remove",
                MenuType = MenuTypeEnum.Component,
                OrderNum = 100,
                ParentId = questionlist.Id,
                IsDeleted = false
            };
            entities.Add(questionlistRemove);


            MenuEntity dictlist = new MenuEntity(_guidGenerator.Create())
            {

                MenuName = "字段管理",
                MenuType = MenuTypeEnum.Menu,
                Router = "dictionary",
                IsShow = true,
                IsLink = false,
                IsCache = false,
                Component = "system/dict/data",
                MenuIcon = "list",
                OrderNum = 100,
                ParentId = question.Id,
                IsDeleted = false
            };










            entities.Add(dictlist);
            //默认值
            entities.ForEach(m =>
            {
                m.IsDeleted = false;
                m.State = true;
            });

            var p = entities.GroupBy(x => x.Id);
            foreach (var k in p)
            {
                if (k.ToList().Count > 1)
                {
                    Console.WriteLine("菜单id重复");
                }

            }
            return entities;
        }
    }
}
