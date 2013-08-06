using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data.OleDb;
using System.Data;
using System.Drawing;

public partial class WebUI_BusinessUnit_Company : BasePage
{
    protected string FormID;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"] + "";
        if (!IsPostBack)
        {
            BindData();
            
            this.btnEdit.Enabled = true;
            this.btnSave.Enabled = false;
            this.btnCancel.Enabled = false;
            InitialCtl();
        }
    }

    private void BindData()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("1=1");

        this.txtCompanyNo.Text = dt.Rows[0]["CompanyNo"].ToString();
        this.txtCompanyName.Text = dt.Rows[0]["CompanyName"].ToString();
        this.txtUnionID.Text = dt.Rows[0]["UnionID"].ToString();
        this.txtPresident.Text = dt.Rows[0]["President"].ToString();
        this.txtPhone.Text = dt.Rows[0]["Phone"].ToString();
        this.txtFax.Text = dt.Rows[0]["Fax"].ToString();
        this.txtRegisterAddress.Text = dt.Rows[0]["RegisterAddress"].ToString();
        this.txtWebUrl.Text = dt.Rows[0]["WebUrl"].ToString();
        this.txtEnglishAddress.Text = dt.Rows[0]["EnglishAddress"].ToString();
        this.txtEnglishName.Text = dt.Rows[0]["EnglishName"].ToString();
        this.txtCreateUserName.Text = dt.Rows[0]["CreateUserName"].ToString();
        this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["CreateDate"].ToString());
        this.txtLastModifyUserName.Text = dt.Rows[0]["LastModifyUserName"].ToString();
        this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(dt.Rows[0]["LastModifyDate"].ToString());

    }
    private void InitialCtl()
    {        
        this.txtCompanyNo.ReadOnly = this.btnEdit.Enabled;        
        this.txtCompanyName.ReadOnly = this.btnEdit.Enabled;
        this.txtUnionID.ReadOnly = this.btnEdit.Enabled;
        this.txtPresident.ReadOnly = this.btnEdit.Enabled;
        this.txtPhone.ReadOnly = this.btnEdit.Enabled;
        this.txtFax.ReadOnly = this.btnEdit.Enabled;
        this.txtRegisterAddress.ReadOnly = this.btnEdit.Enabled;
        this.txtWebUrl.ReadOnly = this.btnEdit.Enabled;
        this.txtEnglishAddress.ReadOnly = this.btnEdit.Enabled;
        this.txtEnglishName.ReadOnly = this.btnEdit.Enabled;

        this.txtCompanyNo.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtCompanyName.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtUnionID.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtPresident.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtPhone.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtFax.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtRegisterAddress.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtWebUrl.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtEnglishAddress.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
        this.txtEnglishName.BackColor = this.btnEdit.Enabled ? Color.Gainsboro : Color.White;
    }
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        this.btnEdit.Enabled = false;
        this.btnSave.Enabled = true;
        this.btnCancel.Enabled = true;
        InitialCtl();
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "$('#txtRegisterAddress').dblclick(function () { load(this); });", true);
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //BaseDal
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("1=1");
        string CompanyNo = dt.Rows[0]["CompanyNo"].ToString();

        DataRow dr = dt.NewRow();
        dr["CompanyNo"] = this.txtCompanyNo.Text;
        dr["CompanyName"] = this.txtCompanyName.Text;
        dr["UnionID"] = this.txtUnionID.Text.Trim();
        dr["President"] = this.txtPresident.Text.Trim();
        dr["Phone"] = this.txtPhone.Text;
        dr["Fax"] = this.txtFax.Text;
        dr["RegisterAddress"] = this.txtRegisterAddress.Text.Trim();
        dr["WebUrl"] = this.txtWebUrl.Text.Trim();
        dr["EnglishName"] = this.txtEnglishName.Text.Trim();
        dr["EnglishAddress"] = this.txtEnglishAddress.Text.Trim();
        if (this.txtCreateDate.Text.Length>0)
        {
            dr["CreateUserName"] = this.txtCreateUserName.Text;
            dr["CreateDate"] = this.txtCreateDate.Text;
        }
        else
        {
            dr["CreateUserName"] = Session["User"].ToString();
            dr["CreateDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        }
        dr["LastModifyUserName"] = Session["User"].ToString();
        dr["LastModifyDate"] = DateTime.Now.ToString(Js.Com.User.strDateFormat);
        
        if (CompanyNo.Length > 0)
            dal.Update(dr, CompanyNo);
        else
            dal.Add(dr);
        BindData();
        this.btnEdit.Enabled = true;
        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;
        InitialCtl();
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        this.btnEdit.Enabled = true;
        this.btnSave.Enabled = false;
        this.btnCancel.Enabled = false;
        InitialCtl();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        string filePath = Server.MapPath("~/Upload/" + Guid.NewGuid().ToString() + ".xls");
        File.Copy(Server.MapPath("~/Upload/demo.xls"), filePath);

        string strOcn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0;";
        OleDbConnection conn = new OleDbConnection(strOcn);
        using (conn)
        {
            conn.Open();
            // 增加记录
            OleDbCommand cmd = new OleDbCommand("INSERT INTO [Sheet1$]([ID], [姓名], [生日]) VALUES(@Id, @Name, @Birthday)", conn);
            cmd.Parameters.AddWithValue("@Id", "1");
            cmd.Parameters.AddWithValue("@Name", "Hsu Yencheng");
            cmd.Parameters.AddWithValue("@Birthday", "1981-10-13");
            cmd.ExecuteNonQuery();
        }
        // 输出副本的二进制字节流
        Response.ContentType = "application/ms-excel";
        Response.AppendHeader("Content-Disposition", "attachment;filename=info.xls");
        Response.BinaryWrite(File.ReadAllBytes(filePath));
        // 删除副本
        File.Delete(filePath);
    }
    protected void Button2_Click(object sender, EventArgs e)
    {
        //Provider=Microsoft.ACE.OLEDB.12.0;Data Source=info.xlsx;Extended Properties="Excel 12.0 Xml;HDR=YES";
        string filePath = Server.MapPath("~/info.xls");
        OleDbDataAdapter da = new OleDbDataAdapter(
        "SELECT * FROM [Sheet1$]",
        "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + filePath + ";Extended Properties=Excel 8.0");
        DataTable dt = new DataTable();
        da.Fill(dt);
    }
    protected void Button3_Click(object sender, EventArgs e)
    {
        if (this.FileUpload1.HasFile)  
        {  
            //判断文件是否小于10Mb  
            if (this.FileUpload1.PostedFile.ContentLength < 10485760)  
            {  
                try  
                {  
                    //上传文件并指定上传目录的路径  
                    this.FileUpload1.PostedFile.SaveAs(Server.MapPath("~/Upload/")
                        + this.FileUpload1.FileName);  
                    /*注意->这里为什么不是:FileUpLoad1.PostedFile.FileName 
                    * 而是:FileUpLoad1.FileName? 
                    * 前者是获得客户端完整限定(客户端完整路径)名称 
                    * 后者FileUpLoad1.FileName只获得文件名. 
                    */  
  
                    //当然上传语句也可以这样写(貌似废话):  
                    //FileUpLoad1.SaveAs(@"D:\"+FileUpLoad1.FileName);  

                    Label1.Text = "上傳成功!";  
                }  
                catch (Exception ex)  
                {
                    Label1.Text = "出現異常,無法上傳!";  
                    //lblMessage.Text += ex.Message;  
                }  
  
            }  
            else  
            {
                Label1.Text = "上傳文件不能大於10MB!";  
            }  
        }  
        else  
        {
            Label1.Text = "尚未選擇文件!";  
        }  

    }
    protected void Button4_Click(object sender, EventArgs e)
    {
        string uploadName = InputFile.Value;//获取待上传图片的完整路径，包括文件名 
        //string uploadName = InputFile.PostedFile.FileName; 
        string pictureName = "";//上传后的图片名，以当前时间为文件名，确保文件名没有重复 
        if (InputFile.Value != "")
        {
            int idx = uploadName.LastIndexOf(".");
            string suffix = uploadName.Substring(idx);//获得上传的图片的后缀名 
            pictureName = DateTime.Now.Ticks.ToString() + suffix;
        }
        try
        {
            if (uploadName != "")
            {
                string path = Server.MapPath("~/Upload/");
                InputFile.PostedFile.SaveAs(path + pictureName);
            }
        }
        catch (Exception ex)
        {
            Response.Write(ex);
        }
    }
}