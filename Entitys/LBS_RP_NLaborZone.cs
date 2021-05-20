using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_NLaborZone
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_NLaborZone()
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
        public System.Decimal? AutoHours { get; set; }
    }
}