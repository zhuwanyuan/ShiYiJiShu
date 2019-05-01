<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TechnologyList.aspx.cs" Inherits="ShiYiJiShu.web_manage.TechnologyList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="right">
            <div class="top">
                <div class="title">
                    <asp:Label ID="title" runat="server" Text=""></asp:Label></div>  <div class="rimg"><img src="images/admin_12.gif" alt="" /></div>
            </div>

            <div class="path">
                <span class="fl">
                    <asp:TextBox ID="txtKey" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox>
                    &nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn" OnClientClick="return CheckData();" OnClick="btnSearch_Click" /></span>
            </div>

            <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand">
                <HeaderTemplate>
                    <table border="0" cellpadding="0" cellspacing="1" class="tb">
                        <tr>
                            <td class="head" style="width: 50px">ID</td>
                            <td class="head">技术名称</td>
                            <td class="head">单位名称</td>
                            <td class="head" style="width: 100px">操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1">
                            <asp:Label ID="TechnologyID" runat="server" Text='<%#Eval("TechID") %>'></asp:Label>
                        </td>
                        <td class="td1">
                            <p>
                                <a href="<%#Eval("TechNameLink")%>" target="_blank"><%#Eval("TechName")%> </a>
                            </p>
                        </td>
                        <td class="td1">
                            <p>
                                <a href="<%#Eval("TechCompanyLink")%>" target="_blank"><%#Eval("TechCompany")%></a>
                            </p>
                        </td>
                        <td class="td1">
                            <a href='TechnologyAdd.aspx?TechnologyID=<%#Eval("TechID") %>&classid=<%#Eval("ClassID") %>'>
                                <img src="images/toolbar_edit.gif" alt="修改" /></a>&nbsp; &nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');" />
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                      <tr>
                        <td class="td2">
                            <asp:Label ID="TechnologyID" runat="server" Text='<%#Eval("TechID") %>'></asp:Label>
                        </td>
                        <td class="td2">
                            <p>
                                <a href="<%#Eval("TechNameLink")%>" target="_blank"><%#Eval("TechName")%> </a>
                            </p>
                        </td>
                        <td class="td2">
                            <p>
                                <a href="<%#Eval("TechCompanyLink")%>" target="_blank"><%#Eval("TechCompany")%></a>
                            </p>
                        </td>
                        <td class="td2">
                            <a href='TechnologyAdd.aspx?TechnologyID=<%#Eval("TechID") %>&classid=<%#Eval("ClassID") %>'>
                                <img src="images/toolbar_edit.gif" alt="修改" /></a>&nbsp; &nbsp;&nbsp;<asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');" />
                        </td>
                    </tr>
                </AlternatingItemTemplate>
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
            <div class="search">
                <asp:Label ID="lbResult" runat="server" Text=""></asp:Label>
            </div>
            <div class="pager" id="pager" runat="server"></div>
            <div class="clear"></div>

        </div>
    </form>
</body>
</html>
