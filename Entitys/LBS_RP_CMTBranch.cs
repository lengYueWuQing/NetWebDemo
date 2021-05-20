using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_CMTBranch
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_CMTBranch()
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
        public System.DateTime? CheckDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 UnCompleteCnt2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 UnCompleteCnt3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 UnCompleteCnt4 { get; set; }
    }
}