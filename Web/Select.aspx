<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Select.aspx.cs" Inherits="Select" %>

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Untitled Page</title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
    <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
    <script type="text/javascript">

    function AddValues(oChk,strReturn) 
    {   var chkSelect=document.getElementById(oChk);
        if(chkSelect.checked)
        {
            (chkSelect.parentElement).parentElement.className="bottomtable";
            if (SelectPage.HdnSelectedValues.value == '')
                SelectPage.HdnSelectedValues.value += strReturn;
            else
                SelectPage.HdnSelectedValues.value += ',' + strReturn;
        }
        else
        {
            (chkSelect.parentElement).parentElement.className="";
            SelectPage.HdnSelectedValues.value = SelectPage.HdnSelectedValues.value.replace(',' + strReturn, '');
            SelectPage.HdnSelectedValues.value = SelectPage.HdnSelectedValues.value.replace(strReturn,'');  
        }
    }
    function SelectAll(tempControl)
    {
        var theBox=tempControl;
        xState = theBox.checked;

        elem=theBox.form.elements;
        for(i=0;i<elem.length;i++)
        //if(elem[i].type=="checkbox" && elem[i].id!=theBox.id && elem[i].id.substring(elem[i].id.length-2,elem[i].id.length)==theBox.id.substring(theBox.id.length-2,theBox.id.length))
        if(elem[i].id.indexOf("GridView1_chkSelect")>=0)
        {
           if(elem[i].checked!=xState)
           {
                elem[i].click();
           }
        }
	}
    function SelectedCell()
    {
        elem=SelectPage.elements;
        for(i=0;i<elem.length;i++)
        if(elem[i].type=='checkbox' && elem[i].checked==true)
        {
           (elem[i].parentElement).parentElement.className="bottomtable";
        }
    }
	function AllClear(bln)
    {
        elem=SelectPage.elements;
        for(i=0;i<elem.length;i++)
        if(elem[i].type=="checkbox" && elem[i].id.substring(elem[i].id.length-6,elem[i].id.length)=="Select")
        {
           if(bln==0)
               elem[i].checked=true;
           else
               elem[i].checked=false;
           elem[i].click();
        }
   }
   function Close() {
       SelectPage.HdnSelectedValues.value = "";
       window.parent.returnValue = document.getElementById('HdnSelectedValues').value;
       window.parent.close();
   }
  </script>
  </head>
<body>
    <form id="SelectPage" runat="server" >
    <div >
        <table width="100%" cellpadding="0" cellspacing="0"  class="table_bgcolor">
            <tr>
                <td style="height: 4px">
                <asp:Button ID="btnAll" runat="server" Text="本頁全選" Width="70px" CssClass="but" OnClientClick="AllClear(1);return false;" />
                &nbsp;<asp:Button ID="btnClear" runat="server" Text="本頁全清" Width="70px" CssClass="but" OnClientClick="AllClear(0);return false;" />
                &nbsp;<asp:Button ID="btnSelect" runat="server" Text="取回" Width="50px" CssClass="but" OnClick="btnSelect_Click"   />
                &nbsp;<asp:Button ID="btnClose" runat="server" Text="關閉" Width="50px" CssClass="but" OnClientClick="Close()" /></td>
            </tr>

        </table>
         <table class="maintable" cellspacing="0" cellpadding="0" width="100%" align="center" border="1" >
					    <tr> 
						    <td class="smalltitle" align="center" width="10%" >查詢欄位</td>
						    <td  width="20%" height="20">&nbsp;
							    <asp:dropdownlist id="ddlFieldName" runat="server" Width="90%" 
                                    meta:resourcekey="ddlFieldNameResource2"></asp:dropdownlist></td>
						    <td class="smalltitle" align="center" width="10%">查詢內容</td>
						    <td  width="60%" height="20" valign="middle">&nbsp;<asp:textbox id="txtContent" 
                                    tabIndex="1" runat="server" Width="60%" CssClass="TextBox" MaxLength="100" 
                                    heigth="16px" meta:resourcekey="txtContentResource2"></asp:textbox>
                                &nbsp;<asp:button id="btnSearch" tabIndex="2" runat="server" Width="60px" 
                                    CssClass="but" Text="立即查詢" meta:resourcekey="btnSearchResource2" 
                                    onclick="btnSearch_Click"></asp:button>
                                &nbsp;<asp:Button ID="btnMultiSearch" runat="server" CssClass="but" tabIndex="3" 
                                    Text="多條件查詢" OnClientClick="SearchDialog('<%= FormID%>',300)" Width="80px" 
                                    meta:resourcekey="btnMultiSearchResource2" onclick="btnMultiSearch_Click" />
                            </td>
					    </tr>
				    
			    </table>
        <div style="overflow-x: auto;overflow-y:no; width:100%; height:420px" id="SelectDiv">
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <asp:GridView ID="GridView1" runat="server" SkinID="GridViewSkin" Height="20px" Width="100%" AllowPaging="True"  PageSize="12" OnRowDataBound="GridView1_RowDataBound" FooterStyle-Wrap="false" HeaderStyle-Wrap="false" RowStyle-Wrap="false" >

