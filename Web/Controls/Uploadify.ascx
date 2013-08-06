<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Uploadify.ascx.cs" Inherits="Controls_Uploadify" %>
<div>
    
    <input type="file" id="file_upload" name="file_upload" />
    <div id="uploadfileQueue"></div>
    <p>

        <asp:Button ID="btnUpload" runat="server" Text="上傳" 
            OnClientClick="javascript:jQuery('#file_upload').uploadify('upload','*');return false" 
            meta:resourcekey="btnUploadResource1" />
        <asp:Button ID="btnCancel" runat="server" Text="取消上傳" 
            OnClientClick="javascript:jQuery('#file_upload').uploadify('cancel','*');return false" 
            meta:resourcekey="btnCancelResource1" />
    </p>
    
</div>