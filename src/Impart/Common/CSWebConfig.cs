using System;
using System.IO;

namespace Impart
{
    internal static class CSWebConfig
    {
        internal static void Initialize()
        {
            Timer.StartTimer();
            if (Directory.GetFiles(Environment.CurrentDirectory, "*.csweb").Length == 0)
            {
                using (StreamWriter writer = new StreamWriter("config.csweb"))
                {
                    writer.WriteLine($"<Debug>false</Debug>{Environment.NewLine}<DebugFileWrite>false</DebugFileWrite>{Environment.NewLine}<Formatting>true</Formatting>");
                    writer.Close();
                }
            }
            else
            {
                using (StreamReader r = new StreamReader("config.csweb"))
                {
                    bool debug = false;
					bool debugFileWrite = false;
                    bool formatting = true;
                    if (Boolean.TryParse(r.ReadLine().Replace("<Debug>", "").Replace("</Debug>", ""), out debug) 
                    && Boolean.TryParse(r.ReadLine().Replace("<DebugFileWrite>", "").Replace("</DebugFileWrite>", ""), out debugFileWrite)
                    && Boolean.TryParse(r.ReadLine().Replace("<Formatting>", "").Replace("</Formatting>", ""), out formatting))
                    {
                        Debug.debug = System.Convert.ToBoolean(debug);
                        Debug.FileLogging = System.Convert.ToBoolean(debugFileWrite);
                        Debug.Formatting = formatting;
                    }
                    else
                    {
                        throw new ConfigError("All values must be \"true\" or \"false\"!");
                    }
                    r.Close();
                }
            }
            Debug.CallObjectEvent("[csweb] initialized config file");
        }
    }
}