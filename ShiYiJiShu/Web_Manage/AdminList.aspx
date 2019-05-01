<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AdminList.aspx.cs" Inherits="ShiYiJiShu.web_manage.AdminList" %>

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
            editor = K.create('#txtDoctorIntro', {
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


            editor2 = K.create('#txtDoctorMajor', {
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

            editor3 = K.create('#txtDoctorAchievement', {
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
 
    </script>
 
</head>
<body>
    <form id="form1" runat="server">
<div class="right">
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="一级类别管理"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>
 
    <asp:Repeater ID="repAdminList" runat="server" OnItemCommand="repAdminList_ItemCommand" >
                <HeaderTemplate>
                    <table  border="0" cellpadding="0" cellspacing="1" class="tb">
                        <tr>
                            <td class="head" style="width:50px">ID</td>
                            <td class="head">用户名</td>
                            <td class="head w80">操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" align="center">
                            <asp:Label ID="lbUserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                        </td>
                        <td class="td1">
                          <p> <%#Eval("UserName")%> </p>
                        </td>  
                        
                        <td class="td1" align="center">
                                <a href='AdminAdd.aspx?userid=<%#Eval("UserID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                 <tr>
                        <td class="td1" align="center">
                            <asp:Label ID="lbUserID" runat="server" Text='<%#Eval("UserID") %>'></asp:Label>
                        </td>
                        <td class="td1">
                          <p> <%#Eval("UserName")%> </p>
                        </td>  
                        
                        <td class="td1" align="center">
                                <a href='AdminAdd.aspx?userid=<%#Eval("UserID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
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
