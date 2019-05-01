<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteStaffAdd.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.VoteStaffAdd" ValidateRequest="false" %>

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
            editor = K.create('#txtStaffDetail', {
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
            ValidateInput($("#txtTechName"), "spTechName", "请输入技术名称，不超过200个字符！", " &nbsp; ", 1, 200, "技术名称不能为空并且不能大于200个字符！");
            ValidateInput($("#txtStaffName"), "spStaffName", "请输入姓名，不超过200个字符！", " &nbsp; ", 1, 200, "姓名不能为空并且不能大于200个字符！");
            ValidateInput($("#txtJobTitle"), "spJobTitle", "请输入职位，不超过200个字符！", " &nbsp; ", 1, 200, "职位不能为空并且不能大于200个字符！");
             ValidateInput($("#txtCompany"), "spCompany", "请输入公司，不超过200个字符！", " &nbsp; ", 1, 200, "公司不能为空并且不能大于200个字符！");
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
            <td class="l" style=" width:80px"> <span class="red">*</span> 项目类别：</td>
            <td class="r">
                
                <asp:DropDownList ID="DropDownList1" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged" AutoPostBack="True">
                </asp:DropDownList> &nbsp;
                <asp:DropDownList ID="DropDownList2" runat="server">
                </asp:DropDownList>
                
            </td>
        </tr>
         <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 技术名称：</td>
            <td class="r">
                <asp:TextBox ID="txtTechName" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spTechName" class="red"></span>
               </td>
        </tr>
       <%-- <tr>
            <td class="l" style=" width:100px"> <span class="red">*</span> 姓名：</td>
            <td class="r">
                <asp:TextBox ID="txtStaffName" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spStaffName" class="red"></span>
               </td>
        </tr>
         <tr>
            <td class="l" style=" width:100px"> <span class="red">*</span> 职位：</td>
            <td class="r">
                <asp:TextBox ID="txtJobTitle" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spJobTitle" class="red"></span>
               </td>
        </tr>
         <tr>
            <td class="l" style=" width:100px"> <span class="red">*</span> 所在单位：</td>
            <td class="r">
                <asp:TextBox ID="txtCompany" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spCompany" class="red"></span>
               </td>
        </tr>--%>
         <tr>
            <td class="l" style=" width:100px"> <span class="red">*</span> 详情链接：</td>
            <td class="r">
                <asp:TextBox ID="txtLinkUrl" runat="server" Width="500px" CssClass="txtbox"></asp:TextBox> <span id="spLinkUrl" class="red"></span>
               </td>
        </tr>
         <tr>
            <td class="l"> <span class="red">*</span> 图片：</td>
            <td class="r">
                <input type="hidden" id="hidSmallPic" runat="server" />
               <img id="img" runat="server" src="images/nopic01.jpg" alt="" style=" width:120px; height:80px; border:#d6b87c 1px solid;"  />
               &nbsp; <input type="button" onclick="javascript: UploadPic()" class="btn" value="上传" /> 【图片尺寸：170px × 130px】
            </td>
        </tr>
   
        <tr>
            <td class="l"> <span class="red">*</span> 简介：</td>
            <td class="r">
               <textarea id="txtStaffDetail" runat="server" name="StaffDetail" rows="100"  cols="300" style="width:727px; height:450px"> </textarea>
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
