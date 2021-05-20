using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RouteEx
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RouteEx()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 RouteExID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UnitNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 BuildingID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ContractNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 RouteID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 SupervisorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 DepotID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 BranchID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ZoneID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 EmployeeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 IsChange { get; set; }
    }
}