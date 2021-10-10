using System;
using System.IO;

namespace Csweb
{
    static internal class Common
    {
        //deletes all file contents
        static internal void Delete(string path) => File.WriteAllText(path, "");
        //called everytime a new set of components is rendered
        static internal void Change(string path, string text)
        {
            Delete(path);
            using (StreamWriter _streamWriter = new StreamWriter(path))
            {
                _streamWriter.Write(text);
                _streamWriter.Close();
            }
        }
        //checks file path by relying the streamwriter class throwing an error
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
        static internal bool IsImage(string extension)
        {
            switch (extension)
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
            //.apng .avif .gif .jpeg .png .svg .webp"
        }
    }
}