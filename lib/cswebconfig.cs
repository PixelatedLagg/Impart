namespace Csweb
{
    internal static class CSwebconfig
    {
        internal static void Initialize()
        {
            Timer.StartTimer();
            if (Directory.GetFiles(Environment.CurrentDirectory, "*.csweb").Length == 0)
            {
                using (StreamWriter writer = new StreamWriter("config.csweb"))
                {
                    writer.WriteLine($"<Debug>false</Debug>{Environment.NewLine}<DebugFileWrite>false</DebugFileWrite>");
                    writer.Close();
                }
            }
            else
            {
                using (StreamReader r = new StreamReader("config.csweb"))
                {
                    bool debug = false;
					bool debugFileWrite = false;
                    if (Boolean.TryParse(r.ReadLine().Replace("<Debug>", "").Replace("</Debug>", ""), out debug) && Boolean.TryParse(r.ReadLine().Replace("<DebugFileWrite>", "").Replace("</DebugFileWrite>", ""), out debugFileWrite))
                    {
                        Debug.debug = Convert.ToBoolean(debug);
                        Debug.FileLogging = Convert.ToBoolean(debugFileWrite);
                    }
                    else
                    {
                        throw new ConfigError("Debug and DebugFileWrite values must be \"true\" or \"false\"!");
                    }
                    r.Close();
                }
            }
            Debug.CallObjectEvent("[csweb] initialized config file");
        }
    }

}
