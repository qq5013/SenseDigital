<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReturnLabelEdit.aspx.cs" Inherits="WebUI_Label_ReturnLabelEdit" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
   <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 	  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        <script type="text/javascript" src="Js/ReturnLabel.js"></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript">
            $(document).ready(function () {
                $("#btnLoad").bind("click", function () {
                    window.showModalDialog('ReturnLabelLoad.aspx', null, 'dialogHeight:300px;dialogWidth:820px;toolbar=no,status=yes,resizable=no'); return false;
                    return false;
                });
                content_resize();
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });
            });
            function Save() {
                //主檔驗證提示            

                //子檔驗證提示
                //                var table = $("#" + tbTableId[0] + "_sDataTable");
                //                var rowCount = table.find('tr').length - 1;                
                //                for (var i = 0; i < rowCount; i++) {
                //                    var rowIndex = i + 1;
                //                    if ($.trim($("#" + tbRowName[0] + "_" + rowIndex + "_LinkMan").val()) == "") {
                //                        alert("<%=Resources.Resource.BU_Detail_LinkMan_NoNull %>");
                //                        $("#" + tbRowName[0] + "_" + rowIndex + "_LinkMan").focus();
                //                        return false;
                //                    }
                //                }
                //                var data = GenerateDetailToJson(0);
                //                $('#HdnSubDetail1').val(data);
                //                return true;
            }
          
        </script>
</head>
<body>
    <form id="form1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />  
    <div>
           <table style="width: 100%; height: 20px;" class="OperationBar">
                <tr>
                      <td align="right">
                        <asp:Button ID="btnCancel" runat="server" Text="放棄" OnClientClick="history.go(-1);return false" CssClass="ButtonCancel" />
                        <asp:Button ID="btnSave" runat="server" Text="保存" OnClientClick="return Save()" 
                            CssClass="ButtonCreate" onclick="btnSave_Click" />
                        <asp:Button ID="btnExit" runat="server" Text="離開" OnClientClick="Exit()" CssClass="ButtonExit" />
                    </td>
                </tr>
            </table>
            <div id="surdiv" style="overflow:auto">
			<table id="Table1" class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" runat="server">			
					<tr>
						<td  valign="middle" align="left" colspan="2" height="22" class="title1">
							<p><asp:Literal ID="ltlTitle" Text="標籤退貨作廢作業[ 單筆編輯畫面 ]" runat="server"></asp:Literal></p>
						</td>
					</tr>							
						
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlBillID" Text="退貨單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead"  ReadOnly="true" Width="80%" MaxLength="20"></asp:textbox>
                     &nbsp;&nbsp;
								  <asp:button id="btnLoad" runat="server" Text="載入單據"  CssClass="but" Width="80px"></asp:button>
                        </td>
					</tr>
                    		
					<tr>
						<td class="musttitle" align="center" width="15%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="85%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextBox" Width="30%" MaxLength="6"></asp:textbox>
                                <asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="50%" MaxLength="100"></asp:textbox>
                        &nbsp;
								  </td>
                       						
					</tr>                 
					
			</table>           
            
            
          <table style="width:100%">
                        <tr>
                            <td class="table_titlebgcolor" height="25px">
                                <input id="btnAddDetail" class="ButtonCss" type="button" value="新增明細" style="width:60px;"  />
                                <input id="btnDelDetail" class="ButtonCss" type="button" value="刪除明細" style="width:60px;" />
                                <input id="btnInsDetail" class="ButtonCss" type="button" value="插入明細"  style="width:60px;" />
                                <input id="btnInsDetail0" class="ButtonCss" type="button" value="以下同值"  
                                    style="width:60px;" /></td>
                        </tr>
              </table> 

               <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:205px;">
               </div>      
         
            <table width="100%" class="maintable" bordercolor="#ffffff"
				border="1">   
                 <tr>
						<td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlBillState" Text="狀&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;態"
                                runat="server"></asp:Literal>
                        </td>
						<td width="30%">&nbsp;
								<asp:textbox id="txtBillState" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" width="15%">                            
                            <asp:Literal ID="ltlQtyTotal" runat="server" Text="數量合計"></asp:Literal>							
						    
						</td>
						<td width="40%">&nbsp;
								<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>&nbsp;
                                 </td>
					</tr>                       
                 <tr>
						<td class="smalltitle" align="center" width="15%"><asp:Literal ID="ltlCreateUserName" Text="建檔人員"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%">&nbsp;
								<asp:textbox id="txtCreateUserName" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>
                         </td>
						<td class="smalltitle" align="center" width="15%">                            
                            <asp:Literal ID="ltlCreateDate" runat="server" Text="建檔日期"></asp:Literal>							
						    
						</td>
						<td width="40%">&nbsp;
								<asp:textbox id="txtCreateDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="True"></asp:textbox>&nbsp;
                                  </td>
					</tr>
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlLastModifyUserName" Text="異動人員"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtLastModifyUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true" ></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">							
						 
                            <asp:Literal ID="ltlLastModifyDate" Text="異動日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
                        <asp:textbox id="txtLastModifyDate" runat="server" CssClass="TextRead" Width="90%" 
                                MaxLength="20" ReadOnly="true"></asp:textbox>&nbsp;
                               </td>
					</tr>
                     
                    <tr>
						<td class="smalltitle" align="center"><asp:Literal ID="ltlCheckUserName" Text="營運覆核"
                                runat="server"></asp:Literal>
                        </td>
						<td >&nbsp;
								<asp:textbox id="txtCheckUserName" runat="server" CssClass="TextRead" Width="90%" MaxLength="20" ReadOnly="true"></asp:textbox>
                        </td>
						<td class="smalltitle" align="center">	
                            <asp:Literal ID="ltlCheckDate" Text="營運覆核日期"
                                runat="server"></asp:Literal>
						</td>
						<td >&nbsp;
								<asp:textbox id="txtCheckDate" runat="server" CssClass="TextRead" Width="90%" ReadOnly="true"
                                MaxLength="20"></asp:textbox>&nbsp;
                                </td>
					</tr>
                    </table>  
            </div>
        </div>       
    </div>
   
    </form>
</body>
</html>
