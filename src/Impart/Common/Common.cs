using System;
using System.IO;

namespace Impart
{
    static internal class Common
    {
        static internal string GetAllText(string path) => File.ReadAllText(path);
        static internal void Change(string path, string text)
        {
            File.WriteAllText(path, "");
            using (StreamWriter _streamWriter = new StreamWriter(path))
            {
                _streamWriter.Write(text);
                _streamWriter.Close();
            }
        }
        static internal void Write(string path, string text)
        {
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
        static internal string Str(this string str)
        {
            return str.Replace("<", "&lt;").Replace(">", "&gt;").Replace("&", "&amp;").Replace("\"", "&quot;").Replace("'", "&#39;").Replace("%^", "&#37;^");
        }
    }
}