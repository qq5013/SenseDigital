using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Configuration;

/// <summary>
/// Summary description for Config
/// </summary>
public class Config
{
	public Config()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void AddKey()
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("/Web");
        AppSettingsSection app = config.AppSettings;
        app.Settings.Add("p", "p:\\");
        config.Save(ConfigurationSaveMode.Modified);
    }
    public static void ModifyKey()
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("/Web");
        AppSettingsSection app = config.AppSettings;
        app.Settings["p"].Value = @"g:\";
        config.Save(ConfigurationSaveMode.Modified);
    }
    public static void DeleteKey()
    {
        Configuration config = WebConfigurationManager.OpenWebConfiguration("/Web");
        AppSettingsSection app = config.AppSettings;
        app.Settings.Remove("p");
        config.Save(ConfigurationSaveMode.Modified);
    }
}