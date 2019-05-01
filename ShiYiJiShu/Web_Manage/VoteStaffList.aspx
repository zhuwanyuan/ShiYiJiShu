<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VoteStaffList.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.VoteStaffList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
<div class="right">
	<div class="top">
    <div class="title"><asp:Label ID="title" runat="server" Text="投票管理"></asp:Label></div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div>
    </div>
      
     <div class="path">
     <span class="fl"><asp:TextBox ID="txtKey" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox> &nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn" OnClientClick="return CheckData();" OnClick="btnSearch_Click" /></span>
     </div>

     <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" >
                <HeaderTemplate>
                    <table  border="0" cellpadding="0" cellspacing="1" class="tbProduct">
                        <tr>
                            <td class="head" style="width:50px">ID</td>
                            <td class="head" >技术名称</td>
                          
                            <td class="head w80" >操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" align="center">
                            <asp:Label ID="lbStaffID" runat="server" Text='<%#Eval("StaffID") %>'></asp:Label>
                        </td>
                        <td class="td1" style="padding:6px;">
                          <p><img src='/uploadpic/<%#Eval("StaffPhoto")%>' alt="" width="60"  />  &nbsp;  <%#Eval("TechName")%>
                          </p>
                        </td> 
            
                                                         
                        <td class="td1" align="center">
                            <a href='VoteStaffAdd.aspx?staffid=<%#Eval("StaffID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
              <tr>
                        <td class="td2" align="center">
                            <asp:Label ID="lbStaffID" runat="server" Text='<%#Eval("StaffID") %>'></asp:Label>
                        </td>
                        <td class="td2" style="padding:6px;">
                          <p><img src='/uploadpic/<%#Eval("StaffPhoto")%>' alt="" width="60"  /> &nbsp;  <%#Eval("TechName")%>
                          </p>
                        </td> 
                                                     
                        <td class="td2" align="center">
                            <a href='VoteStaffAdd.aspx?staffid=<%#Eval("StaffID") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </AlternatingItemTemplate>             
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
           <div class="search">
               <asp:Label ID="lbResult" runat="server" Text=""></asp:Label>  </div>
           <div class="pager" id="pager" runat="server"> </div>
           <div class="clear"></div>
           <br/>
</div>
</form>
</body>
</html>
