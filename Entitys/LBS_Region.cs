using SqlSugar;

namespace WebApplication.Entitys
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class LBS_Region
    {
        /// <summary>
        /// 
        /// </summary>
        public LBS_Region()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        [SugarColumn(IsPrimaryKey = true)]
        public System.Int32 RegionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RegionType { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String RegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 ParentRegionID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.String ParentRegionName { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 RoleID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.DateTime? UpdateDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public System.Int32 IsDelete { get; set; }
    }
}