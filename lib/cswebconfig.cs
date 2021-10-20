using System;
using System.IO;

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
                    writer.Write("<Debug>false</Debug>");
                    writer.Write("<DebugFileWrite>false</DebugFileWrite>");
                    writer.Close();
                }
            }
            else
            {
                using (StreamReader reader = new StreamReader("config.csweb"))
                {
                    string debug = reader.ReadLine().Replace("<Debug>", "").Replace("</Debug>", "");
                    string debugFileWrite = reader.ReadLine().Replace("<DebugFileWrite>", "").Replace("</DebugFileWrite>", "");
                    if ((debugFileWrite.Contains("true") || debugFileWrite.Contains("false")) && (debug.Contains("true") || debug.Contains("false")))
                    {
                        Debug.debug = Convert.ToBoolean(debug);
                        Debug.FileLogging = Convert.ToBoolean(debugFileWrite);
                    }
                    else
                    {
                        throw new ConfigError("Debug and DebugFileWrite values must be \"true\" or \"false\"!");
                    }
                    reader.Close();
                }
            }
            Debug.CallObjectEvent("[csweb] initialized config file");
        }
    }
}