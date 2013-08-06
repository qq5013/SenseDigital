<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Sample.aspx.cs" Inherits="WebUI_BusinessUnit_Sample" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" />
    <link rel="stylesheet" type="text/css"  href="~/Scripts/jquery.ui/themes/cupertino/jquery.ui.theme.css" />    
    <link rel="stylesheet" type="text/css"  href="~/Scripts/jquery.ui/themes/all/jquery.ui.all.css" />

    
    <!-- ui.jqgrid.css 在下载的jqGrid包里  -->  
    <link rel="stylesheet" type="text/css" href="~/Scripts/jqgrid/css/ui.jqgrid.css" media="all"/>  
    
    <!-- js  -->  
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.3.min.js") %>'></script> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/jquery.ui/jquery-ui-1.9.1.custom.min.js") %>'></script>   
    <!-- jqGrid de iln8 语言包，中文环境下必须安装grid.locale-cn.js，其他环境需要装对应的语言包 --> 
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/jqgrid/js/grid.locale-cn.js") %>'></script>   
    <!-- jqGrid的所有的压缩版。  -->
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/jqgrid/js/jquery.jqGrid.min.js") %>'></script>
    
    <script type="text/javascript" src="Sample.js"></script>
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/Js/Tabs.js") %>'></script>
    <!--
    <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/Js/Loading.js") %>'></script>  -->
   
</head>
<body>
    <form id="form1" runat="server">
    <table class="gbtext" id="KcTable" style="WORD-BREAK: break-all; BORDER-COLLAPSE: collapse; HEIGHT: 88px; WORD-WRAP: break-word;border-color:#ffffff;background-color:#bad5eb;"
				 cellspacing="0" cellpadding="0" width="100%" align="center"
				border="1" runat="server">
				<tbody>
					<tr>
						<td style="HEIGHT: 29px" valign="middle" align="center" colspan="6" height="29"  >
							<table cellspacing="0" cellpadding="0" width="100%" border="0" >
								<tr>
									<td class="titline1">
										<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td class="title1" noWrap><b>客戶資料[ 多筆明細畫面 ]</b></td>
											</tr>
											<tr>
												<td style="background-color:#109a42"><IMG height="3" src="../../Images/Blue_1.gif" width="5"></td>
											</tr>
										</table>
									</td>
									<td class="titline4" width="100%"><IMG height="1" src="../../Images/t.gif" width="1" border="0"></td>
									<td class="titline2" noWrap><asp:button id="btnMultiSearch" tabIndex="3" runat="server" Width="80px" CssClass="but" Text="多條件查詢" OnClientClick="openMultipleSearchDialog();return false" onclick="btnMultiSearch_Click"></asp:button>&nbsp;<asp:button 
                                            id="btnAddNew" tabIndex="4" runat="server" Width="70px" CssClass="but" 
                                            Text="新增" onclick="btnAddNew_Click"></asp:button>&nbsp;<asp:button id="btnDelete" tabIndex="5" runat="server" Width="70px" CssClass="but" Text="刪除"
											CausesValidation="False" onclick="btnDelete_Click"></asp:button>&nbsp;<INPUT class="but" onclick="history.go(-1)" type="button" value="回上一頁"></td>
							
                                </tr>
							</table>
						</td>
					</tr>
					<tr>
						<td style="background-color:#7baed9;" align="center" width="10%"  height="20"><font face="新細明體" color="#ffffff">查詢欄位</font></td>
						<td  width="20%" height="20"><font face="新細明體">&nbsp;</font>
							<asp:dropdownlist id="ddlFieldName" runat="server" Width="90%"></asp:dropdownlist></td>
						<td style="background-color:#7baed9;" align="center" width="10%"  height="20"><font face="新細明體" color="#ffffff">查詢內容</font></td>
						<td style="HEIGHT: 20px" width="60%" height="32"><asp:textbox id="txtContent" tabIndex="1" runat="server" Width="80%" CssClass="TextBox" MaxLength="100"></asp:textbox><font face="新細明體">&nbsp;</font><asp:button id="btnSearch" tabIndex="2" runat="server" Width="60px" CssClass="but" Text="立即查詢" onclick="btnSearch_Click"></asp:button></td>
					</tr>
					
				</tbody>
			</table>

            <table id="gridTable" style="padding:0px;width:100%"></table>
         <div id="gridPager"></div>

         <div id="multipleSearchDialog">  
     <table class="formTable">  
         <thead>  
             <tr>  
                 <th>查询条件</th>  
                 <th>查询方式</th>  
                 <th>查询值</th>  
             </tr>  
         </thead>  
         <tbody>  
             <tr>  
                 <td>  
                     <select class="searchField">  
                         <option value="TASK_NO">任务号</option>  
                         <option value="TASK_TYPE">任务类型</option>  
                         <option value="BILL_NO">单据编号</option>  
                     </select>  
                 </td>  
                 <td>  
                     <select class="searchOper">  
                         <option value="eq">等于</option>  
                         <option value="gt">大于</option>  
                         <option value="lt">小于</option>  
                     </select>  
                 </td>  
                 <td>  
                     <input type="text" class="searchString"/>
                 </td>  
             </tr>  
             <tr>  
                 <td>  
                     <select class="searchField">  
                         <option value="TASK_NO">任务号</option>  
                         <option value="TASK_TYPE">任务类型</option>  
                         <option value="BILL_NO">单据编号</option>  
                     </select>  
                 </td>  
                 <td>  
                     <select class="searchOper">  
                         <option value="eq">等于</option>  
                         <option value="gt">大于</option>  
                         <option value="lt">小于</option>  
                     </select>  
                 </td>  
                 <td>  
                     <input type="text" class="searchString" />
                 </td>  
             </tr>  
             <tr>  
                 <td>  
                     <select class="searchField">  
                         <option value="TASK_NO">任务号</option>  
                         <option value="TASK_TYPE">任务类型</option>  
                         <option value="BILL_NO">单据编号</option>  
                     </select>  
                 </td>  
                 <td>  
                     <select class="searchOper">  
                         <option value="eq">等于</option>  
                         <option value="gt">大于</option>  
                         <option value="lt">小于</option>  
                     </select>  
                 </td>  
                 <td>  
                     <input type="text" class="searchString" />
                 </td>  
             </tr>  
         </tbody>  
     </table>  
 </div>
    </form>
</body>
</html>
