using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Helper
{
    public class DataTableHelper
    {

        /// <summary>
        /// DataTable 转 JArray
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static JArray DataTableConvertToJArray(DataTable dt)
        {
            // 定义集合
            JArray jss = new JArray();
            //定义一个临时变量  
            string tempName = string.Empty;
            //遍历DataTable中所有的数据行  
            foreach (DataRow dr in dt.Rows)
            {
                foreach (DataColumn dataColumn in dt.Columns)
                {
                    JObject js = new JObject();
                    object value = dr[dataColumn.ColumnName];
                    if (value == null)
                    {
                        js.Add(dataColumn.ColumnName, null);
                        jss.Add(js);
                        continue;
                    }
                    else if (string.Empty.Equals(dr[dataColumn.ColumnName]))
                    {
                        js.Add(dataColumn.ColumnName, string.Empty);
                        jss.Add(js);
                        continue;
                    }
                    switch (dataColumn.DataType.Name)
                    {
                        case "Int32":
                            js.Add(dataColumn.ColumnName, Int32.Parse(value.ToString()));
                            jss.Add(js);
                            break;
                        case "Int64":
                            js.Add(dataColumn.ColumnName, Int64.Parse(value.ToString()));
                            jss.Add(js);
                            break;
                        case "Double":
                            js.Add(dataColumn.ColumnName, Double.Parse(value.ToString()));
                            jss.Add(js);
                            break;
                        case "Decimal":
                            js.Add(dataColumn.ColumnName, Decimal.Parse(value.ToString()));
                            jss.Add(js);
                            break;
                        case "DateTime":
                            js.Add(dataColumn.ColumnName, DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd HH:mm:ss"));
                            jss.Add(js);
                            break;
                        case "short":
                            js.Add(dataColumn.ColumnName, short.Parse(value.ToString()));
                            jss.Add(js);
                            break;
                        default:
                            js.Add(dataColumn.ColumnName, value.ToString());
                            jss.Add(js);
                            break;
                    }
                }
            }
            return jss;
        }
    }
}
