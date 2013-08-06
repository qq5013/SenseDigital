using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using FastReport;
using System.Collections.Generic;
using FastReport.Data;
using FastReport.Utils;
using System.Data;

public class Category
{
    public string Name;
    public string Description;
    public List<Product> Products;

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
        Products = new List<Product>();
    }
}

public class Product
{
    public string Name;
    public decimal UnitPrice;

    public Product(string name, decimal unitPrice)
    {
        Name = name;
        UnitPrice = unitPrice;
    }
}

public partial class WebUI_Rpt_frmRptView : System.Web.UI.Page
{
    public void RegisterData(Report FReport)
    {
        DataSet FDataSet = new DataSet();
        FDataSet.ReadXml(Request.PhysicalApplicationPath + "App_Data\\nwind.xml");

        FReport.RegisterData(FDataSet, "NorthWind");

        List<Category> list = new List<Category>();
        Category category = new Category("Beverages", "Soft drinks, coffees, teas, beers");
        category.Products.Add(new Product("Chai", 18m));
        category.Products.Add(new Product("Chang", 19m));
        category.Products.Add(new Product("Ipoh coffee", 46m));
        list.Add(category);

        category = new Category("Confections", "Desserts, candies, and sweet breads");
        category.Products.Add(new Product("Chocolade", 12.75m));
        category.Products.Add(new Product("Scottish Longbreads", 12.5m));
        category.Products.Add(new Product("Tarte au sucre", 49.3m));
        list.Add(category);

        category = new Category("Seafood", "Seaweed and fish");
        category.Products.Add(new Product("Boston Crab Meat", 18.4m));
        category.Products.Add(new Product("Red caviar", 15m));
        list.Add(category);

        FReport.RegisterData(list, "Categories BusinessObject", BOConverterFlags.AllowFields, 3);

    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //WebReport1.Zoom = 1.5f;
    }

    protected void WebReport1_StartReport(object sender, EventArgs e)
    {
        //WebReport1.Report = new FastReport.SimpleList();
        WebReport1.Report = new Report();
        //System.IO.StreamReader dd = new System.IO.StreamReader(@"D:\Untitled.frx");
        //WebReport1.Report.LoadFromString(new System.IO.StreamReader(@"D:\Untitled.frx").ReadToEnd());
        //WebReport1.Report.Load(dd);
        WebReport1.Report.Load(@"D:\Untitled.frx");

        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("LB_Style", "Label");
        WebReport1.Report.RegisterData(dal.GetViewRecord("1=1 and id = 'A0001_A02'"), "LB_Style");
        //WebReport1.Report.Dictionary.Connections[0].ConnectionString = "server=(local);database=SDOPDB;uid=sa;pwd=a@1";
        //WebReport1.Report.Dictionary.p
        //WebReport1.Report.Dictionary.
        //FastReport.Data.ParameterCollection para = WebReport1.Report.Dictionary.Parameters;
        //para[0].Value = "1003";
        //WebReport1.Report.Dictionary.Connections[0].ConnectionString = "Provider=SQLOLEDB.1;Password=a@1;Persist Security Info=True;User ID=sa;Initial Catalog=rptdata01;Data Source=ACER-PC";
        return;
        //RegisterData(WebReport1.Report);
    }
}