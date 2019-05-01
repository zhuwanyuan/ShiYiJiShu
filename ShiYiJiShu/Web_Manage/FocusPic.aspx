<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FocusPic.aspx.cs" Inherits="ShiYiJiShu.web_manage._FocusPic" %>

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
        function CheckData() {
            var picTitle = document.getElementById("txtPicTitle");
            if (picTitle.value == "") {
                alert("请输入图片标题！");
                picTitle.focus();
                return false;
            }

            var picLink = document.getElementById("txtPicLink");
            if (picLink.value == "") {
                alert("请输入图片链接！");
                picLink.focus();
                return false;
            }

            var hidPic = document.getElementById("hidPic");
            if (hidPic.value == "") {
                alert("请上传图片！");
                hidPic.focus();
                return false;
            }

            return true;
        }
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
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="焦点图管理"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 图片标题：</td>
            <td class="r">
                <asp:TextBox ID="txtPicTitle" runat="server" Width="400px" CssClass="txtbox"></asp:TextBox> <span id="spPicTitle" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 图片链接：</td>
            <td class="r">
                <asp:TextBox ID="txtPicLink" runat="server" Width="400px" CssClass="txtbox"></asp:TextBox> <span id="spPicLink" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 图片上传：</td>
            <td class="r">
                  <input type="hidden" id="hidSmallPic" runat="server" />
                <img id="img" runat="server" src="images/nopic01.jpg" alt="" style=" border:#d6b87c 1px solid;"  /> &nbsp;
                <input type="button" onclick="javascript: UploadPic()" class="btn" value="上传" /> 【图片尺寸：380px × 270px】
               </td>
        </tr>
        <tr>
            <td class="l">
            </td>
            <td class="r"> 
                <asp:Button ID="Button1" runat="server" Text="提交" OnClientClick="return CheckData();" OnClick="Button1_Click" CssClass="btn" /></td>
        </tr>
    </table>
 
    <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" >
                <HeaderTemplate>
                    <table  border="0" cellpadding="0" cellspacing="1" class="tbProduct">
                        <tr>
                            <td class="head" style="width:50px">ID</td>
                            <td class="head" >图片标题</td>
                            <td class="head" >图片链接</td>
                            <td class="head w80" >操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" align="center">
                            <asp:Label ID="lbPicID" runat="server" Text='<%#Eval("PicID") %>'></asp:Label>
                        </td>
                        <td class="td1">
                          <p><img src='/uploadpic/<%#Eval("PicName")%>' alt="" width="60px"  /> &nbsp; <%#Eval("PicTitle")%> </p>
                        </td>  
                        <td class="td1">
                          <p><%#Eval("PicLinks")%> </p>
                        </td>   
                        <td class="td1" align="center">
                                <a href='FocusPic.aspx?PicID=<%#Eval("PicID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                 <tr>
                        <td class="td2" align="center">
                            <asp:Label ID="lbPicID" runat="server" Text='<%#Eval("PicID") %>'></asp:Label>
                        </td>
                        <td class="td2">
                          <p><img src='/uploadpic/<%#Eval("PicName")%>' alt="" width="60px"  /> &nbsp; <%#Eval("PicTitle")%> </p>
                        </td>   
                        <td class="td2">
                          <p><%#Eval("PicLinks")%> </p>
                        </td>  
                        <td class="td2" align="center">
                                <a href='FocusPic.aspx?PicID=<%#Eval("PicID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            
</div>
</form>
</body>
</html>
