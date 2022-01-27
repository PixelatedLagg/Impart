using System.IO;
using System.Text;

namespace Impart
{
    static internal class File
    {
        static internal void Change(string path, string text)
        {
            System.IO.File.WriteAllText(path, "");
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
                case ".jpg":
                    return true;
                default:
                    return false;
            }
        }
    }
}