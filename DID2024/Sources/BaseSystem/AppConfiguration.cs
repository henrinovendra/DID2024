using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DID

{
    public class Configuration
    {
        #region General Configuration
        public static string ConfigFolder = System.IO.Path.Combine(System.Environment.CurrentDirectory, "wwwroot");
        public static string ConfigDbConnection = "server=127.0.0.1;port=5432;user id=postgres;password=admin;database=did23;";
        //public static string ConfigServerLink = "http://localhost:5000";
        #endregion
    }

    public class CoreProcessing
    {
        public static System.DateTime DefaultDateMinimum = new System.DateTime(2000, 1, 1, 0, 0, 0);
        public static DateTime DateTimeTrimMilliseconds(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second, 0, dt.Kind);
        }
    }

    [System.Serializable]
    public class ConfigurationSetting : System.IDisposable
    {

        #region Constructor-Destructor
        public ConfigurationSetting() { this.DataSettingLoad(); }
        ~ConfigurationSetting() { this.Dispose(); }
        public void Dispose()
        {
            System.GC.Collect();
        }
        #endregion

        #region Data Configuration Init
        public void DataSettingLoad()
        {
            if (!System.IO.Directory.Exists(Configuration.ConfigFolder)) return;
            string vStNFile = System.IO.Path.Combine(Configuration.ConfigFolder, "appsettings.conf");
            System.IO.FileInfo vObFInfo = new System.IO.FileInfo(vStNFile);
            if (vObFInfo.Exists)
            {
                string vStLines;
                string vStIData;
                string vStICode;
                using (System.IO.StreamReader vObReads = new System.IO.StreamReader(vObFInfo.FullName))
                {
                    while ((vStLines = vObReads.ReadLine()) != null)
                    {
                        vStLines = vStLines.Trim();
                        if (vStLines.Length > 3)
                        {
                            vStICode = vStLines.Substring(0, 2);
                            vStIData = vStLines.Substring(3, vStLines.Length - 3).Trim();
                            switch (vStICode)
                            {
                                case "DC": Configuration.ConfigDbConnection = vStIData; break;
                                    //case "SV": Configuration.ConfigServerLink = vStIData; break;
                            }
                        }
                    }
                }
                vStICode = null;
                vStIData = null;
                vStLines = null;
            }
            vObFInfo = null;
            vStNFile = null;
        }
        #endregion

    }
}