<RowStyle Wrap="False"></RowStyle>
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>                        
				        <input type="checkbox" runat="server" id="chkHeadSelect" onclick="SelectAll(this);" value='<%#Eval("ID")%>'/>                    
                    </HeaderTemplate>
			        <ItemTemplate>
				        <input type="checkbox" runat="server" id="chkSelect" value='<%#Eval("ID")%>'/>
			        </ItemTemplate>
                    <HeaderStyle Width="20px" />
                    <ItemStyle HorizontalAlign="Center" />
		        </asp:TemplateField>
		        <asp:TemplateField HeaderText="編號" Visible="False">
			        <ItemTemplate>
				        <asp:Literal Text='<%#Eval("ID")%>' runat="server" ID="IDShow" Visible="false"/>
			        </ItemTemplate>
		        </asp:TemplateField>
		        <asp:TemplateField HeaderText="選取">
			        <ItemTemplate>
                        <asp:Button ID="btnSingle" runat="server" Text="選取" CssClass="but"  />
			        </ItemTemplate>                    
                    <ControlStyle Width="50px" Height="20px" />
		        </asp:TemplateField>		        
               
	        </Columns>
	         <PagerSettings Visible="False" />
                
<FooterStyle Wrap="False"></FooterStyle>

<HeaderStyle Wrap="False"></HeaderStyle>
                
        </asp:GridView>
        
        </div>
        <table width="100%" class="table_bgcolor">
            <tr>
                <td style=" height:3px;"></td>
            </tr>
            <tr>
                <td align="left">
                    <asp:LinkButton ID="btnFirst" runat="server" OnClick="btnFirst_Click" >首頁</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btnPre" runat="server"   OnClick="btnPre_Click" >上一頁</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btnNext" runat="server"  OnClick="btnNext_Click" >下一頁</asp:LinkButton>&nbsp;
                    <asp:LinkButton ID="btnLast" runat="server"  OnClick="btnLast_Click" >尾頁</asp:LinkButton>&nbsp;&nbsp;
                    <asp:TextBox ID="txtPage" runat="server" Width="34px" CssClass="pagetext" onkeypress="return regInput(this,/^\d+$/,String.fromCharCode(event.keyCode)) ;"
                     ondrop="return regInput(this,/^\d+$/,event.dataTransfer.getData('Text'));" onpaste="return regInput(this,/^\d+$/,window.clipboardData.getData('Text'));"
                     onkeydown="if(event.keyCode==13){event.keyCode=9;document.getElementById('btnToPage').click();return false;}" ></asp:TextBox>
                     <asp:LinkButton ID="btnToPage" runat="server"  Width="40px" OnClick="btnToPage_Click" >跳轉</asp:LinkButton>&nbsp;&nbsp;  
                    <asp:Label ID="lblPage" runat="server" Text=""></asp:Label>&nbsp;</td>
            </tr>
        </table>
       
        <input id="HdnSelectedValues" type="hidden" name="HdnSelectedValues" runat="server" />
        
    </div>
    
    </form>
</body>
</html>
