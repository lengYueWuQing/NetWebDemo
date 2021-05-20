using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_PrcSupervisor
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_PrcSupervisor()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 RPID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ZoneID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 BranchID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 DepotID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 SupervisorID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 RouteID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 BuildingID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String BuildingName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String UnitNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ContractType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ModelType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ItemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ItemName { get; set; }
    }
}