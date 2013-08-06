using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class WebUI_Account_BU_User : BasePage
{
    protected string FormID;
    protected bool IsEdit = false;
    protected int UserID;
    protected string NodeUser = "administrator";
    DataTable dtUser;
    DataTable dtRole;

    protected void Page_Load(object sender, EventArgs e)
    {
        FormID = Request.QueryString["FormID"];
        if(ViewState["UserID"]!=null)
            UserID = int.Parse(ViewState["UserID"].ToString());
        if (!IsPostBack)
        {
            BindDropDownList();
            
            BindTreeView();
            ViewState["UserID"] = this.TreeView1.SelectedNode.Value;
            ViewState["UserName"] = this.TreeView1.SelectedNode.Text;
            ViewState["UserLevel"] = this.TreeView1.SelectedNode.ToolTip;

            if (this.TreeView1.SelectedNode.Parent != null)
            {
                ViewState["ParentUserID"] = this.TreeView1.SelectedNode.Parent.Value;
                ViewState["ParentUserName"] = this.TreeView1.SelectedNode.Parent.Text;
                ViewState["ParentLevel"] = this.TreeView1.SelectedNode.Parent.ToolTip;
            }
            else
            {
                ViewState["ParentUserID"] = "0";
                ViewState["ParentUserName"] = "";
                ViewState["ParentLevel"] = "0";
            }
            BindGrid();
            this.btnCopyPermission.Enabled = false;
            this.btnCancel.Enabled = false;
            this.btnSave.Enabled = false;
            this.ddlUserLevel.Enabled = false;
            this.ddlSex.Enabled = false;
        }
        if(this.TreeView1.SelectedNode!=null)
            NodeUser = this.TreeView1.SelectedNode.Text;
    }

    #region BindDropDownList
    private void BindDropDownList()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal("BU_Department");
        DataTable dt = dal.GetIDNameList("");
        this.ddlDepartmentID.DataSource = dt;
        this.ddlDepartmentID.DataTextField = "IDName";
        this.ddlDepartmentID.DataValueField = "ID";
        this.ddlDepartmentID.DataBind();
    }
    #endregion

    #region BindTreeView
    //綁定根節點
    public void BindTreeView()
    {
        Js.BLL.BaseDal dal = new Js.BLL.BaseDal(FormID);
        DataTable dt = dal.GetRecord("1=1");
        //選出所有子節點
        DataRow[] drs = dt.Select("ParentLevel=0");    

        //菜單狀態
        TreeView1.Nodes.Clear(); // 清空樹

        foreach (DataRow dr in drs)
        {
            string UserLevel = dr["UserLevel"].ToString();
            string UserName = dr["UserName"].ToString();
            UserID = int.Parse(dr["UserID"].ToString());

            //treeview set
            //this.TreeView1.Font.Name = "新細明體";
            //this.TreeView1.Font.Size = FontUnit.Parse("9");

            //權限控制菜單
            TreeNode rootnode = new TreeNode();
            rootnode.Text = UserName;
            rootnode.Value = UserID.ToString();
            rootnode.ImageUrl = "../../Images/User/persons.gif";
            //rootnode.SelectAction = TreeNodeSelectAction.Expand;
            rootnode.Expanded = true;
            rootnode.Selected = true;
            //rootnode.ImageUrl = imageurl;
            rootnode.ToolTip = UserLevel;

            TreeView1.Nodes.Add(rootnode);

            int sonparentid = int.Parse(UserLevel);
            CreateNode(sonparentid, rootnode, dt);
        }
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "treeview_resize();", true);
    }

    //綁定任意節點
    public void CreateNode(int parentid, TreeNode parentnode, DataTable dt)
    {
        //選出所有子節點
        DataRow[] drs = dt.Select("ParentLevel=" + parentid);
        foreach (DataRow dr in drs)
        {
            string UserLevel = dr["UserLevel"].ToString();
            string UserName = dr["UserName"].ToString();
            UserID = int.Parse(dr["UserID"].ToString());

            TreeNode node = new TreeNode();
            node.Text = UserName;
            node.Value = UserID.ToString();
            node.ToolTip = UserLevel;
            if (int.Parse(UserLevel) % 1000 == 0)
            {
                if (dr["State"].ToString() == "2")
                    node.ImageUrl = "../../Images/User/users.gif";
                else
                    node.ImageUrl = "../../Images/User/persons.gif";
            }
            else
            {
                if (dr["State"].ToString() == "2")
                    node.ImageUrl = "../../Images/User/user.gif";
                else
                    node.ImageUrl = "../../Images/User/person.gif";
            }
            //node.SelectAction = TreeNodeSelectAction.Expand;
            node.Expanded = true;
            int sonparentid = int.Parse(UserLevel);// or =location

            if (parentnode == null)
            {
                TreeView1.Nodes.Clear();
                parentnode = new TreeNode();
                TreeView1.Nodes.Add(parentnode);
            }
            parentnode.ChildNodes.Add(node);
            CreateNode(sonparentid, node, dt);    
        }
    }
    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {        
        ViewState["UserID"] = this.TreeView1.SelectedNode.Value;
        ViewState["UserName"] = this.TreeView1.SelectedNode.Text;
        ViewState["UserLevel"] = this.TreeView1.SelectedNode.ToolTip;
        if (this.TreeView1.SelectedNode.Parent != null)
        {
            ViewState["ParentUserID"] = this.TreeView1.SelectedNode.Parent.Value;
            ViewState["ParentUserName"] = this.TreeView1.SelectedNode.Parent.Text;
        }
        else
        {
            ViewState["ParentUserID"] = "0";
            ViewState["ParentUserName"] = "";
        }
        BindGrid();
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Resize", "treeview_resize();", true);
    }
    #endregion

    #region BindGrid
    private void BindGrid()
    {
        this.GridView1.Columns[12].Visible = true;
        this.GridView1.Columns[13].Visible = true;
        this.GridView1.Columns[14].Visible = true;

        UserID = int.Parse(ViewState["UserID"].ToString());
        string UserName = ViewState["UserName"].ToString();
        int ParentUserID = int.Parse(ViewState["ParentUserID"].ToString());
        string ParentUserName = ViewState["ParentUserName"].ToString();

        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        Js.Model.Account.UsersInfo model = dal.GetModel(UserID);

        this.txtUserID.Text = model.UserID.ToString();
        this.txtUserName.Text = model.UserName;
        this.txtTrueName.Text = model.TrueName;
        this.txtPersonID.Text = model.PersonID;
        this.txtPersonName.Text = model.PersonName;
        this.ddlDepartmentID.SelectedValue = model.DepartmentID;
        if (!model.Sex)
            this.ddlSex.SelectedIndex = 0;
        else
            this.ddlSex.SelectedIndex = 1;
        if (model.State == 0)
            this.txtState.Text = Resources.Resource.User_State0;
        else if (model.State == 1)
            this.txtState.Text = Resources.Resource.User_State1;
        else
            this.txtState.Text = Resources.Resource.User_State2;

        if (model.UserLevel == 1000)
            this.ddlUserLevel.SelectedIndex = 2;
        else if (model.UserLevel % 1000 == 0)
            this.ddlUserLevel.SelectedIndex = 1;
        else
            this.ddlUserLevel.SelectedIndex = 0;

        this.txtEnableDate.Text = Js.Com.PageValidate.ParseDateTime(model.EnableDate.ToString());
        this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(model.StopDate.ToString());

        this.txtEMail.Text = model.Email;
        this.txtPhone.Text = model.Phone;
        this.txtCellPhone.Text = model.CellPhone;
        this.txtCreateDate.Text = Js.Com.PageValidate.ParseDateTime(model.CreateDate.ToString());
        this.txtCreateUserName.Text = model.CreateUserName;
        this.txtLastModifyDate.Text = Js.Com.PageValidate.ParseDateTime(model.LastModifyDate.ToString());
        this.txtLastModifyUserName.Text = model.LastModifyUserName;

        //用戶本身權限
        dtUser = dal.GetUserPermission(UserID, UserName).Tables[0];
        //父級用戶權限
        DataTable dt;
        if (ViewState["ParentUserName"].ToString().ToLower() == "administrator" || ViewState["ParentUserName"].ToString() == "")
            dt = dal.GetPermission(Session["language_session"].ToString()).Tables[0];
        else
            dt = dal.GetUserPermission(ParentUserID, ParentUserName).Tables[0];

        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        this.GridView1.Columns[12].Visible = false;
        this.GridView1.Columns[13].Visible = false;
        this.GridView1.Columns[14].Visible = false;
        if (UserName.ToLower() == "administrator")
        {
            this.btnPermission.Enabled = false;
            this.btnDelete.Enabled = false;
            this.btnRole.Enabled = false;
            this.btnState.Enabled = false;
        }
        else
        {
            this.btnPermission.Enabled = true;
            this.btnDelete.Enabled = true;
            this.btnRole.Enabled = true;
            this.btnState.Enabled = true;
        }
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
            //HiddenField hfd1 = (HiddenField)e.Row.FindControl("HiddenField1");
            //string Permission = hfd1.Value;
            //HiddenField hfd2 = (HiddenField)e.Row.FindControl("HiddenField2");
            //int SysID = int.Parse(hfd2.Value);
            //HiddenField hfd3 = (HiddenField)e.Row.FindControl("HiddenField3");
            //int PermissionID = int.Parse(hfd3.Value);
            string Permission = e.Row.Cells[12].Text;
            int SysID = int.Parse(e.Row.Cells[13].Text);
            int PermissionID = int.Parse(e.Row.Cells[14].Text);

            DataRow[] dr = dtUser.Select(string.Format("SysID={0} and PermissionID={1}", SysID, PermissionID));

            //用戶權限
            string UserPermission = "0000000000";
            bool IsRole = false;
            if (dr.Length > 0)
            {
                UserPermission = dr[0]["Permission"].ToString();
                if(dr[0]["RolePermission"].ToString()=="1")
                    IsRole = true;
            }

            ddl = (DropDownList)(e.Row.FindControl("ddlBrowse"));
            //foreach (ListItem li in ddl.Items)
            //{
            //    li.Attributes.Add("disabled", "disabled");
            //}
            //ddl.Items[2].Attributes.Add("disabled", "disabled");
            

            if (Permission.Substring(0, 1).CompareTo("0")>0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(0, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlDo"));
            if (Permission.Substring(1, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(1, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlAdd"));
            if (Permission.Substring(2, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(2, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlEdit"));
            if (Permission.Substring(3, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(3, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlDelete"));
            if (Permission.Substring(4, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(4, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlPrint"));
            if (Permission.Substring(5, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(5, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlStop"));
            if (Permission.Substring(6, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(6, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlCheck"));
            if (Permission.Substring(7, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(7, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
                    ddl.Enabled = IsEdit;
            }
            else
                ddl.Visible = false;
            ddl = (DropDownList)(e.Row.FindControl("ddlUnCheck"));
            if (Permission.Substring(8, 1).CompareTo("0") > 0)
            {
                ddl.SelectedIndex = int.Parse(UserPermission.Substring(8, 1));
                if (IsRole)
                    ddl.Enabled = false;
                else
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
        UserID = int.Parse(ViewState["UserID"].ToString());
        string UserName = ViewState["UserName"].ToString();
        int ParentUserID = int.Parse(ViewState["ParentUserID"].ToString());
        string ParentUserName = ViewState["ParentUserName"].ToString();

        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        dtRole = dal.GetUserRolePermission(UserID, UserName).Tables[0];

        //父級用戶權限
        DataTable dt;
        if (ViewState["ParentUserName"].ToString().ToLower() == "administrator" || ViewState["ParentUserName"].ToString() == "")
            dt = dal.GetPermission(Session["language_session"].ToString()).Tables[0];
        else
            dt = dal.GetUserPermission(ParentUserID, ParentUserName).Tables[0];

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
            //int SysID = int.Parse(this.GridView1.Rows[i].Cells[13].Text);
            //int PermissionID = int.Parse(this.GridView1.Rows[i].Cells[14].Text);

            //DataRow[] dr = dtRole.Select(string.Format("SysID={0} and PermissionID={1}", SysID, PermissionID));
            //DataRow[] drp = dt.Select(string.Format("SysID={0} and PermissionID={1}", SysID, PermissionID));

            //角色權限
            //string RolePermission = "0000000000";
            //string ParentPermission = "0000000000";
            
            //if (dr.Length > 0)
            //    RolePermission = dr[0]["Permission"].ToString();
            //if (drp.Length > 0)
            //    ParentPermission = drp[0]["Permission"].ToString();

            //Browse
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlBrowse"));
            //if (int.Parse(RolePermission.Substring(0, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //int PIndex = int.Parse(ParentPermission.Substring(0, 1));
            //for(int j=PIndex+1;j<4;j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Do
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlDo"));
            //if (int.Parse(RolePermission.Substring(1, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(1, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Add
            ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlAdd"));
            //if (int.Parse(RolePermission.Substring(2, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(2, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Edit
                ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlEdit"));
            //if (int.Parse(RolePermission.Substring(3, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(3, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Delete
                ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlDelete"));
            //if (int.Parse(RolePermission.Substring(4, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(4, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Print
                ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlPrint"));
            //if (int.Parse(RolePermission.Substring(5, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(5, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Stop
                ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlStop"));
            //if (int.Parse(RolePermission.Substring(6, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(6, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //Check
                ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlCheck"));
            //if (int.Parse(RolePermission.Substring(7, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(7, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");

            //UnCheck
                ddl = (DropDownList)(this.GridView1.Rows[i].FindControl("ddlUnCheck"));
            //if (int.Parse(RolePermission.Substring(8, 1)) > 0)
            //    ddl.Enabled = false;
            //else
                ddl.Enabled = Enable;

            //PIndex = int.Parse(ParentPermission.Substring(8, 1));
            //for (int j = PIndex + 1; j < 4; j++)
            //    ddl.Items[j].Attributes.Add("disabled", "disabled");
        }
    }
    #endregion

    #region Button Event
    protected void btnSave_Click(object sender, EventArgs e)
    {
        UserID = int.Parse(ViewState["UserID"].ToString());
        string UserName = ViewState["UserName"].ToString();

        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        dtRole = dal.GetUserRolePermission(UserID, UserName).Tables[0];

        ArrayList SysID = new ArrayList();
        ArrayList PermissionID = new ArrayList();
        ArrayList Permission = new ArrayList();
        DropDownList ddl;

        for (int i = 0; i < this.GridView1.Rows.Count; i++)
        {
            DataRow[] dr = dtRole.Select(string.Format("SysID={0} and PermissionID={1}", this.GridView1.Rows[i].Cells[13].Text, this.GridView1.Rows[i].Cells[14].Text));

            //角色權限
            string RolePermission = "0000000000";

            if (dr.Length > 0)
                RolePermission = dr[0]["Permission"].ToString();

            string Permissions = "";

            SysID.Add(this.GridView1.Rows[i].Cells[13].Text);
            PermissionID.Add(this.GridView1.Rows[i].Cells[14].Text);
            //Permission += "," + this.GridView1.Rows[i].Cells[13].Text;
            //Permission += "|" + this.GridView1.Rows[i].Cells[14].Text + "|";

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
        dal.AddUserPermission(int.Parse(ViewState["UserID"].ToString()), SysID, PermissionID, Permission);

        
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
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = '';treeview_resize();", true);

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
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = 'none';treeview_resize();", true);
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
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = '';treeview_resize();", true);
        
        BindGrid();
    }
    
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        dal.Delete(int.Parse(ViewState["UserID"].ToString()));
        
        BindTreeView();

        ViewState["UserID"] = this.TreeView1.SelectedNode.Value;
        ViewState["UserName"] = this.TreeView1.SelectedNode.Text;
        ViewState["UserLevel"] = this.TreeView1.SelectedNode.ToolTip;

        if (this.TreeView1.SelectedNode.Parent != null)
        {
            ViewState["ParentUserID"] = this.TreeView1.SelectedNode.Parent.Value;
            ViewState["ParentUserName"] = this.TreeView1.SelectedNode.Parent.Text;
            ViewState["ParentLevel"] = this.TreeView1.SelectedNode.Parent.ToolTip;
        }
        else
        {
            ViewState["ParentUserID"] = "0";
            ViewState["ParentUserName"] = "";
            ViewState["ParentLevel"] = "0";
        }

        BindGrid();
    }
    #endregion

    protected void btnState_Click(object sender, EventArgs e)
    {
        UserID = int.Parse(ViewState["UserID"].ToString());
        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        dal.ChangeUserState(UserID);

        Js.Model.Account.UsersInfo model = dal.GetModel(UserID);
        if (model.State == 0)
            this.txtState.Text = Resources.Resource.User_State0;
        else if (model.State == 1)
            this.txtState.Text = Resources.Resource.User_State1;
        else
            this.txtState.Text = Resources.Resource.User_State2;

        this.txtEnableDate.Text = Js.Com.PageValidate.ParseDateTime(model.EnableDate.ToString());
        this.txtStopDate.Text = Js.Com.PageValidate.ParseDateTime(model.StopDate.ToString());
    }
    protected void btnCopyPermission_Click(object sender, EventArgs e)
    {
        IsEdit = true;
        this.btnAdd.Enabled = !IsEdit;
        this.btnPermission.Enabled = !IsEdit;
        this.btnDelete.Enabled = !IsEdit;
        this.btnCopyPermission.Enabled = IsEdit;
        this.btnCancel.Enabled = IsEdit;
        this.btnSave.Enabled = IsEdit;

        this.GridView1.Columns[12].Visible = true;
        this.GridView1.Columns[13].Visible = true;
        this.GridView1.Columns[14].Visible = true;        

        UserID = int.Parse(this.txtCopyUserID.Text);

        Js.BLL.Account.UserDal dal = new Js.BLL.Account.UserDal();
        DataTable dtp = dal.GetParentUser(int.Parse(this.txtCopyUserID.Text));

        string UserName = "";
        int ParentUserID = 0;
        string ParentUserName = "";
        if (dtp.Rows.Count > 0)
        {
            UserName = dtp.Rows[0]["UserName"].ToString();
            ParentUserID = int.Parse(dtp.Rows[0]["ParentUserID"].ToString());
            ParentUserName = dtp.Rows[0]["ParentUserName"].ToString();
        }
        

        //用戶本身權限
        dtUser = dal.GetUserPermission(UserID, UserName).Tables[0];
        //父級用戶權限
        DataTable dt;
        if (ViewState["ParentUserName"].ToString().ToLower() == "administrator" || ViewState["ParentUserName"].ToString() == "")
            dt = dal.GetPermission(Session["language_session"].ToString()).Tables[0];
        else
            dt = dal.GetUserPermission(ParentUserID, ParentUserName).Tables[0];

        this.GridView1.DataSource = dt;
        this.GridView1.DataBind();
        this.GridView1.Columns[12].Visible = false;
        this.GridView1.Columns[13].Visible = false;
        this.GridView1.Columns[14].Visible = false;
        ScriptManager.RegisterStartupScript(this.updatePanel, this.GetType(), "Display", "document.getElementById('tdTree').style.display = '';treeview_resize();", true);
    }
}