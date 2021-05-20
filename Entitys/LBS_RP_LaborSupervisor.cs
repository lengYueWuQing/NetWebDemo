using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_LaborSupervisor
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_LaborSupervisor()
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
        public System.Int32 EmployeeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String FirstName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Maintain1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Maintain2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Maintain3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Maintain4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Road1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Road2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Road3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Road4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal AdjustmentHours { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal BaseHours { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CheckSequence { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CheckYear { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? AutoHours { get; set; }
    }
}