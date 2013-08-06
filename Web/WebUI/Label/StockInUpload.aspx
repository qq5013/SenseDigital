<%@ Page Language="C#" AutoEventWireup="true" CodeFile="StockInUpload.aspx.cs" Inherits="WebUI_Label_StockInUpload" %>

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
        <script type="text/javascript" src="Js/StockInUpload.js"></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Ajax.js") %>'></script>
        <script type="text/javascript">
            $(document).ready(function () {
                subData[0] = jQuery.parseJSON($('#HdnSubDetail1').val());
                initialTable();
                AddToDetail();
                $('#btnClearList').bind('click', function () { doOption('Add', 1); });
                $('#btnDelDetail').bind('click', function () { doOption('Del'); });
                $('#btnInsDetail').bind('click', function () { doOption('Ins', 1); });
            });

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
        <asp:UpdateProgress ID="updateprogress" runat="server" AssociatedUpdatePanelID="updatePanel">
        <ProgressTemplate>            
                 <div id="progressBackgroundFilter" style="display:none"></div>
            <div id="processMessage"> Loading...<br />
                 <img alt="Loading" src="../../images/main/loading.gif" />
            </div>            
 
            </ProgressTemplate>
 
        </asp:UpdateProgress>  
                    <asp:UpdatePanel ID="updatePanel" runat="server" UpdateMode="Conditional">                
                    <ContentTemplate>
			<table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >
                    <tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="lblBillID" Text="入庫單號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtBillID" runat="server" CssClass="TextRead" Width="90%" MaxLength="10">A0001</asp:textbox>
                                &nbsp;
                        </td>
                        <td class="smalltitle" align="center" width="10%" ><asp:Literal ID="ltlStyleID" Text="款式編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtStyleID" runat="server" CssClass="TextRead" Width="30%" MaxLength="10">Q01</asp:textbox>
                                &nbsp;<asp:textbox id="txtLabelMode" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="10">紋理防偽標籤15mm*30mm</asp:textbox>
                        </td>

                       		<td>
                            <asp:button id="btnUpload" runat="server" Text="上傳"  CssClass="but" 
                                    Width="80px"></asp:button>
                        </td>				
					</tr>
					<tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="ltlEnterpriseID" Text="企業編號"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%">&nbsp;
								<asp:textbox id="txtEnterpriseID" runat="server" CssClass="TextRead" Width="30%" MaxLength="10">A0001</asp:textbox>
                                &nbsp;<asp:textbox id="txtEnterpriseName" runat="server" CssClass="TextRead"  ReadOnly="true" Width="60%" MaxLength="10">屏東漁會</asp:textbox>
                        </td>
                        <td class="smalltitle" align="center" width="10%" ><asp:Literal ID="ltlQtyTotal" Text="入庫數量"
                                runat="server"></asp:Literal>
                        </td>
						<td width="35%" >&nbsp;
								<asp:textbox id="txtQtyTotal" runat="server" CssClass="TextRead" Width="90%" MaxLength="10">10</asp:textbox>
                                &nbsp;
                        </td>
                       		<td>
                                &nbsp;</td>				
					</tr>
                    <tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="Literal1" Text="檔案目錄"
                                runat="server"></asp:Literal>
                        </td >
						<td width="80%" colspan="3">&nbsp;
								<asp:textbox id="txtFileName" runat="server" CssClass="TextRead" Width="96%" MaxLength="10"></asp:textbox>
                                &nbsp;</td>
                        <td>
                            <asp:button id="btnViewFile" runat="server" Text="檢視檔案"  CssClass="but" Width="80px"></asp:button>
                        </td>
					</tr>                 
					
			</table>           
            
            
            <table style="width:100%">
                        <tr>
                            <td class="table_titlebgcolor" height="25px">
                                <input id="btnClearList" class="ButtonCss" type="button" value="清除清單" 
                                    style="width:60px;"  />
                                <input id="btnClearAll" class="ButtonCss" type="button" value="以下全清" 
                                    style="width:60px;" />
                                <input id="btnClearBetween" class="ButtonCss" type="button" value="區間清除"  
                                    style="width:60px;" />
                                <input id="btnBrowse" class="ButtonCss" type="button" value="瀏覽取回"  
                                    style="width:60px;" /></td>
                        </tr>
              </table> 
              
               <div class="fakeContainer" id="div_Detail1" style=" margin:5px;height:205px;">
               </div>      
         
           <input id="HdnSubDetail1" type="hidden" runat="server" /> 

           <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" bordercolor="#ffffff"
				border="1" >
                    <tr>
						<td class="smalltitle" align="center" width="10%" ><asp:Literal ID="Literal2" Text="檔案筆數"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="Textbox1" runat="server" CssClass="TextRead" Width="90%" MaxLength="10"></asp:textbox>
                                &nbsp;
                        </td>
                        <td class="smalltitle" align="center" width="10%" ><asp:Literal ID="Literal3" Text="有效筆數"
                                runat="server"></asp:Literal>
                        </td>
						<td width="25%" >&nbsp;
								<asp:textbox id="Textbox2" runat="server" CssClass="TextRead" Width="90%" MaxLength="10"></asp:textbox>
                                &nbsp;</td>

                       		<td class="smalltitle" align="center" width="10%" >
                                <asp:Literal ID="Literal4" runat="server" Text="已上傳筆數"></asp:Literal>
                        </td>	
                                <td>
                                    &nbsp;&nbsp;<asp:TextBox ID="Textbox3" runat="server" CssClass="TextRead" MaxLength="10" 
                                        Width="90%"></asp:TextBox>
                                    &nbsp;</td>				
					</tr>
					
			</table>           
           </ContentTemplate>
            </asp:UpdatePanel> 	
    </form>
</body>
</html>

