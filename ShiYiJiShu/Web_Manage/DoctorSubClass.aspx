<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DoctorSubClass.aspx.cs" Inherits="ShiYiJiShu.web_manage.DoctorSubClass" %>

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

        $(document).ready(function () {
            $.formValidator.initConfig({ formid: "form1", onerror: function (msg) { alert(msg) } });

            //验证专家名称
            $("#txtProjectName").formValidator({ tipid: "spProjectName", onfocus: "请输入项目名称！", oncorrect: " &nbsp; " }).inputValidator({ min: 1, max: 200, onerror: "项目名称不能为空！" });
 
        });

    </script>

    <script type="text/javascript">
        function UploadPic() {
            art.dialog.open('UploadPic.aspx', { title: '图片上传', width: 520, height: 250, padding: 0, drag: true }, false);
        }


        function CheckData() {
            var classname = document.getElementById("txtClassName");
            if (classname.value == "") {
                alert("请输入二级类别名称！");
                classname.focus();
                return false;
            }
 
            return true;
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
<div class="right">
<div class="top"><div class="title"><asp:Label ID="lbName" runat="server" Text="一级类别管理"></asp:Label> </div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div></div>

     <div class="addform">
        <asp:TextBox ID="txtClassName" runat="server" Width="300px" CssClass="txtbox" ></asp:TextBox> <asp:Button ID="Button1" runat="server" Text="添加" OnClientClick="return CheckData();" OnClick="Button1_Click" CssClass="btn" /> 
     </div>
 
    <asp:Repeater ID="repClassList" runat="server" OnItemCommand="repClassList_ItemCommand" >
                <HeaderTemplate>
                    <table  border="0" cellpadding="0" cellspacing="1" class="tb">
                        <tr>
                            <td class="head" style="width:50px">ID</td>
                            <td class="head" >类别名称</td>
                        
                            <td class="head w80" >操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" align="center">
                            <asp:Label ID="lbClassID" runat="server" Text='<%#Eval("ClassID") %>'></asp:Label>
                        </td>
                        <td class="td1">
                          <p> <%#Eval("ClassName")%> </p>
                        </td>  
                       
                        <td class="td1" align="center">
                                <a href='DoctorSubClass.aspx?classid=<%#Eval("ParentClassID") %>&subclassid=<%#Eval("ClassID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                 <tr>
                        <td class="td2" align="center">
                            <asp:Label ID="lbClassID" runat="server" Text='<%#Eval("ClassID") %>'></asp:Label>
                        </td>
                        <td class="td2">
                          <p> <%#Eval("ClassName")%> </p>
                        </td>  
                        
                        <td class="td2" align="center">
                                <a href='DoctorSubClass.aspx?classid=<%#Eval("ParentClassID") %>&subclassid=<%#Eval("ClassID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
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
