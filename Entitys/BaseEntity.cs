using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Entitys
{
    public class BaseEntity
    {
        public BaseEntity() { 
        
        }

        [SugarColumn(ColumnName= "CreateTime", ColumnDescription ="创建时间")]
        public DateTime CreateTime { get; set; }
        //IsEnableUpdateVersionValidation 判断 修改
        [SugarColumn(IsEnableUpdateVersionValidation = true, ColumnName = "UpdateTime", ColumnDescription = "更新时间")]
        public DateTime UpdateTime { get; set; }
    }
}
