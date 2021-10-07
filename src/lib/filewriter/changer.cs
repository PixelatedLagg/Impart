using System.IO;
using System;

namespace Csweb
{
    static internal class Changer
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
    }
}