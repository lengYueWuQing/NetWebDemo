using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_MorSupervisor
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_MorSupervisor()
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
        public System.String EmployeeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 DateInterval { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastCheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? LastItemDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? NextCheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? NextItemDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ScheduleID { get; set; }
    }
}