<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info.aspx.cs" Inherits="ShiYiJiShu.Web_Master.Info" %>

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

            //验证关键词
            $("#txtKeyword").formValidator({ tipid: "spKeyword", onfocus: "请输入关键词，不超过200个字符！", oncorrect: " &nbsp; " }).inputValidator({ min: 1, max: 200, onerror: "关键词不能为空并且不能大于200个字符！" });

            //验证描述
            $("#txtDescription").formValidator({ tipid: "spDescription", onfocus: "请输入描述，不超过500个字符！", oncorrect: " &nbsp; " }).inputValidator({ min: 1, max: 200, onerror: "描述不能为空并且不能大于500个字符！" });

        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text=""></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
 
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 关键词：</td>
            <td class="r">
                <asp:TextBox ID="txtKeyword" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spKeyword"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 描 述：</td>
            <td class="r">
                <asp:TextBox ID="txtDescription" runat="server" Width="500px" Height="60px" CssClass="txtbox" TextMode="MultiLine"></asp:TextBox> <span id="spDescription"></span>
               </td>
        </tr>
        <tr>
            <td class="l"> <span class="red">*</span> 内 &nbsp; &nbsp; 容：</td>
            <td class="r">
               <textarea id="txtContent" runat="server" name="content" rows="100" cols="500" style="width:99%; height:450px"> </textarea>
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
