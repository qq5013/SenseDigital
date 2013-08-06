using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Account_BU_Role : BasePage
{
    protected string FormID;
    protected bool IsEdit = false;

    DataTable dtRole;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];

        if (!IsPostBack)
        {
            Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
            DataTable dt = dal.GetRecord("1=1");
            BindTreeView(dt);
            ViewState["RoleID"] = this.TreeView1.SelectedNode.Value;
            BindGrid();
            this.btnCopyPermission.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnSave.Enabled = false;
        }
    } 

    #region BindTreeView
    //綁定根節點
    public void BindTreeView(DataTable dt)
    {
        //菜單狀態
        TreeView1.Nodes.Clear(); // 清空樹

        foreach (DataRow dr in dt.Rows)
        {
            string RoleName = dr["RoleName"].ToString();
            int RoleID = int.Parse(dr["RoleID"].ToString());

            //treeview set
            //this.TreeView1.Font.Name = "新細明體";
            //this.TreeView1.Font.Size = FontUnit.Parse("9");

            //權限控制菜單
            TreeNode rootnode = new TreeNode();
            rootnode.Text = RoleName;
            rootnode.Value = RoleID.ToString();
            rootnode.ImageUrl = "../../Images/User/persons.gif";
            //rootnode.SelectAction = TreeNodeSelectAction.Expand;
            rootnode.Expanded = true;
            //rootnode.Selected = true;
            //rootnode.ImageUrl = imageurl;

            TreeView1.Nodes.Add(rootnode);

        }
        if(this.TreeView1.Nodes.Count>0)
            this.TreeView1.Nodes[0].Selected = true;
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "treeview_resize2();", true);
    }
    
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        ViewState["RoleID"] = this.TreeView1.SelectedNode.Value;
        
        BindGrid();
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "treeview_resize2();", true);
    }
    #endregion

    #region BindGrid
    private void BindGrid()
    {
        this.GridView1.Columns[12].Visible = true;
        this.GridView1.Columns[13].Visible = true;
        this.GridView1.Columns[14].Visible = true;

        int RoleID = int.Parse(ViewState["RoleID"].ToString());
        this.HiddenField1.Value = RoleID.ToString();

        Js.BLL.Account.PermissionDal pdal = new Js.BLL.Account.PermissionDal();
        dtRole = pdal.GetRolePermissions(RoleID).Tables[0];

        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        
        //用戶本身權限
        DataTable dt = dal.GetPermission(Session["language_session"].ToString()).Tables[0];


        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        this.GridView1.Columns[12].Visible = false;
        this.GridView1.Columns[13].Visible = false;
        this.GridView1.Columns[14].Visible = false;

        this.btnPermission.Enabled = true;
        this.btnDelete.Enabled = true;

    }
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DropDownList ddl;
        if (e.Row.RowType == DataControlRowType.Header)
        {
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadBrowse"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadDo"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadAdd"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadEdit"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadDelete"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadPrint"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadStop"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadCheck"));
            ddl.Enabled = IsEdit;
            ddl = (DropDownList)(e.Row.FindControl("ddlHeadUnCheck"));
            ddl.Enabled = IsEdit;
        }
        else if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string Permission = e.Row.Cells[12].Text;
            int SysID = int.Parse(e.Row.Cells[13].Text);
            int PermissionID = int.Parse(e.Row.Cells[14].Text);

            DataRow[] dr = dtRole.Select(string.Format("SysID={0} and PermissionID={1}", SysID, PermissionID));

            //用戶權限
            string RolePermission = "0000000000";

            if (dr.Length > 0)
            {
                RolePermission = dr[0]["Permission"].ToString();
            }

            ddl = (DropDownList)(e.Row.FindControl("ddlBrowse"));

            if (Permission.Substring(0, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(0, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlDo"));
            if (Permission.Substring(1, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(1, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlAdd"));
            if (Permission.Substring(2, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(2, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlEdit"));
            if (Permission.Substring(3, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(3, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlDelete"));
            if (Permission.Substring(4, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(4, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlPrint"));
            if (Permission.Substring(5, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(5, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlStop"));
            if (Permission.Substring(6, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(6, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlCheck"));
            if (Permission.Substring(7, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(7, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlUnCheck"));
            if (Permission.Substring(8, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(RolePermission.Substring(8, 1));
                ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
        }
    }
    #endregion

    #region Permission
    private void SetDropDownList(bool Enable)
    {
        if (this.GridView1.Rows.Count <= 0)
            return;

        DropDownList ddl;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadBrowse"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadDo"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadAdd"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadEdit"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadDelete"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadPrint"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadStop"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadCheck"));
        ddl.Enabled = Enable;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadUnCheck"));
        ddl.Enabled = Enable;

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            //Browse
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlBrowse"));
            ddl.Enabled = Enable;
            //Do
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlDo"));
            ddl.Enabled = Enable;
            //Add
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlAdd"));
            ddl.Enabled = Enable;
            //Edit
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlEdit"));
            ddl.Enabled = Enable;
            //Delete
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlDelete"));
            ddl.Enabled = Enable;
            //Print
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlPrint"));
            ddl.Enabled = Enable;
            //Stop
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlStop"));
            ddl.Enabled = Enable;
            //Check
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlCheck"));
            ddl.Enabled = Enable;
            //UnCheck
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlUnCheck"));
            ddl.Enabled = Enable;
        }
    }
    #endregion

    #region Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.RoleDal dal = new Js.BLL.Account.RoleDal();
        ArrayList SysID = new ArrayList();
        ArrayList PermissionID = new ArrayList();
        ArrayList Permission = new ArrayList();
        DropDownList ddl;

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            string Permissions = "";

            SysID.Add(this.GridView1.Rows[i].Cells[13].Text);
            PermissionID.Add(this.GridView1.Rows[i].Cells[14].Text);

            //Browse
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlBrowse"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions = "0";
            //Do
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlDo"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlAdd"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            //Edit
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlEdit"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            //Delete
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlDelete"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            //Print
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlPrint"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            //Stop
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlStop"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            //Check
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlCheck"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";
            //UnCheck
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlUnCheck"));
            if (ddl != null)
            {
                Permissions += ddl.SelectedIndex.ToString();
                ddl.Enabled = false;
            }
            else
                Permissions += "0";

            Permission.Add(Permissions);
        }
        dal.AddRolePermission(int.Parse(ViewState["RoleID"].ToString()), SysID, PermissionID, Permission);

        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadBrowse"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadDo"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadAdd"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadEdit"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadDelete"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadPrint"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadStop"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadCheck"));
        ddl.Enabled = false;
        ddl = (DropDownList)(this.GridView1.HeaderRow.FindControl("ddlHeadUnCheck"));
        ddl.Enabled = false;

        IsEdit = false;
        this.btnAdd.Enabled = !IsEdit;
        this.btnPermission.Enabled = !IsEdit;
        this.btnDelete.Enabled = !IsEdit;
        this.btnCopyPermission.Enabled = IsEdit;
        this.btnCancel.Enabled = IsEdit;
        this.btnSave.Enabled = IsEdit;
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = '';treeview_resize2();", true);

    }
    protected void btnPermission_Click(object sender, EventArgs e)
    {
        IsEdit = true;
        this.btnAdd.Enabled = !IsEdit;
        this.btnPermission.Enabled = !IsEdit;
        this.btnDelete.Enabled = !IsEdit;
        this.btnCopyPermission.Enabled = IsEdit;
        this.btnCancel.Enabled = IsEdit;
        this.btnSave.Enabled = IsEdit;
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = 'none';treeview_resize2();", true);
        SetDropDownList(IsEdit);
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        IsEdit = false;
        this.btnAdd.Enabled = !IsEdit;
        this.btnPermission.Enabled = !IsEdit;
        this.btnDelete.Enabled = !IsEdit;
        this.btnCopyPermission.Enabled = IsEdit;
        this.btnCancel.Enabled = IsEdit;
        this.btnSave.Enabled = IsEdit;
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = '';treeview_resize2();", true);

        BindGrid();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.RoleDal dal = new Js.BLL.Account.RoleDal();
        dal.Delete(int.Parse(ViewState["RoleID"].ToString()));

        Js.BLL.BaseDal bdal = new Js.BLL.BaseDal(FormID);
        DataTable dt = bdal.GetRecord("1=1");
        BindTreeView(dt);

        ViewState["RoleID"] = this.TreeView1.SelectedNode.Value;
        
        BindGrid();
    }
    #endregion
}