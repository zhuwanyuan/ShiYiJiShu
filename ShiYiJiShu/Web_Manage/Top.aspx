<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Top.aspx.cs" Inherits="ShiYiJiShu.Web_Master.Top" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
</head>
<body>
<form id="form1" runat="server">
    <div class="topbg">
                <div class="logo"><img src="images/admin_01.gif" alt="" /></div>
                <div class="rcontent">
                    <asp:LinkButton ID="LinkButton1" runat="server" onclick="LinkButton1_Click">注销账户</asp:LinkButton>
                    <a href="javascript:location.reload()" class="top">刷新页面</a> 
                    <a href="Info.aspx?classid=5" class="top" target="mainFrame">后台首页</a> 
                    <a href="/" class="top" target="_blank">前台首页</a><br />
                    欢迎您，<asp:Label ID="lbUserName" runat="server" Text=""></asp:Label>
                </div>
     </div>
     <div class="topbg2">
                <div class="webname">
                   网站后台管理系统</div>
                <div class="toplink">
                    <ul>
                        <li class="line"></li>
                        <li><a href="EditPassword.aspx" class="toplink" target="mainFrame">密码修改</a></li>
                        <li class="line"></li>
                        <li><a href="JobList.aspx" class="toplink" target="mainFrame">招贤纳士</a></li>
                        <li class="line"></li> 
 
                    </ul>
                </div>
            </div>
</form>
</body>
</html>
