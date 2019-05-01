<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="ShiYiJiShu.Web_Master.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>管理员登录</title>
    <link href="css/style.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript">
        function CheckData() {
            var username = document.getElementById("txtUsername");
            if (username.value == "") {
                alert("请输入用户名！");
                username.focus();
                return false;
            }

            var password = document.getElementById("txtPassword");
            if (password.value == "") {
                alert("请输入密码！");
                password.focus();
                return false;
            }

            var code = document.getElementById("code");
            if (code.value == "") {
                alert("请输入验证码！");
                code.focus();
                return false;
            }
        }
    </script>
</head>
<body>
   <form id="form1" runat="server">
<div class="login_bg h485">
<div class="mt100 tcenter w800">
<div class="fl"><img src="images/index_05.png"  alt=""/></div>
<div class="fl">
<div class="login_dl">
  <table border="0" cellpadding="0" cellspacing="0" class="table" style=" width: 210px; height: 116px;">
    <tr>
      <td style="width: 50px; color:#22529D; height: 28px;"align="left">用户名：</td>
      <td style="height: 35px; text-align:left">
           <asp:TextBox ID="txtUsername" runat="server" Width="118px" CssClass="txtbox1"></asp:TextBox>
       </td>
    </tr>
    <tr>
      <td style="width: 50px; color:#22529D; height: 28px;" align="left">密&nbsp;&nbsp; 码：</td>
      <td style=" height: 35px; text-align:left" >
         <asp:TextBox ID="txtPassword" runat="server" TextMode="Password" Width="118px" CssClass="txtbox1"></asp:TextBox>
                    </td>
    </tr>
 
    <tr>
      <td style="width: 50px; color:#22529D; height: 28px;"align="left">验证码：</td>
      <td align="left" style="height: 35px" >
          <asp:TextBox ID="code" runat="server" Width="50px" CssClass="txtbox1"></asp:TextBox> <img src="CheckCode.aspx" alt="验证码"/></td>
    </tr>
    <tr>
      <td colspan="2" align="center" style="height: 40px; text-align:left; padding-left:50px ">
          <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="images/index_12.gif" OnClientClick="return CheckData();"
              OnClick="ImageButton1_Click" /> &nbsp; <asp:ImageButton ID="ImageButton3" runat="server"
                  ImageUrl="images/index_15.gif" OnClick="ImageButton3_Click" /></td>
      </tr>
  </table>
</div>
</div>
</div>
    </div>
<div class="login_bottom"></div>
</form>
</body>
</html>
