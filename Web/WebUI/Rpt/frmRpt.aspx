<%@ Page Language="C#" AutoEventWireup="true" CodeFile="frmRpt.aspx.cs" Inherits="WebUI_Rpt_frmRpt" %>
<%@ Register Assembly="FastReport.Web" Namespace="FastReport.Web" TagPrefix="cc1" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="~/Styles/Main.css" type="text/css" rel="stylesheet" /> 
 <link href="~/Styles/op.css" type="text/css" rel="stylesheet" /> 
        <link rel="stylesheet" type="text/css" href="~/ext-3.3.1/resources/css/ext-all.css" />
 	    <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-base.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/ext-3.3.1/ext-all.js") %>'></script>

        <script type="text/javascript" src='<%=ResolveUrl("~/Scripts/JQuery/jquery-1.8.2.min.js") %>'></script> 
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/DataProcess.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Commons.js") %>'></script>
        <script type="text/javascript" src='<%=ResolveUrl("~/JScript/Resize.js") %>'></script>  
 <%--   <base target ="_self" />--%>

</head>
<body style="overflow:hidden;">
    <form id="form1" runat="server">   
         
   <%-- <div id="Report" align="center" style="width:100%;height:100%; overflow:auto;" >--%>
  <table style="width:100%;height:100%;"  >
    <tr><td align ="center">
    <table  class="maintable" style=" width:100%;"><tr style=" height: 22px;"><td align="left" ><asp:Label ID="lblCaption" runat="server" Text="Label">列印</asp:Label></td><td align="right" >&nbsp;&nbsp;&nbsp;&nbsp;</td></tr></table>
 
    <table  cellpadding="0" cellspacing="0"  border="1">
    <tr>
        <td runat ="server" id = "rptform"  valign="top">            
            <table border="0" cellpadding="0" cellspacing="0" width="586px" >                            
                            <tr>
                                <td style="height: 22px; width: 586px;">
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                        <tr>
                                            <td align="right" style="height: 24px; width: 25%;">
                                                <asp:RadioButton ID="optAlone" runat="server" Text="單個列印" Checked="True" GroupName="Area" />&nbsp;
                                            </td>
                                            <td align="left" width="*" style="height: 24px">
                                                <asp:TextBox ID="txtAlone" runat="server" CssClass="TextBox" Width="114px"></asp:TextBox></td>                                                
                                        </tr>
                                        <tr>
                                            <td align="right" style="height: 23px; width: 25%;">
                                                <asp:RadioButton ID="optArea" runat="server" Text="區間列印" GroupName="Area" />&nbsp;</td>
                                            <td align="left" style="height: 23px" width="45%">
                                                <asp:TextBox ID="txtStart" runat="server" CssClass="TextBox" ToolTip="啟始編號" Width="114px"></asp:TextBox>
                                                To
                                                <asp:TextBox ID="txtEnd" runat="server" CssClass="TextBox" Width="114px" ToolTip="結束編號"></asp:TextBox></td>
                                            <td align="left" width="20%" valign="middle">
                                                <asp:Button ID="btnSel1" runat="server" CssClass="but" Text=" 指定" 
                                                    Width="75px" />
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" style="width: 586px; height: 40px;padding-bottom:8px;">    
                                  <fieldset style="height:40px;width:80%;"> 
                                   <legend align="left">報表選擇</legend>                           
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                         <tr>
                                            <td align="right" style="height: 23px; width: 40%;">
                                                 <asp:RadioButton ID="rbtFixed" runat="server" Text="固定報表"
                                                     GroupName="form"  
                                                     Checked="true" AutoPostBack="True" 
                                                     oncheckedchanged="rbtFixed_CheckedChanged"/>
                                                  <asp:RadioButton ID="rbtUser" runat="server" Text="自定報表"  
                                                     GroupName="form"  
                                                     AutoPostBack="True" oncheckedchanged="rbtUser_CheckedChanged"/>&nbsp;&nbsp;
                                            </td>
                                            <td align="left" style="height: 23px; " >
                                                <asp:DropDownList ID="cmbRptName" runat="server" Width="236px"></asp:DropDownList>
                                            </td>
                                        </tr>
                                    </table>
                                   </fieldset>    
                                </td>
                            </tr>                          
                             <tr>
                                <td align="center" style="width: 586px; height: 40px">    
                                  <fieldset style="height:35px;width:80%;"> 
                                   <legend align="left">表尾條文</legend>                           
                                    <table border="0" cellpadding="0" cellspacing="0" width="100%" >
                                        <tr>
                                            <td align="right" style="height: 23px; width: 35%;">
                                                <asp:TextBox ID="txtTrail" runat="server" CssClass="TextBox" Width="113px" 
                                                   ></asp:TextBox>&nbsp;</td>
                                            <td align="left" style="height: 23px;" >
                                                <asp:TextBox ID="txtTrailN" 
                                                    runat="server" CssClass="ReadTextBox" Width="255px"  
                                                    ></asp:TextBox><input type="hidden" runat="server"  ID="txtItemContent" />
                                            </td>
                                        </tr>
                                    </table>
                                   </fieldset>    
                                </td>
                            </tr>
                           
                            <tr>
                                <td style="height: 40px; width: 586px;">
                                    <div align="center">
                                        <asp:Button ID="btPrint" runat="server" class="sabtnLength" Height="25px"
                                            Text="直接列印" Width="70px"  
                                            onclick="btPrint_Click" />&nbsp;
                                        <asp:Button ID="btViewLast" runat="server" class="sabtnLength" Height="25px"
                                            Text="預視上次" Width="70px" Enabled="False" onclick="btViewLast_Click" />&nbsp;
                                        <asp:Button ID="btnPreview" runat="server" class="sabtn" Height="25px"
                                            Text="  預視  " Width="52px"   
                                            onclick="btnPreview_Click" /><font face="新細明體">&nbsp;</font>
                                      
                                        <asp:Button ID="btnPrint" runat="server" class="sabtn" Height="25px"
                                            Text="  列印  " Width="52px" Visible="False" />
                                        &nbsp;<asp:Button ID="BtBack" runat="server" class="sabtn" Height="25px" 
                                            Text="返回" Width="52px" onclick="BtBack_Click"/>
                                    </div>
                                </td>
                            </tr>
                        </table>
        </td>
      
        <td runat ="server" id = "rptview" valign="top">           
        
         <span style="font-size: 10pt; font-family: Tahoma"><strong>
                        <span style="font-size: 4pt"> </span>
                        <cc1:WebReport ID="WebReport1" runat="server" BackColor="White" ButtonsPath="images\buttons1"
                            Font-Bold="False" Height = "100%" OnStartReport="WebReport1_StartReport"
                            ToolbarColor="Lavender" Width="100%" Zoom="1" />
                    </strong></span>       
       
        </td>
        <td runat ="server" id = "rptopt" valign="top" >
            <div >
            <table width = "150px" runat ="server" id = "rptset">
            <%--<tr><td align ="center"><asp:Button ID="btnReportSet" runat="server" class="cssbtn" Height="25px"
                                            Text="邊界設定" Width="70px"  /></td></tr>--%>
                                             <tr><td align ="center">
                                                 <asp:Button ID="btnback" runat="server" class="cssbtn" Height="25px"  
                                            Text="返回" Width="70px" onclick="btnback_Click"  /></td></tr>
                                            </table> 
                              
          <%-- <fieldset style="width: 150px; margin-left: 2px;">
                                                <legend>選擇票況</legend>
                                                <table width="85%" cellpadding="0px" cellspacing="2px">
                                                    <tr>
                                                        <td align="left" style="width: 100%;">
                                                            <fieldset style="width: 90%; margin-left: 2px;">
                                                                <legend>保證票</legend>
                                                                <asp:CheckBox ID="chkAssureNote" runat="server" Text="含保證票" Checked="true" />
                                                            </fieldset>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="CheckBox6" runat="server" Text="小計顯示" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk1" runat="server" Text="庫存" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk2" runat="server" Text="現金兌現" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk3" runat="server" Text="託收兌現" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk4" runat="server" Text="託收退票" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk5" runat="server" Text="託收" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <asp:CheckBox ID="chk6" runat="server" Text="轉付" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk7" runat="server" Text="撤票" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk8" runat="server" Text="退還" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk9" runat="server" Text="貼現" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk10" runat="server" Text="貼現兌現" Checked="true" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td height="9px">
                                                            <asp:CheckBox ID="chk11" runat="server" Text="貼現退票" Checked="true" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </fieldset>--%>
            
            </div>
           
        </td>
     
    </tr>
    </table>
    </td></tr>
    </table> 
    <input id="Hidden1" type="hidden" runat="server" />
    <asp:Literal ID="Literal8" runat="server" Visible="false" Text="請先指定報表!"></asp:Literal>
    <asp:Literal ID="Literal1" runat="server" Visible="false" Text="您所選的條件,無資料!"></asp:Literal>
    </form>
</body>
</html>
