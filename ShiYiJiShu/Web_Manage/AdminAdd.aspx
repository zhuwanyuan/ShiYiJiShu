<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminAdd.aspx.cs" Inherits="ShiYiJiShu.Web_Master.AdminAdd" %>

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
            $("#txtUserName").formValidator({ tipid: "userNameTip", onfocus: "请输入用户名！", oncorrect: " &nbsp; " }).inputValidator({ min: 1, onerror: "用户名不能为空！" });

            //验证新密码
            $("#txtPassword").formValidator({ tipid: "passwordTip", onfocus: "请输入密码！", oncorrect: " &nbsp; " }).inputValidator({ min: 6, max: 30, onerror: "密码长度不能小于6并且不能大于30！" });
 
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="right">
            <div class="top">
                <div class="title">
                    <asp:Label ID="lbName" runat="server" Text="添加管理员"></asp:Label>
                </div>
                <div class="rimg">
                    <img src="images/admin_12.gif" alt="" /></div>
            </div>
            <table border="0" cellpadding="0" cellspacing="1" class="tb2">
                <tr>
                    <td class="l" style="width: 80px"><span class="red">*</span> 用户名：</td>
                    <td class="r">
                        <asp:TextBox ID="txtUserName" runat="server" Width="200px" CssClass="txtbox" ></asp:TextBox>
                        <span id="userNameTip" class="red"></span>
                    </td>
                </tr>
                <tr>
                    <td class="l" style="width: 80px"><span class="red">*</span> 密码：</td>
                    <td class="r">
                        <asp:TextBox ID="txtPassword" runat="server" Width="200px" CssClass="txtbox" ></asp:TextBox>
                        <span id="passwordTip"></span>
                    </td>
                </tr>
                <tr>
                    <td class="l"></td>
                    <td class="r">
                        <asp:Button ID="Button1" runat="server" Text="提交" OnClick="Button1_Click" CssClass="btn" /></td>
                </tr>
            </table>

        </div>
    </form>
</body>
</html>
