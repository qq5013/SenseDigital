using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for BaseLanguage
/// </summary>
public class BaseLanguage : System.Web.UI.Page
{
	public BaseLanguage()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    protected void Page_PreInit(object sender, EventArgs e)
    {
        try
        {
            
            

        }
        catch { }
    }   
    /// <summary>
    /// zh-cn/en-us
    /// </summary>
    public string SelectedLanguage
    {
        get
        {
            string RequestLanguage = Request.QueryString["currentculture"];
            string selectedLanguage = string.Empty;
            if (!string.IsNullOrEmpty(RequestLanguage))
            {
                if (RequestLanguage == "zh-cn" || RequestLanguage == "en-us")
                {
                    Session["language_session"] = RequestLanguage;
                    selectedLanguage = RequestLanguage;
                }
                if (RequestLanguage == "zh-TW")
                    Session["language_session"] = null;
            }
            if (Session["language_session"] != null && Convert.ToString(Session["language_session"]) != string.Empty)
            {
                selectedLanguage = Convert.ToString(Session["language_session"]);
            }
            else
            {
                selectedLanguage = Request.Headers["accept-language"].Split(',')[0].ToString().ToLower();
                Session["language_session"] = selectedLanguage;
            }
            return selectedLanguage;
        }
    }

    protected override void InitializeCulture()
    {
        setCulture(SelectedLanguage);
    }
    private void setCulture(string selectedLanguage)
    {
        UICulture = selectedLanguage;
        Culture = selectedLanguage;
        System.Threading.Thread.CurrentThread.CurrentCulture = System.Globalization.CultureInfo.CreateSpecificCulture(selectedLanguage);
        System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(selectedLanguage);
    }
}