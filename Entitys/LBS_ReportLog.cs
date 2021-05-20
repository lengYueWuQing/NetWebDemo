using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_ReportLog
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_ReportLog()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ReportLogID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? StartDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? EndDate { get; set; }
    }
}