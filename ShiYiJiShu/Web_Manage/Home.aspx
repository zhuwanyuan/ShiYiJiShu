<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Home.aspx.cs" Inherits="ShiYiJiShu.Web_Master.Home" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/style.css" type="text/css" rel="stylesheet" />
    <script src="editor/kindeditor.js" charset="utf-8" type="text/javascript" ></script>
    <script src="editor/lang/zh_CN.js" charset="utf-8" type="text/javascript" ></script>
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/formValidator.js" type="text/javascript"></script>
    <script src="js/formValidatorRegex.js" type="text/javascript"></script>
    
    
</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
        <div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text=""></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
           
        
            <div style="margin-top:30px; text-align:center; font-size:14px; padding-bottom:30px;">欢迎来到后台管理系统</div>
 
        </div>
    </form>
</body>
</html>
