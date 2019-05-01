<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ProjectRegister.aspx.cs" Inherits="ShiYiJiShu.Web_Master.ProjectRegister_" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link href="css/style.css?v=1" type="text/css" rel="stylesheet" />
    <script type="text/javascript">
        function CheckData() {
            var key = document.getElementById("txtKey");
            if (key.value == "") {
                alert("请输入搜索关键字！");
                key.focus();
                return false;
            }
        }
    </script>
</head>
<body>
  <form id="form1" runat="server">
<div class="right">
	<div class="top">
    <div class="title"><asp:Label ID="title" runat="server" Text="报名信息"></asp:Label></div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div>
    </div>
      
     <div class="path">
        <span class="fl"><asp:TextBox ID="txtKey" runat="server" CssClass="txtbox" Width="300px"></asp:TextBox> &nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn" OnClientClick="return CheckData();" OnClick="btnSearch_Click" /></span>
     </div>

     <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" >
                <HeaderTemplate>
                    <table  border="0" cellpadding="0" cellspacing="1" class="tb">
                        <tr>
                            <td class="head" style="width:60px">ID</td>
                            <td class="head">姓名</td>
                            <td class="head">手机号码</td>
                            <td class="head">公司名称</td>
                            <td class="head">邮箱</td>
                            <td class="head" style=" width:100px">操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" align="center"><asp:Label ID="lbRegID" runat="server" Text='<%#Eval("RegID") %>'></asp:Label> </td>                       
                        <td class="td1" align="center"><%#Eval("UserName") %></td>
                        <td class="td1" align="center"><%#Eval("Mobile") %></td>
                        <td class="td1" align="center"><%#Eval("UserCompany") %></td>
                        <td class="td1" align="center"><%#Eval("UserEmail") %></td>
                        <td class="td1" align="center">
                              <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                    <tr>
                        <td class="td2" align="center"><asp:Label ID="lbRegID" runat="server" Text='<%#Eval("RegID") %>'></asp:Label> </td>                       
                        <td class="td2" align="center"><%#Eval("UserName") %></td>
                        <td class="td2" align="center"><%#Eval("Mobile") %></td>
                        <td class="td2" align="center"><%#Eval("UserCompany") %></td>
                        <td class="td2" align="center"><%#Eval("UserEmail") %></td>
                        <td class="td2" align="center">
                              <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>

           <div class="pager" id="pager" runat="server"> </div>
           <div class="clear"></div>
           <div class="search"><asp:Label ID="lbResult" runat="server" Text=""></asp:Label></div>
           
</div>
</form>
</body>
</html>
