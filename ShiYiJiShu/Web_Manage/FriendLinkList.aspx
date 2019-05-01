<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="FriendLinkList.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.FriendLinkList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
    <script src="js/jquery-1.8.3.min.js" type="text/javascript"></script>
    <script src="js/formValidator.js" type="text/javascript"></script>
    <script src="js/formValidatorRegex.js" type="text/javascript"></script>
    <script src="js/artDialog.js?skin=idialog" type="text/javascript"></script>
    <script src="js/plugins/iframeTools.js" type="text/javascript"></script>
    <script src="js/plugins/iframeTools.source.js" type="text/javascript"></script>

    <script type="text/javascript">
        $(document).ready(function () {
            $("#DDLLinkType").change(function () {
                if (this.value == 2) {
                    $("#trLogo").show();
                }
                else {
                    $("#trLogo").hide();
                }
            })

            if ($("#DDLLinkType").val()==2) {
                $("#trLogo").show();
            }
            else {
                $("#trLogo").hide();
            }
 
        })

        function CheckData() {
            var picTitle = document.getElementById("txtWebName");
            if (picTitle.value == "") {
                alert("请输入网站名称！");
                picTitle.focus();
                return false;
            }

            var picLink = document.getElementById("txtWebLink");
            if (picLink.value == "") {
                alert("请输入图片链接！");
                picLink.focus();
                return false;
            }
            return true;
        }

        function UploadPic() {
            art.dialog.open('UploadPic.aspx', { title: '图片上传', width: 520, height: 250, padding: 0, drag: true }, false);
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
<div class="right">
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="合作网站"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
    <table border="0" cellpadding="0" cellspacing="1" class="tb2">
       <%-- <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 类型：</td>
            <td class="r">
                <asp:DropDownList ID="DDLLinkType" runat="server">
                    <asp:ListItem Value="1">友情链接</asp:ListItem>
                    <asp:ListItem Value="2">合作网站</asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>--%>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 网站名称：</td>
            <td class="r">
                <asp:TextBox ID="txtWebName" runat="server" Width="200px" CssClass="txtbox"></asp:TextBox> <span id="spPicTitle" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> <span class="red">*</span> 网站链接：</td>
            <td class="r">
                <asp:TextBox ID="txtWebLink" runat="server" Width="200px" CssClass="txtbox"></asp:TextBox> （以“http://”开头）<span id="spPicLink" class="red"></span>
               </td>
        </tr>
        <tr>
            <td class="l" style=" width:80px"> LOGO：</td>
            <td class="r">
                <input type="hidden" id="hidSmallPic" runat="server" />
                <img id="img" runat="server" src="images/nopic01.jpg" alt="" style=" width:140px; height:50px; border:#d6b87c 1px solid;"  />
                &nbsp; <input type="button" onclick="javascript: UploadPic()" class="btn" value="上传" /> 【图片尺寸：140px × 50px】
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
                    <table  border="0" cellpadding="0" cellspacing="1" class="tb">
                        <tr>
                            <td class="head" style="width:50px">ID</td>
                            <td class="head" >网站名称</td>
                            <td class="head" >网站链接</td>
                            <td class="head w80" >操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" align="center">
                            <asp:Label ID="lbLinkID" runat="server" Text='<%#Eval("LinkID") %>'></asp:Label>
                        </td>
                        <td class="td1">
                          <p> <%#Eval("LinkName")%> </p>
                        </td>  
                        <td class="td1">
                          <p><%#Eval("LinkURL")%> </p>
                        </td>   
                        <td class="td1" align="center">
                                <a href='FriendLinkList.aspx?LinkID=<%#Eval("LinkID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                 <tr>
                        <td class="td2" align="center">
                            <asp:Label ID="lbLinkID" runat="server" Text='<%#Eval("LinkID") %>'></asp:Label>
                        </td>
                        <td class="td2">
                          <p> <%#Eval("LinkName")%> </p>
                        </td>      
                        <td class="td2">
                          <p><%#Eval("LinkURL")%> </p>
                        </td>  
                        <td class="td2" align="center">
                                <a href='FriendLinkList.aspx?LinkID=<%#Eval("LinkID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
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
