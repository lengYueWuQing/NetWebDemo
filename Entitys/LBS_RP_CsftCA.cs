using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_RP_CsftCA
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_RP_CsftCA()
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
        public System.Int32 TotalType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 SftCnt1 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 SftCnt2 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 SftCnt3 { get; set; }
    }
}