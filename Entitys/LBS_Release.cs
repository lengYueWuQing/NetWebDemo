using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_Release
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_Release()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public System.Int32 ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RemindUpdateVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? ReleaseDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? UpdateOverDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ForceUpdateVersion { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String Desc { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RemindMessage { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ForceMessage { get; set; }
    }
}