<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StyleLimitedProduct.aspx.cs" Inherits="WebUI_Label_StyleLimitedProduct" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =TitleName%></title>
   <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/Detail.css" type="text/css" rel="stylesheet" /> 
        <link href="~/Styles/superTables.css" type="text/css" rel="stylesheet" /> 
        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 	  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Detail.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/superTables.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Scrolltable.js") %>'></script>
        <script type="text/javascript" src="Js/StyleLimitedProduct.js"></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript">
            $(document).ready(function () {
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                $('#btnAddDetail').bind('click', function () { doOption('Add', 1); });
                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });
                $("#btnOk").bind('click', function () {
                    //子檔驗證提示
                    var table = $("#" + tbTableId[0] + "_sDataTable");
                    var rowCount = table.find('tr').length - 1;
                    if (rowCount == 0) {
                        window.close();
                        return false;
                    }
                    for (var i = 0; i < rowCount; i++) {
                        var rowIndex = i + 1;
                        if ($.trim($("#" + tbRowName[0] + "_" + rowIndex + "_ProductID").val()) == "") {
                            alert("<%=sub1xxProductID.Text.Split(',')[0] %>" + "<%=Resources.Resource.NotNull %>!");
                            $("#" + tbRowName[0] + "_" + rowIndex + "_ProductID").focus();
                            return false;
                        }
                    }
                    window.returnValue = GenerateDetailToJson(0);
                    //$('#HdnSubDetail1').val(data);
                    window.close();
                    return false;
                });
                $("#btnCancel").bind('click', function () { window.close(); return false; });
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
            function Open() {
                //var page = "ProductResumeCompare.aspx";
                //var EnterpriseID = $("#txtEnterpriseID").val();
                var strReturn = window.showModalDialog('OtherActionUpload.aspx', window, 'DialogHeight:200px;DialogWidth:800px;help:no;scroll:no');
                return false;
            }
        </script>
</head>
<body>
    <form id="form1" runat="server">
         <asp:ScriptManager ID="ScriptManager1" runat="server" />  
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >
					<tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseName" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="70%" >&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead" Width="30%" MaxLength="10"  ReadOnly="true">A0001</asp:textbox>
                                &nbsp;<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="65%" MaxLength="10">屏東漁會</asp:textbox>
                        </td>
                       		<td><asp:button id="btnOk" runat="server" Text="確定"  CssClass="but" Width="80px"></asp:button>&nbsp;
                       <asp:button id="btnCancel" runat="server" Text="放棄"  CssClass="but" Width="80px" 
                                    ></asp:button></td>				
					</tr>
                    <tr>
						<td class="musttitle" align="center" width="10%" ><asp:Literal ID="Literal1" Text="款式編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="70%" >&nbsp;
								<asp:textbox id="txtStyleID" runat="server" CssClass="TextRead" Width="30%" MaxLength="10"  ReadOnly="true">Q01</asp:textbox>
                                &nbsp;<asp:textbox id="txtLabelMode" runat="server" CssClass="TextRead"  ReadOnly="true" Width="65%" MaxLength="10">紋理防偽標籤15mm*30mm</asp:textbox>
                        </td>
                       			
					</tr>                 
					
			</table>           
            
            
            <table style="width:100%">
                        <tr>
                            <td class="table_titlebgcolor" height="25px">
                                <input id="btnAddDetail" class="ButtonCss" type="button" value="新增明細" style="width:60px;"  />
                                <input id="btnDelDetail" class="ButtonCss" type="button" value="刪除明細" style="width:60px;" />
                                <input id="btnInsDetail" class="ButtonCss" type="button" value="插入明細"  style="width:60px;" />
                            </td>
                        </tr>
              </table> 

               <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:205px;">
               </div>      
         
           <input id="HdnSubDetail1" type="hidden" runat="server" /> 
           <div id="subColsName1" runat="server">
                    <asp:Literal ID="sub1xxRowID" Text="(序號),80,label,1" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxProductID" Text="產品編號,100,text,0" Visible="false" runat="server"></asp:Literal>  
                    <asp:Literal ID="sub1xxProductName" Text="(品名規格),200,text,1" Visible="false" runat="server"></asp:Literal>    
                    <asp:Literal ID="sub1xxMemo" Text="備註,200,text,0" Visible="false" runat="server"></asp:Literal>     
                       
                    </div>
    </form>
</body>
</html>

