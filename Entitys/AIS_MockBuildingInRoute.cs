using SqlSugar;
using System;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class AIS_MockBuildingInRoute
    {
        /// <summary>
        /// 
        /// </summary>
        public AIS_MockBuildingInRoute()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 MockRouteID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 BuildingID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 DepotID { get; set; }
    }



    [Serializable]
    public class Test_Start : BaseEntity
    {
        /// <summary>
        /// 
        /// </summary>
        public Test_Start()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public long ID { get; set; }
        public string Content { get; set; }
    }
    
}