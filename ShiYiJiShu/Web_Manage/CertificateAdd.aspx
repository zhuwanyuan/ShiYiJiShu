<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CertificateAdd.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.CertificateAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
            editor = K.create('#txtCertSample', {
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

            ValidateInput($("#txtNewsTitle"), "spNewsTitle", "请输入名称，不超过200个字符！", " &nbsp; ", 1, 200, "名称不能为空并且不能大于200个字符！");
            ValidateInput($("#txtKeyword"), "spKeyword", "请输入名称，不超过200个字符！", " &nbsp; ", 1, 200, "名称不能为空并且不能大于200个字符！");
            ValidateInput($("#txtDescription"), "spDescription", "请输入名称，不超过500个字符！", " &nbsp; ", 1, 500, "名称不能为空并且不能大于500个字符！");
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
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text=""></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 姓名：</td>
            <td class="r">
                <asp:TextBox ID="txtName" runat="server" CssClass="txtbox w100"></asp:TextBox> <span id="spNewsTitle" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 查询类别：</td>
            <td class="r">
                <asp:DropDownList ID="DDLSearchType" runat="server">
                    <asp:ListItem Value="0">理事证书</asp:ListItem>
                    <asp:ListItem Value="1">培训证书</asp:ListItem>
                    <asp:ListItem Value="2">单位会员证书</asp:ListItem>
                </asp:DropDownList>
               </td>
        </tr>
         <tr>
            <td class="l"> <span class="red">*</span> 照片：</td>
            <td class="r">
                <input type="hidden" id="hidSmallPic" runat="server" />
               <img id="img" runat="server" src="images/nopic01.jpg" alt="" style=" width:110px; height:140px; border:#d6b87c 1px solid;"  />
               &nbsp; <input type="button" onclick="javascript: UploadPic()" class="btn" value="上传" /> 【图片尺寸：110px × 140px】
            </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 身份证号：</td>
            <td class="r">
                <asp:TextBox ID="txtIDNumber" runat="server" CssClass="txtbox w200"></asp:TextBox> <span id="spNewsTitle" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 证书编号：</td>
            <td class="r">
                <asp:TextBox ID="txtCertNo" runat="server" CssClass="txtbox w200"></asp:TextBox> <span id="spNewsTitle" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 职务：</td>
            <td class="r">
                <asp:TextBox ID="txtJobTitle" runat="server" CssClass="txtbox w100"></asp:TextBox> <span id="spNewsTitle" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l"> <span class="red">*</span> 证书样本：</td>
            <td class="r">
               <textarea id="txtCertSample" runat="server" name="content" rows="100" cols="500" style="width:99%; height:450px"> </textarea>
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
