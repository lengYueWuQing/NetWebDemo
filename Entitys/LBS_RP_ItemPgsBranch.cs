using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_ItemPgsBranch
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_ItemPgsBranch()
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
        public System.Int32 CardTypeID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ItemCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ItemName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? CheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 PlanCnt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 CompleteCnt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 UnCompleteCnt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Decimal Rate { get; set; }
    }
}