using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApplication.Helper
{
    public class FileHelper
    {
        #region 创建文本文件
        /// <summary>
        /// 创建文件
        /// </summary>
        /// <param name="path"></param>
        /// <param name="content"></param>
        public static void CreateFile(string path, string content)
        {
            if (!Directory.Exists(Path.GetDirectoryName(path)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(path));
            }
            using (StreamWriter sw = new StreamWriter(path, false, Encoding.UTF8))
            {
                sw.Write(content);
            }
        }
        #endregion

        #region 上传单个文件

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /*public static TData<string> UploadFile(UploadImages uploadImages, UploadFileType uploadFileType)
        {
            TData<string> returnDta = new TData<string>();

            string year = DateTime.Now.Year.ToString("0000");
            string month = DateTime.Now.Month.ToString("00");
            string day = DateTime.Now.Day.ToString("00");

            var temp = uploadFileType.ParseToString();
            var path = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath,
                "Resource" + Path.DirectorySeparatorChar + temp + Path.DirectorySeparatorChar + year + Path.DirectorySeparatorChar + month + Path.DirectorySeparatorChar + day);

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);  //获取当前项目所在目录

            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            var datetime = Convert.ToInt64(ts.TotalMilliseconds).ToString();
            var suffix = "." + (string.IsNullOrEmpty(uploadImages.ImgType) ? "jpg" : uploadImages.ImgType); //文件的后缀名根据实际情况
            var strPath = path + Path.DirectorySeparatorChar + datetime + suffix;
            byte[] bt = Convert.FromBase64String(uploadImages.ImgPath.Split(',')[1]);

            //写入文件
            File.WriteAllBytes(strPath, bt);
            returnDta.Message = "保存成功。";
            returnDta.Tag = 1;

            //返回路径
            returnDta.Data = @"/Resource/" + temp + @"/" + year + @"/" + month + @"/" + day + @"/" + datetime + suffix;
            return returnDta;
        }*/

        /// <summary>
        /// 上传单个文件
        /// </summary>
        /*public async static Task<TData<string>> UploadFile(int fileModule, IFormFileCollection files)
        {
            string dirModule = string.Empty;
            TData<string> obj = new TData<string>();
            if (files == null || files.Count == 0)
            {
                obj.Message = "请先选择文件！";
                return obj;
            }
            if (files.Count > 1)
            {
                obj.Message = "一次只能上传一个文件！";
                return obj;
            }

            TData objCheck = null;
            IFormFile file = files[0];
            switch (fileModule)
            {
                case (int)UploadFileType.Portrait:
                    objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png");
                    if (objCheck.Tag != 1)
                    {
                        obj.Message = objCheck.Message;
                        return obj;
                    }
                    dirModule = UploadFileType.Portrait.ToString();
                    break;

                //case (int)UploadFileType.News:
                //    if (file.Length > 5 * 1024 * 1024) // 5MB
                //    {
                //        obj.Message = "文件最大限制为 5MB";
                //        return obj;
                //    }
                //    objCheck = CheckFileExtension(Path.GetExtension(file.FileName), ".jpg|.jpeg|.gif|.png");
                //    if (objCheck.Tag != 1)
                //    {
                //        obj.Message = objCheck.Message;
                //        return obj;
                //    }
                //    dirModule = UploadFileType.News.ToString();
                //    break;

                default:
                    obj.Message = "请指定上传到的模块";
                    return obj;
            }
            string fileExtension = TextHelper.GetCustomValue(Path.GetExtension(file.FileName), ".png");

            string newFileName = SecurityHelper.GetGuid() + fileExtension;
            string dir = "Resource" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + DateTime.Now.ToString("yyyy-MM-dd").Replace('-', Path.DirectorySeparatorChar) + Path.DirectorySeparatorChar;

            string absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, dir);
            string absoluteFileName = string.Empty;
            if (!Directory.Exists(absoluteDir))
            {
                Directory.CreateDirectory(absoluteDir);
            }
            absoluteFileName = absoluteDir + newFileName;
            try
            {
                using (FileStream fs = File.Create(absoluteFileName))
                {
                    await file.CopyToAsync(fs);
                    fs.Flush();
                }
                obj.Data = Path.AltDirectorySeparatorChar + ConvertDirectoryToHttp(dir) + newFileName;
                obj.Message = Path.GetFileNameWithoutExtension(TextHelper.GetCustomValue(file.FileName, newFileName));
                obj.Description = (file.Length / 1024).ToString(); // KB
                obj.Tag = 1;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }*/

        #endregion

        #region 删除单个文件
        /// <summary>
        /// 删除单个文件
        /// </summary>
        /// <param name="fileModule"></param>
        /// <param name="filePath"></param>
        /// <returns></returns>
        /*public static TData<string> DeleteFile(int fileModule, string filePath)
        {
            TData<string> obj = new TData<string>();
            string dirModule = fileModule.GetDescriptionByEnum<UploadFileType>();

            if (string.IsNullOrEmpty(filePath))
            {
                obj.Message = "请先选择文件！";
                return obj;
            }
            filePath = "Resource" + Path.DirectorySeparatorChar + dirModule + Path.DirectorySeparatorChar + filePath;
            string absoluteDir = Path.Combine(GlobalContext.HostingEnvironment.ContentRootPath, filePath);
            try
            {
                if (File.Exists(absoluteDir))
                {
                    File.Delete(absoluteDir);
                }
                else
                {
                    obj.Message = "文件不存在";
                }
                obj.Tag = 1;
            }
            catch (Exception ex)
            {
                obj.Message = ex.Message;
            }
            return obj;
        }*/
        #endregion

        #region 下载文件
        /// <summary>
        /// 下载文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="delete"></param>
        /// <returns></returns>
        /*public static TData<FileContentResult> DownloadFile(string filePath, int delete)
        {
            TData<FileContentResult> obj = new TData<FileContentResult>();
            string absoluteFilePath = GlobalContext.HostingEnvironment.ContentRootPath + Path.DirectorySeparatorChar + filePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            byte[] fileBytes = File.ReadAllBytes(absoluteFilePath);
            if (delete == 1)
            {
                File.Delete(absoluteFilePath);
            }
            string fileNamePrefix = DateTime.Now.ToString("yyyyMMddHHmmss");
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(filePath);
            string title = string.Empty;
            if (fileNameWithoutExtension.Contains("_"))
            {
                title = fileNameWithoutExtension.Split('_')[1].Trim();
            }
            string fileExtensionName = Path.GetExtension(filePath);
            obj.Data = new FileContentResult(fileBytes, "application/octet-stream")
            {
                FileDownloadName = string.Format("{0}_{1}{2}", fileNamePrefix, title, fileExtensionName)
            };
            obj.Tag = 1;
            return obj;
        }*/
        #endregion 

        #region GetContentType
        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            var contentType = types[ext];
            if (string.IsNullOrEmpty(contentType))
            {
                contentType = "application/octet-stream";
            }
            return contentType;
        }
        #endregion

        #region GetMimeTypes
        public static Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".pdf", "application/pdf"},
                {".doc", "application/vnd.ms-word"},
                {".docx", "application/vnd.ms-word"},
                {".xls", "application/vnd.ms-excel"},
                {".xlsx", "application/vnd.openxmlformats officedocument.spreadsheetml.sheet"},
                {".png", "image/png"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".gif", "image/gif"},
                {".csv", "text/csv"},
                {".html", "text/html"}
            };
        }
        #endregion

        public static void CreateDirectory(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }
        }

        public static void DeleteDirectory(string filePath)
        {
            if (Directory.Exists(filePath)) //如果存在这个文件夹删除之 
            {
                foreach (string d in Directory.GetFileSystemEntries(filePath))
                {
                    if (File.Exists(d))
                        File.Delete(d); //直接删除其中的文件                        
                    else
                        DeleteDirectory(d); //递归删除子文件夹 
                }
                Directory.Delete(filePath, true); //删除已空文件夹                 
            }
        }


        
        /// <summary>
        /// 获取文件信息
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFileToEnd(string filePath)
        {
            string content = string.Empty;
            if (File.Exists(filePath)) //如果存在这个文件夹删除之 
            {
                using (StreamReader sr = new StreamReader(filePath, EncodingType.GetType(filePath))) //以gb2312编码方式读取
                {
                    content = sr.ReadToEnd();
                }
            }
            return content;
        }



    }


    /// <summary> 
    /// 获取文件的编码格式 
    /// </summary> 
    public class EncodingType
    {

        public static readonly string HTML = "text/html";
        public static readonly string TEXT = "text/plain";
        public static readonly string PDF = "application/pdf";
        public static readonly string JSON = "application/json";
        public static readonly string STEAM = "application/octet-stream";
        public static readonly string FROM = "application/x-www-form-urlencoded";
        public static readonly string WORD = "application/msword";
        public static readonly string XML = "application/xml";
        public static readonly string PNG = "image/png";
        public static readonly string JPG = "image/jpeg";

        /// <summary> 
        /// 给定文件的路径，读取文件的二进制数据，判断文件的编码类型 
        /// </summary> 
        /// <param name=“fileName“>文件路径</param> 
        /// <returns>文件的编码类型</returns> 
        public static System.Text.Encoding GetType(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        /// <summary> 
        /// 通过给定的文件流，判断文件的编码类型 
        /// </summary> 
        /// <param name=“fs“>文件流</param> 
        /// <returns>文件的编码类型</returns> 
        public static System.Text.Encoding GetType(FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //带BOM 
            Encoding reVal = Encoding.Default;

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;

        }

        /// <summary> 
        /// 判断是否是不带 BOM 的 UTF8 格式 
        /// </summary> 
        /// <param name=“data“></param> 
        /// <returns></returns> 
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1; //计算当前正分析的字符应还有的字节数 
            byte curByte; //当前分析的字节. 
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //判断当前 
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //标记位首位若为非0 则至少以2个1开始 如:110XXXXX...........1111110X 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //若是UTF-8 此时第一位必须为1 
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("非预期的byte格式");
            }
            return true;
        }
    }
}
