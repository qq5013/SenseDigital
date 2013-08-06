using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Js.Model;
using System.Data;
using System.Data.SqlClient;

namespace Js.DAO.Sys
{
    public class SysManageDao
    {
        DBAccessLayer.IDBAccess ia;

        public SysManageDao()
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, "");
        }
        public SysManageDao(string cnKey)
        {
            ia = DBAccessLayer.DBFactory.GetDBAccess(DBAccessLayer.DBAccessType.SQL, cnKey);
        }
        public int GetMaxID()
        {
            string strsql = "select max(NodeID)+1 from Sys_TreeList";
            object obj = ia.ExecuteScalarSql(strsql);
            if (obj == null)
            {
                return 1;
            }
            else
            {
                return int.Parse(obj.ToString());
            }
        }
		public int AddTreeNode(SysNode node)
		{
            node.NodeID = GetMaxID();

            string strSql = string.Format("insert into Sys_TreeList(SysID,NodeID,Text,ParentID,Location,OrderID,comment,Url,PermissionID,ImageUrl) values " +
                            "({0},{1},'{2}',{3},'{4}',{5},'{6}','{7}',{8},'{9}')", node.SysID, node.NodeID, node.Text, node.ParentID, node.Location, node.OrderID, node.Comment, node.Url, node.PermissionID, node.ImageUrl);
            ia.ExecuteNonQuerySql(strSql);
			return node.NodeID;	
		}
		
		public void UpdateNode(SysNode node)
		{
            string strSql = string.Format("update Sys_TreeList set Text='{0}',ParentID={1},Location='{2}',OrderID={3},comment='{4}',Url='{5}',PermissionID={6},ImageUrl={7} " +
                            "Where SysID={8} And NodeID={9}",node.Text, node.ParentID, node.Location, node.OrderID, node.Comment, node.Url, node.PermissionID, node.ImageUrl,node.SysID, node.NodeID);
            ia.ExecuteNonQuerySql(strSql);			
		}

        public void DelTreeNode(int SysID, int NodeID)
		{
            string strSql = string.Format("delete Sys_TreeList where SysID={0} And NodeID={1}",SysID, NodeID);
            ia.ExecuteNonQuerySql(strSql);
        }
	
		/// <summary>
		/// 得到菜單節點
		/// </summary>
		/// <param name="NodeID"></param>
		/// <returns></returns>
		public SysNode GetNode(int SysID,int NodeID)
		{
            SysNode node = new SysNode();
            string strSql = string.Format("select * from Sys_TreeList where SysID={0} And NodeID={1}", SysID, NodeID);
            DataSet ds = ia.ExecuteDataSetSql(strSql);
			
			if(ds.Tables[0].Rows.Count>0)
			{
                node.SysID = int.Parse(ds.Tables[0].Rows[0]["SysID"].ToString());
				node.NodeID=int.Parse(ds.Tables[0].Rows[0]["NodeID"].ToString());
				node.Text=ds.Tables[0].Rows[0]["text"].ToString();
				if(ds.Tables[0].Rows[0]["ParentID"].ToString()!="")
				{
					node.ParentID=int.Parse(ds.Tables[0].Rows[0]["ParentID"].ToString());
				}
				node.Location=ds.Tables[0].Rows[0]["Location"].ToString();
				if(ds.Tables[0].Rows[0]["OrderID"].ToString()!="")
				{
					node.OrderID=int.Parse(ds.Tables[0].Rows[0]["OrderID"].ToString());
				}
				node.Comment=ds.Tables[0].Rows[0]["comment"].ToString();
				node.Url=ds.Tables[0].Rows[0]["url"].ToString();
				if(ds.Tables[0].Rows[0]["PermissionID"].ToString()!="")
				{
					node.PermissionID=int.Parse(ds.Tables[0].Rows[0]["PermissionID"].ToString());
				}
				node.ImageUrl=ds.Tables[0].Rows[0]["ImageUrl"].ToString();	
								
				return node;
			}
			else
			{
				return null;
			}			
		}
        
        public DataSet dtSysIdName()
        {
            string strSql = "Select SysID,SysName from Sys_SysIdName ";

            return ia.ExecuteDataSetSql(strSql);
        }
        public DataSet GetTreeList(string strWhere)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("Select SysID,NodeID,Text,ParentID,Url,PermissionID,ImageUrl,precondition,Location from Sys_TreeList ");
            strSql.Append("where 1=1 ");
            if (strWhere.Trim() != "")
            {
                strSql.Append(" and " + strWhere);
            }
            strSql.Append(" and Visible=1");
            strSql.Append(" Order by SysID,NodeID ");

            return ia.ExecuteDataSetSql(strSql.ToString());
        }

        /// <summary>
        /// 得到分頁數據
        /// </summary>
        /// <param name="PageSize">頁尺寸</param>
        /// <param name="PageIndex">頁碼</param>
        /// <param name="strWhere">查詢條件 (注意: 不要加 where)</param>
        /// <returns></returns>
        public DataSet GetListByPage(int PageSize, int PageIndex, string strWhere)
        {

            SqlParameter[] parameters = {
												new SqlParameter("@tblName", SqlDbType.VarChar, 255),
												new SqlParameter("@fldName", SqlDbType.VarChar, 255),
												new SqlParameter("@PageSize", SqlDbType.Int),
												new SqlParameter("@PageIndex", SqlDbType.Int),
												new SqlParameter("@IsReCount", SqlDbType.Bit),
												new SqlParameter("@OrderType", SqlDbType.Bit),
												new SqlParameter("@strWhere", SqlDbType.VarChar,1000),
													
				};
            parameters[0].Value = "Sys_TreeList";
            parameters[1].Value = "NodeID";
            parameters[2].Value = PageSize;
            parameters[3].Value = PageIndex;
            parameters[4].Value = 0;// 返回記錄總數, 非 0 值則返回
            parameters[5].Value = 0;//設置排序類型, 非 0 值則降序
            parameters[6].Value = strWhere;
            return ia.ExecuteDataSetProc("sp_GetRecordByPage", parameters);

        }

		#region 日志
		/// <summary>
		/// 增加日志
		/// </summary>
		/// <param name="time"></param>
		/// <param name="loginfo"></param>
		public void AddLog(string loginfo)
		{
            string strSql = string.Format("insert into Sys_Log(LogDateTime,LogInfo,UserID,HostName)values(getdate(),'{0}','{1}','{2}')", loginfo,"","");
            ia.ExecuteNonQuerySql(strSql);			
		}

		public void DeleteLog(int ID)
		{
            string strSql = string.Format("delete Sys_Log where ID={0}", ID);
            ia.ExecuteNonQuerySql(strSql);
		}
		public void DelOverdueLog(int days)
		{			
			string str=" DATEDIFF(day,[datetime],getdate())>"+days;
			DeleteLog(str);
		}
		public void DeleteLog(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("delete Sys_Log ");	
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
            ia.ExecuteNonQuerySql(strSql.ToString());
		}
		public DataSet GetLogs(string strWhere)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * from Sys_Log ");	
			if(strWhere.Trim()!="")
			{
				strSql.Append(" where "+strWhere);
			}
			strSql.Append(" order by ID DESC");
            return ia.ExecuteDataSetSql(strSql.ToString());
		}
		public DataRow GetLog(string ID)
		{
			StringBuilder strSql=new StringBuilder();
            strSql.Append("select * from Sys_Log ");				
			strSql.Append(" where ID= "+ID);
            return ia.ExecuteDataSetSql(strSql.ToString()).Tables[0].Rows[0];
		}
        public string HostName
        {
            get 
            {
                return ia.ExecuteDataSetSql("SELECT HOST_NAME() ").Tables[0].Rows[0][0].ToString();
            }
        }

        /// <summary>
        /// 獲得最大RecordID
        /// </summary>
        /// <returns></returns>
        public int GetMaxRecordID()
        {
            string strSQL = " select max(RecordID) from Sys_Record ";
            DataTable dt = ia.ExecuteDataSetSql(strSQL).Tables[0];
            int RowIndex = 1;
            if (dt.Rows.Count > 0)
                if (dt.Rows[0][0].ToString() != "")
                    RowIndex = int.Parse(dt.Rows[0][0].ToString()) + 1;
            return RowIndex;
        }
        /// <summary>
        /// Add
        /// </summary>
        /// <param name="Record"></param>
        /// <param name="HostProcess"></param>
        public void InsertSysRrecord(DataRow dr)
        {
            string strSql = string.Format("insert into Sys_Record(UserType, UserName, PersonName, OpDate, FormID, FormName, ActionState, IP) " +
                            "values ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}')",
                            dr["UserType"].ToString(), dr["UserName"].ToString(), dr["PersonName"].ToString(), DateTime.Now.ToString("yyyy/MM/dd HH:mm"),
                            dr["FormID"].ToString(), dr["FormName"].ToString(), dr["ActionState"].ToString(), dr["IP"].ToString());


            ia.ExecuteNonQuerySql(strSql.ToString());
        }

        /// <summary>
        /// 使用系統記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysRecord()
        {
            string strSQL = "Select * from Sys_Record order by RecordID desc";
            return ia.ExecuteDataSetSql(strSQL).Tables[0];
        }
        /// <summary>
        /// 使用系統記錄
        /// </summary>
        /// <returns></returns>
        public DataTable GetSysEmptyRecord()
        {
            string strSQL = "Select * from Sys_Record Where 1=2";
            return ia.ExecuteDataSetSql(strSQL).Tables[0];
        }
        /// <summary>
        /// 清除使用記錄
        /// </summary>
        public void ClearSysRecord()
        {
            string strSQL = "delete from Sys_Record";
            ia.ExecuteNonQuerySql(strSQL);            
        }

		#endregion

	}
}