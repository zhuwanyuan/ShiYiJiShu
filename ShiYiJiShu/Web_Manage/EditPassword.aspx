<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EditPassword.aspx.cs" Inherits="ShiYiJiShu.Web_Master.EditPassword" %>

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
    
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#txtContent', {
                cssPath: 'plugins/code/prettify.css',
                uploadJson: 'editor/asp.net/upload_json.ashx',
                fileManagerJson: 'editor/asp.net/file_manager_json.ashx',
                allowFileManager: true,
                afterCreate: function () {
                    var self = this;
                    K.ctrl(document, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                    K.ctrl(self.edit.doc, 13, function () {
                        self.sync();
                        K('form[name=form1]')[0].submit();
                    });
                }
            });

        });

        $(document).ready(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function (msg) { alert(msg) } });

            //验证原密码
            $("#txtOldPassword").formValidator({ tipid: "oldPasswordTip", onfocus: "请输入原密码！", oncorrect: " &nbsp; " }).inputValidator({ min: 1, onerror: "原密码不能为空！" });

            //验证新密码
            $("#txtNewPassword").formValidator({ tipid: "passwordTip", onfocus: "请输入新密码！", oncorrect: " &nbsp; " }).inputValidator({ min: 6, max: 30, onerror: "密码长度不能小于6并且不能大于30！" });

            //重复密码
            $("#txtNewPassword2").formValidator({ tipid: "password2Tip", onfocus: "请重新输入新密码！", oncorrect: " &nbsp; " }).inputValidator({ min: 6, max: 30, onerror: "密码长度不能小于6并且不能大于30！" }).compareValidator({ desid: "txtNewPassword", operateor: "=", onerror: "您两次输入密码不一致！" });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
   <div class="right">
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="密码修改"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 原密码：</td>
            <td class="r">
                <asp:TextBox ID="txtOldPassword" runat="server" Width="200px" CssClass="txtbox" TextMode="Password"></asp:TextBox> <span id="oldPasswordTip" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 新密码：</td>
            <td class="r">
                <asp:TextBox ID="txtNewPassword" runat="server" Width="200px" CssClass="txtbox" TextMode="Password"></asp:TextBox> <span id="passwordTip"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 确认密码：</td>
            <td class="r">
                <asp:TextBox ID="txtNewPassword2" runat="server" Width="200px" CssClass="txtbox" TextMode="Password"></asp:TextBox> <span id="password2Tip"></span>
               </td>
        </tr>
       
        <tr>
            <td class="l">
            </td>
            <td class="r"> 
                <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" CssClass="btn" /></td>
        </tr>
    </table>
 
</div>
    </form>
</body>
</html>
