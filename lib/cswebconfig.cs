using System;
using System.IO;
using System.Reflection;

namespace Csweb
{
    internal static class CSwebconfig
    {
        internal static void Initialize()
        {
            Timer.StartTimer();
            if (Directory.GetFiles(System.Environment.CurrentDirectory, "*.csweb").Length == 0)
            {
                using (StreamWriter writer = new StreamWriter("config.csweb"))
                {
                    writer.Write("<DebugFileWrite>false</DebugFileWrite>");
                    writer.Close();
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader("config.csweb"))
                {
                    string temp = reader.ReadLine().Replace("<DebugFileWrite>", "").Replace("</DebugFileWrite>", "");
                    if (temp.Contains("true") || temp.Contains("false"))
                    {
                        Debug.FileLogging = Convert.ToBoolean(temp);
                    }
                    else
                    {
                        throw new ArgumentException("Value must be \"true\" or \"false\"!");
                    }
                    reader.Close();
                }
            }
            Debug.CallObjectEvent(new Log("[csweb] initialized config file", Timer.GetTime()));
        }
    }
}