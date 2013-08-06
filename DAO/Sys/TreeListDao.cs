using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace Js.DAO.Sys
{
    public class TreeListDao
    {
        DBAccessLayer.IDBAccess ia;

        public TreeListDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public TreeListDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Sys.TreeListInfo GetModel(string FormID)
        {
            string strSql = string.Format("select * from Sys_TreeList Where FormID = '{0}'", FormID);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.Sys.TreeListInfo model = new Js.Model.Sys.TreeListInfo();
            if (dt.Rows.Count != 0)
            {
                model.FormID = dt.Rows[0]["FormID"].ToString();
                model.SysID = byte.Parse(dt.Rows[0]["SysID"].ToString());
                model.NodeID = int.Parse(dt.Rows[0]["NodeID"].ToString());
                model.Text = dt.Rows[0]["Text"].ToString();
                model.Text_cn = dt.Rows[0]["Text_cn"].ToString();
                model.Text_en = dt.Rows[0]["Text_en"].ToString();
                model.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                model.Location = dt.Rows[0]["Location"].ToString();
                model.Url = dt.Rows[0]["Url"].ToString();
                model.PermissionID = int.Parse(dt.Rows[0]["PermissionID"].ToString());
                model.ImageUrl = dt.Rows[0]["ImageUrl"].ToString();
                model.Precondition = dt.Rows[0]["Precondition"].ToString();
                model.PrintPrefix = dt.Rows[0]["PrintPrefix"].ToString();
                model.IsUsedView = dt.Rows[0]["IsUsedView"].ToString();
                model.ShowVersion = dt.Rows[0]["ShowVersion"].ToString();
                model.TableName = dt.Rows[0]["TableName"].ToString();
                model.KeyField = dt.Rows[0]["KeyField"].ToString();
                model.ViewName = dt.Rows[0]["ViewName"].ToString();
                model.OrderField = dt.Rows[0]["OrderField"].ToString();
                model.BaseName = dt.Rows[0]["BaseName"].ToString();
                model.SelectSQL = dt.Rows[0]["SelectSQL"].ToString();
                model.strWhere = dt.Rows[0]["strWhere"].ToString();
                model.MainSubTbl = dt.Rows[0]["MainSubTbl"].ToString();
                model.Visible = bool.Parse(dt.Rows[0]["Visible"].ToString());
                model.AutoCodeName = dt.Rows[0]["AutoCodeName"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 得到sys_TreeList數據實體
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public Js.Model.Sys.TreeListInfo GetModel(int SId,int PId)
        {
            string strSql = string.Format("select * from Sys_TreeList Where SysID = {0} and PermissionID={1}", SId, PId);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            Js.Model.Sys.TreeListInfo model = new Js.Model.Sys.TreeListInfo();
            if (dt.Rows.Count != 0)
            {
                model.FormID = dt.Rows[0]["FormID"].ToString();
                model.SysID = byte.Parse(dt.Rows[0]["SysID"].ToString());
                model.NodeID = int.Parse(dt.Rows[0]["NodeID"].ToString());
                model.Text = dt.Rows[0]["Text"].ToString();
                model.Text_cn = dt.Rows[0]["Text_cn"].ToString();
                model.Text_en = dt.Rows[0]["Text_en"].ToString();
                model.ParentID = int.Parse(dt.Rows[0]["ParentID"].ToString());
                model.Location = dt.Rows[0]["Location"].ToString();
                model.Url = dt.Rows[0]["Url"].ToString();
                model.PermissionID = int.Parse(dt.Rows[0]["PermissionID"].ToString());
                model.ImageUrl = dt.Rows[0]["ImageUrl"].ToString();
                model.Precondition = dt.Rows[0]["Precondition"].ToString();
                model.PrintPrefix = dt.Rows[0]["PrintPrefix"].ToString();
                model.IsUsedView = dt.Rows[0]["IsUsedView"].ToString();
                model.ShowVersion = dt.Rows[0]["ShowVersion"].ToString();
                model.TableName = dt.Rows[0]["TableName"].ToString();
                model.KeyField = dt.Rows[0]["KeyField"].ToString();
                model.ViewName = dt.Rows[0]["ViewName"].ToString();
                model.OrderField = dt.Rows[0]["OrderField"].ToString();
                model.BaseName = dt.Rows[0]["BaseName"].ToString();
                model.SelectSQL = dt.Rows[0]["SelectSQL"].ToString();
                model.strWhere = dt.Rows[0]["strWhere"].ToString();
                model.MainSubTbl = dt.Rows[0]["MainSubTbl"].ToString();
                model.Visible = bool.Parse(dt.Rows[0]["Visible"].ToString());
                model.AutoCodeName = dt.Rows[0]["AutoCodeName"].ToString();
            }
            return model;
        }
        /// <summary>
        /// 得到LeftBar數據
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetLeftBar()
        {
            string strSql = "SELECT FormID, SysID, NodeID, cast(SysID as varchar) + '_' + cast(NodeID as varchar) as ID,Text, ParentID, Url, PermissionID, ImageUrl," +
                            "(SELECT Text FROM Sys_TreeList AS B WHERE B.SysID=A.SysID and B.NodeID = A.ParentID) AS ParentTitle," +
                            "(SELECT ImageUrl FROM Sys_TreeList AS B WHERE B.SysID=A.SysID and B.NodeID = A.ParentID) AS ParentImageUrl " +
                            "FROM Sys_TreeList AS A WHERE ParentID > 0";

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 得到LeftBar數據
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetLeftBar(string filter)
        {
            string strSql = string.Format("SELECT FormID, SysID, NodeID, cast(SysID as varchar) + '_' + cast(NodeID as varchar) as ID,Text, ParentID, Url, PermissionID, ImageUrl," +
                            "(SELECT Text FROM Sys_TreeList AS B WHERE B.SysID=A.SysID and B.NodeID = A.ParentID) AS ParentTitle," +
                            "(SELECT ImageUrl FROM Sys_TreeList AS B WHERE B.SysID=A.SysID and B.NodeID = A.ParentID) AS ParentImageUrl " +
                            "FROM Sys_TreeList AS A WHERE ParentID > 0 And {0}", filter);

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }
        /// <summary>
        /// 得到LeftBar數據
        /// </summary>
        /// <param name="row">row</param>
        /// <returns>com_departmentInfo</returns>
        public DataTable GetLeftBar(string filter, string culture)
        {
            string strSql = string.Format("SELECT FormID, SysID, NodeID, cast(SysID as varchar) + '_' + cast(NodeID as varchar) as ID,Text, ParentID, Url, PermissionID, ImageUrl," +
                            "(SELECT Text FROM Sys_TreeList AS B WHERE B.SysID=A.SysID and B.NodeID = A.ParentID) AS ParentTitle," +
                            "(SELECT ImageUrl FROM Sys_TreeList AS B WHERE B.SysID=A.SysID and B.NodeID = A.ParentID) AS ParentImageUrl " +
                            "FROM Sys_TreeList AS A WHERE ParentID > 0 And {0}", filter);

            if(culture == "zh-cn")
                strSql = strSql.Replace("Text","Text_cn as Text");
            else if(culture == "en-us")
                strSql = strSql.Replace("Text","Text_en as Text");

            DataTable dt = ia.ExecuteDataSetSql(strSql).Tables[0];
            return dt;
        }

    }
}
