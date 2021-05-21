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
            var datas = db.Queryable<AIS_MockBuildingInRoute>().WhereIF(true, it => it.BuildingID > 13010 || it.BuildingID == 5119).OrderBy(c => c.BuildingID).ToPagedList(1, 20);
            int total = datas.TotalCount;
            return total + "ssss" + JSON.Serialize(datas.Items.ToList());
        }

        [HttpGet("[action]"), NonUnify, AllowAnonymous]
        public string GetSql2()
        {

            //------------------------------sql查询
            //原生sql 不转对象 写法
            /*var datas = db.Ado.GetDataTable("select t1.ID, t1.RouteName, t.BuildingID, 23.5 dou, getDate() da FROM AIS_MockBuildingInRoute t INNER JOIN AIS_MockRoute t1 ON t.MockRouteID = t1.ID");
            JArray ja = DataTableHelper.DataTableConvertToJArray(datas);
            string resss = ja.ToString();*/

            //结果 转对象 写法
            var datas1 = db.SqlQueryable<RouteTest>("select t1.ID, t1.RouteName, t.BuildingID, 23.5 dou, getDate() da FROM AIS_MockBuildingInRoute t INNER JOIN AIS_MockRoute t1 ON t.MockRouteID = t1.ID").ToList();
            string resss = JSON.Serialize(datas1);



            DateTime now = DateTime.Now;
            Test_Start add = new Test_Start
            {
                UpdateTime = now,
                CreateTime = now,
                Content = ((DateTimeOffset)now).ToUnixTimeMilliseconds() + ""
            };
            var result = db.UseTran(() => {

                //------------------------------sql插入
                /*ExecuteCommand 返回数据库受影响的行数，例如查询返回0，更新0条返回0，更新1条返回1
                ExecuteReturnIdentity   返回自增列 （不支持批量返回自增）
                ExecuteReturnBigIdentity 同上（不支持批量返回自增）
                ExecuteReturnEntity 返回实体
                ExecuteCommandIdentityIntoEntity 给传入实体添加自增列  （不支持批量）*/

                long id = db.Insertable(add).ExecuteReturnIdentity();    //增加的主键id 是直接方法返回的
                add.ID = id;
                long updateTimeL = ((DateTimeOffset)add.UpdateTime).ToUnixTimeMilliseconds();
                DateTime DateTimeUnix = DateTimeOffset.FromUnixTimeMilliseconds(updateTimeL).DateTime;
                add.UpdateTime = DateTimeUnix;

                //------------------------------sql更新
                // 更新：未插入对象的 更新字段方法为  SetColumnsIF
                bool changeFag = db.Updateable<Test_Start>().SetColumnsIF(true, c => new Test_Start { UpdateTime = DateTime.Now})
                .Where(c => c.ID == 4).ExecuteCommandHasChange();

                //更新：插入对象的   更新字段方法为  UpdateColumnsIF
                add.Content = "";
                bool changeFag1 = db.Updateable<Test_Start>(add).UpdateColumnsIF(true, c => new { c.UpdateTime, c.Content })
                .WhereColumns(c=> c.ID).ExecuteCommandHasChange();

                db.Updateable(add).ExecuteCommand();

                add.UpdateTime = DateTimeUnix.AddMilliseconds(1);
                db.Updateable(add).ExecuteCommand();


                //按更新时间判断是否修改过
                /*add.UpdateTime = DateTimeUnix.AddMilliseconds(100);
                db.Updateable(add).IsEnableUpdateVersionValidation().ExecuteCommand();*/


                db.Ado.ExecuteCommand("update Test_Start set Content = @con where ID = 5", new { con = "cdd"});
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
