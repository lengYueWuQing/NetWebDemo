using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Furion.DynamicApiController;
using Furion.FriendlyException;
using Furion.JsonSerialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using SqlSugar;
using WebApplication.Entitys;
using WebApplication.Helper;
using WebApplication.Models;

namespace WebApplication.Controllers
{

    [Route("[controller]")]
    [ApiDescriptionSettings("MyGroup")]
    public class TestDynamicApiController : IDynamicApiController {

        private readonly ILogger<TestDynamicApiController> _logger;
        private readonly ISqlSugarRepository repository; // 仓储对象：封装简单的CRUD
        private readonly SqlSugarClient db; // 核心对象：拥有完整的SqlSugar全部功能
        public TestDynamicApiController(ILogger<TestDynamicApiController> logger, ISqlSugarRepository sqlSugarRepository)
        {
            _logger = logger;
            repository = sqlSugarRepository;
            db = repository.Context;    // 推荐操作
        }

        [HttpPost, HttpGet("get/[action]"), AcceptVerbs("PUT", "DELETE"), NonUnify, AllowAnonymous]
        public string GetVersion()
        {
            var datas = db.Queryable<AIS_MockBuildingInRoute>().WhereIF(true, it => it.BuildingID > 13010 || it.BuildingID == 5119).OrderBy(c => c.BuildingID).ToPagedList(1, 2000);
            int total = datas.TotalCount;
            return total + "ssss" + JSON.Serialize(datas.Items.ToList());
        }

        [HttpGet("[action]"), NonUnify, AllowAnonymous]
        public string GetSql2()
        {
            //原生sql 不转对象 写法
            var datas = db.Ado.GetDataTable("select t1.ID, t1.RouteName, t.BuildingID, 23.5 dou, getDate() da FROM AIS_MockBuildingInRoute t INNER JOIN AIS_MockRoute t1 ON t.MockRouteID = t1.ID");
            JArray ja = DataTableHelper.DataTableConvertToJArray(datas);
            string resss = ja.ToString();

            //结果 转对象 写法
            //var datas1 = db.SqlQueryable<RouteTest>("select t1.ID, t1.RouteName, t.BuildingID, 23.5 dou, getDate() da FROM AIS_MockBuildingInRoute t INNER JOIN AIS_MockRoute t1 ON t.MockRouteID = t1.ID").ToList();
            //string resss = JSON.Serialize(datas1);
            DateTime now = DateTime.Now;
            Test_Start add = new Test_Start
            {
                UpdateTime = now,
                CreateTime = now,
                Content = ((DateTimeOffset)now).ToUnixTimeMilliseconds() + ""
            };
            var result = db.UseTran(() => {
                long id = db.Insertable(add).ExecuteReturnIdentity();    //增加的主键id 是直接方法返回的
                add.ID = id;
                long updateTimeL = ((DateTimeOffset)add.UpdateTime).ToUnixTimeMilliseconds();
                DateTime DateTimeUnix = DateTimeOffset.FromUnixTimeMilliseconds(updateTimeL).UtcDateTime;
                add.UpdateTime = DateTimeUnix;
                db.Updateable(add).ExecuteCommand();

                add.UpdateTime = DateTimeUnix.AddMilliseconds(1);
                db.Updateable(add).ExecuteCommand();

                add.UpdateTime = DateTimeUnix.AddMilliseconds(100);
                db.Updateable(add).IsEnableUpdateVersionValidation().ExecuteCommand();
            });
            if (!result.IsSuccess) {
                throw Oops.Oh(result.ErrorException);
            }

            return result.IsSuccess.ToString();
        }

        [HttpPost, NonUnify]
        [AllowAnonymous]
        //不支持 Content  直接写对象
        public Person GetPerson([FromBody] Person person)
        {
            //对象转json
            string str = JSON.Serialize(person);
            _logger.LogDebug("请求的值：{0}", str);
            var obj = JSON.Deserialize<Person>(str);
            return obj;
        }

    }
}
