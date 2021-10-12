using System;
using System.IO;

namespace Csweb
{
    static internal class Common
    {
        static internal void Delete(string path) => File.WriteAllText(path, "");
        static internal string GetAllText(string path) => File.ReadAllText(path);
        static internal void Change(string path, string text)
        {
            Delete(path);
            using (StreamWriter _streamWriter = new StreamWriter(path))
            {
                _streamWriter.Write(text);
                _streamWriter.Close();
            }
        }
        static internal bool ValidPath(string path, string message)
        {
            try
            {
                using (StreamWriter _streamWriter = new StreamWriter(path)) 
                {
                    if (message != null)
                    {
                        _streamWriter.Write(message);
                    }
                    _streamWriter.Close();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
        static internal bool IsImage(string path)
        {
            switch (Path.GetExtension(path))
            {
                case ".apng":
                case ".avif":
                case ".gif":
                case ".jpeg":
                case ".png":
                case ".svg":
                case ".webp":
                    return true;
                default:
                    return false;
            }
        }
    }
}