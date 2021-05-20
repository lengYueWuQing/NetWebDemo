using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_AdjustmentCA
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_AdjustmentCA()
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
        public System.DateTime? CheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String AdjustmentType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal AdjustmentHours { get; set; }
    }
}