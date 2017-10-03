using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;

namespace KOS.App_Code
{
    public class ClearTemp
    {
        public const string _folder = "Temp";
        const string _ext = "xls";
        const int _oldHours = 12;

        public string Path { get; set; }

        public ClearTemp(HttpRequest request)
        {
            Path = request.PhysicalApplicationPath + _folder + "\\";
        }

        List<FileInfo> EnumXls()
        {
            DirectoryInfo info = new DirectoryInfo(Path);
            FileInfo[] files = info.GetFiles();
            if (files.Length < 1) return null;

            List<FileInfo> list = new List<FileInfo>();
            foreach (FileInfo file in files)
                if (file.Extension.ToLower() == _ext)
                    list.Add(file);
            return list;
        }

        public void DeleteOld()
        {
            List<FileInfo> list = EnumXls();
            foreach (FileInfo fi in list)
                if ((DateTime.Now - fi.CreationTime).TotalHours > _oldHours)
                    try
                    {
                        fi.Delete();
                    }
                    catch { }
        }
    }
}