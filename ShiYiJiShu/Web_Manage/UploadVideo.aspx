<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UploadVideo.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.UploadVideo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
        <link href="css/style.css" type="text/css" rel="stylesheet" />

    <script src="js/artDialog.js?skin=idialog" type="text/javascript"></script>
    <script src="js/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="js/plugins/iframeTools.source.js" type="text/javascript"></script>
</head>
<body>
   <form id="form1" runat="server"> 

    <table border="0" class="tb2">
 
            <tr>
                <td class="l" style=" width:80px"> 上传图片：</td>
                <td class="r"> 
                    <asp:FileUpload ID="FileUpload1" runat="server" Width="180px" Height="23px" /> 
                    <br/>请上传mp4格式的文件。
                   </td>
            </tr>           
            <tr>
                <td class="l">
                </td>
                <td class="r">
                    <asp:Button ID="Button1" runat="server" OnClientClick="return CheckData();"  
                        CssClass="btn" Text="上传" onclick="Button1_Click" /></td>
            </tr>
        </table>
       
        <div class="msg"> <asp:Label ID="lbMessage" runat="server" Text=""></asp:Label></div>
    </form>
</body>
</html>
