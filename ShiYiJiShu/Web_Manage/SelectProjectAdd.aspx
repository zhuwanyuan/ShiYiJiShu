<%@ Page Language="C#" AutoEventWireup="true" ValidateRequest="false" CodeBehind="SelectProjectAdd.aspx.cs" Inherits="ShiYiJiShu.web_manage.SelectProjectAdd" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/style.css?v=2" type="text/css" rel="stylesheet" />
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
            editor = K.create('#txtJiDiIntro', {
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

            //验证专家名称
            $("#txtJiDiName").formValidator({ tipid: "spJiDiName", onfocus: "请输入基地名称！", oncorrect: " &nbsp; " }).inputValidator({ min: 1, max: 200, onerror: "基地名称不能为空！" });
            $("#txtProjectIntro").formValidator({ tipid: "spProjectIntro", onfocus: "请输入项目介绍!", oncorrect: " &nbsp; " }).inputValidator({ min: 1, max: 140, onerror: "项目介绍不能超过70个字！" });
 
        });

    </script>

    <script type="text/javascript">
        function UploadPic() {
            art.dialog.open('UploadPic.aspx', { title: '图片上传', width: 520, height: 250, padding: 0, drag: true }, false);
        }

      
    </script>
</head>
<body>
    <form id="form1" runat="server">
<div class="right">
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="入选项目添加"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
          <tr>
            <td class="l" style=" width:90px"> <span class="red">*</span> 项目类别：</td>
            <td class="r">
                
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList> &nbsp;
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
                
            </td>
        </tr>
        
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 项目名称：</td>
            <td class="r">
                <asp:TextBox ID="txtProjectName" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spJiDiName" class="red"></span>
               </td>
        </tr>
         <tr>
            <td class="l" style=" width:80px"> 申报单位：</td>
            <td class="r">
                <asp:TextBox ID="txtProjectCompany" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spJiDiCompany" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> 项目负责人：</td>
            <td class="r">
                <asp:TextBox ID="txtProjectLeader" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spJiDiLeader"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> 项目介绍：</td>
            <td class="r">
                <asp:TextBox ID="txtProjectIntro" runat="server" Width="500px" autoHeight="true" Rows="6" TextMode="MultiLine"></asp:TextBox> <span id="spProjectIntro"></span>  (最多输入500个文字)
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> 缩略图：</td>
            <td class="r">
                <input type="hidden" id="hidSmallPic" runat="server" />
                <img id="img" runat="server" src="images/nopic02.jpg" alt="" style=" border:#d6b87c 1px solid; width:125px; height:95px"  /> &nbsp;
                <input type="button" onclick="javascript: UploadPic()" class="btn" value="上传" /> &nbsp; [图片尺寸：160*110]
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 项目链接：</td>
            <td class="r">
               <asp:TextBox ID="txtProjectLink" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spJiDiLink"></span>
               </td>
        </tr>
        
        <tr>
            <td class="l"> 推荐：</td>
            <td class="r">
                <asp:CheckBox ID="cbTuiJian" runat="server" />
            </td>
        </tr> 
        
         <tr>
            <td class="l"> 置顶：</td>
            <td class="r">
                <asp:CheckBox ID="cbZhiDing" runat="server" />
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
