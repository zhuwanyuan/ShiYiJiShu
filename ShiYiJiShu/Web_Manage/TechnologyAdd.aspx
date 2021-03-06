﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechnologyAdd.aspx.cs" Inherits="ShiYiJiShu.web_manage.TechnologyAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
     <link href="css/style.css" type="text/css" rel="stylesheet" />
    <script src="editor/kindeditor.js" charset="utf-8" type="text/javascript" ></script>
    <script src="editor/lang/zh_CN.js" charset="utf-8" type="text/javascript" ></script>
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/formValidator.js" type="text/javascript"></script>
    <script src="js/formValidatorRegex.js" type="text/javascript"></script>

    <script src="js/artDialog.js?skin=idialog" type="text/javascript"></script>
    <script src="js/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="js/plugins/iframeTools.source.js" type="text/javascript"></script>
    
    <script type="text/javascript">
        var editor;
        KindEditor.ready(function (K) {
            editor = K.create('#txtDepartmentIntro', {
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

            ValidateInput($("#txtDepartmentName"), "spDepartmentName", "请输入名称，不超过200个字符！", " &nbsp; ", 1, 200, "名称不能为空并且不能大于200个字符！");
            ValidateInput($("#txtKeyword"), "spKeyword", "请输入关键词，不超过200个字符！", " &nbsp; ", 1, 200, "关键词不能为空并且不能大于200个字符！");
            ValidateInput($("#txtDescription"), "spDescription", "请输入描述，不超过500个字符！", " &nbsp; ", 1, 500, "描述不能为空并且不能大于500个字符！");
        });

        function ValidateInput(obj, tipName, onfocusText, oncorrectText, minLength, maxLength, onerrorText) {
            $(obj).formValidator({ tipid: tipName, onfocus: onfocusText, oncorrect: oncorrectText }).inputValidator({ min: minLength, max: maxLength, onerror: onerrorText });
        }

       function UploadPic() {
            art.dialog.open('UploadPic.aspx', { title: '图片上传', width: 520, height: 250, padding: 0, drag: true }, false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="right">
            <div class="top">
                <div class="title">
                    <asp:Label ID="lbName" runat="server" Text=""></asp:Label> 添加
                </div>
                <div class="rimg">
                    <img src="images/admin_12.gif" alt="" /></div>
            </div>
            <table border="0" cellpadding="0" cellspacing="1" class="tb2">
                <tr>
                    <td class="l" style="width: 80px"><span class="red">*</span> 技术名称：</td>
                    <td class="r">
                        <asp:TextBox ID="txtTechnologyName" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                        <span id="spTechnologyName" class="red"></span>
                    </td>
                </tr>
                <tr>
                    <td class="l" style="width: 80px"><span class="red">*</span> 技术链接：</td>
                    <td class="r">
                        <asp:TextBox ID="txtTechnologyLink" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                        <span id="spTechnologyLink" class="red"></span>
                    </td>
                </tr>
                <tr>
                    <td class="l" style="width: 80px"><span class="red">*</span> 单位名称：</td>
                    <td class="r">
                        <asp:TextBox ID="txtCompanyName" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                        <span id="sbcompanyName" class="red"></span>
                    </td>
                </tr>
                <tr>
                    <td class="l" style="width: 80px"><span class="red">*</span> 单位链接：</td>
                    <td class="r">
                        <asp:TextBox ID="txtCompanyLink" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox>
                        <span id="sbcompanyLink" class="red"></span>
                    </td>
                </tr>
                <tr>
                    <td class="l" style="width: 80px">置顶：</td>
                    <td class="r">
                        <asp:CheckBox ID="cbZhiDing" runat="server" Text="" />
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
