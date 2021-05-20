using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_NLaborSupervisor
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_NLaborSupervisor()
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
        public System.String CustomerInfo { get; set; }

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
        public System.Decimal CB1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CB2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CB3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CB4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CBTT1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CBTT2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CBTT3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal CBTT4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCB1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCB2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCB3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCB4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCBTT1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCBTT2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCBTT3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal OTCBTT4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUI1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUI2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUI3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUI4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUITT1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUITT2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUITT3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal PUITT4 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal? AutoHours { get; set; }
    }
}