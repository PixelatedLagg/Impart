using System;
using System.IO;
using System.Collections.Specialized;
using System.Configuration;
using System.Reflection;
using System.Linq;

namespace Impart
{
    internal static class ImpartConfig
    {
        internal static int CommonInitialization;
        internal static void Initialize()
        {
            Timer.StartTimer();
            CommonInitialization++;
            if (Directory.GetFiles(Environment.CurrentDirectory, "*.config").Length == 0)
            {
                using (StreamWriter writer = new StreamWriter("app.config"))
                {
                    writer.WriteLine(($"<?xml version=\"1.0\" encoding=\"utf-8\" ?>%<configuration>%    " + 
                    "<appSettings>%        <add key=\"Debug\" value=\"false\" />%        <add key=\"DebugFile\" value=\"false\" />" + 
                    "%        <add key=\"Formatting\" value=\"true\" />%    </appSettings>%</configuration>").Replace("%", Environment.NewLine));
                    writer.Close();
                }
            }
            else 
            {
                try
                {
                    Debug.debug = Convert.ToBoolean(ConfigurationManager.AppSettings["Debug"]);
                    Debug.FileLogging = Convert.ToBoolean(ConfigurationManager.AppSettings["DebugFile"]);
                    Debug.Formatting = Convert.ToBoolean(ConfigurationManager.AppSettings["Formatting"]);
                }
                catch
                {
                    throw new ConfigError("All key values must be \"true\" or \"false\"!");
                }
            }
            string namespaceName;
            try 
            {
                namespaceName = Assembly.GetExecutingAssembly().GetTypes().Select(t => t.Namespace).Where(t => !t.Contains("Impart")).Distinct().FirstOrDefault();
            }
            catch 
            {
                Debug.CallObjectEvent("[impart] finished initialization");
                return;
            }
            if (Type.GetType($"{namespaceName}.Program")?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance) != null)
            {
                Debug.ObjectEvent += (Action<Log>)Delegate.CreateDelegate(typeof(Action<Log>), null, Type.GetType($"{namespaceName}.Program")?.GetMethod("Event", BindingFlags.NonPublic | BindingFlags.Instance));
            }
            Debug.CallObjectEvent("[impart] finished initialization");
        }
    }
}