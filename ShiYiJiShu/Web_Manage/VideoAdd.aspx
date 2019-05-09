<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoAdd.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.VideoAdd" ValidateRequest="false" %>

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
            editor = K.create('#txtVideoIntro', {
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

            ValidateInput($("#txtVideoName"), "spVideoName", "请输入名称，不超过200个字符！", " &nbsp; ", 1, 200, "名称不能为空并且不能大于200个字符！");
            //ValidateInput($("#txtVideoUrl"), "spVideoUrl", "请输入视频地址！", " &nbsp; ", 1, 1000, "视频地址不能为空并且不能大于1000个字符！");
        });

        function ValidateInput(obj, tipName, onfocusText, oncorrectText, minLength, maxLength, onerrorText) {
            $(obj).formValidator({ tipid: tipName, onfocus: onfocusText, oncorrect: oncorrectText }).inputValidator({ min: minLength, max: maxLength, onerror: onerrorText });
        }

        function UploadPic() {
            art.dialog.open('UploadPic.aspx', { title: '图片上传', width: 520, height: 250, padding: 0, drag: true }, false);
        }

        function UploadFile() {
            art.dialog.open('UploadVideo.aspx', { title: '视频上传', width: 520, height: 250, padding: 0, drag: true }, false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="right">
    <div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="视频添加"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
         <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 视频类别：</td>
            <td class="r">
                
               <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList> &nbsp;
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
              
            </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 视频名称：</td>
            <td class="r">
                <asp:TextBox ID="txtVideoName" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spVideoName" class="red"></span>
               </td>
        </tr>
         <tr>
            <td class="l" style=" width:80px"> <%--<span class="red">*</span> --%>视频地址(选填)：</td>
            <td class="r">
                <asp:TextBox ID="txtVideoUrl" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spVideoUrl" class="red"></span> (可从视频网站复制分享的视频地址)
               </td>
        </tr>
         <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 缩略图：</td>
            <td class="r">
                <input type="hidden" id="hidSmallPic" runat="server" />
                <img id="img" src="images/nopic03.jpg" runat="server" style=" border:#d6b87c 1px solid; width:145px; height:115px" alt="" /> &nbsp;
                <input type="button" onclick="javascript: UploadPic()" class="btn" value="上传" /> &nbsp; [图片尺寸：160*110]
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 视频上传：</td>
            <td class="r">
              <input type="hidden" id="hidFileName" runat="server" />
              <asp:Label ID="spFileName" runat="server" Text=""></asp:Label> &nbsp;<input type="button" onclick="javascript: UploadFile()" class="btn" value="上传" /> &nbsp; 
            </td>
        </tr>
        <tr>
            <td class="l"> <span class="red">*</span> 视频简介：</td>
            <td class="r">
               <textarea id="txtVideoIntro" runat="server" name="content" rows="100" cols="300" style="width:727px; height:450px"> </textarea>
            </td>
        </tr>
         <tr>
            <td class="l" style=" width:80px"> 置顶：</td>
            <td class="r"><asp:CheckBox ID="cbZhiDing" runat="server" Text="置顶" />
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
