<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="JiDiList.aspx.cs" Inherits="ShiYiJiShu.web_manage.JiDiList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <link href="css/style.css" type="text/css" rel="stylesheet" />
</head>
<body>
   <form id="form1" runat="server">
<div class="right">
	<div class="top">
    <div class="title"><asp:Label ID="title" runat="server" Text="基地管理"></asp:Label></div><div class="rimg"><img src="images/admin_12.gif" alt="" /></div>
    </div>
      
     <div class="path">
     <span class="fl"><asp:TextBox ID="txtKey" runat="server" CssClass="txtbox" Width="200px"></asp:TextBox> &nbsp;<asp:Button ID="btnSearch" runat="server" Text="搜索" CssClass="btn" OnClientClick="return CheckData();" OnClick="btnSearch_Click" /></span>
     </div>

     <asp:Repeater ID="Repeater1" runat="server" OnItemCommand="Repeater1_ItemCommand" >
                <HeaderTemplate>
                    <table  border="0" cellpadding="0" cellspacing="1" class="tbProduct">
                        <tr>
                            <td class="head w30"><input type="checkbox" name="checkall" id="checkall" /></td>
                            <td class="head" style="width:50px">ID</td>
                            <td class="head" >基地名称</td>
                            <td class="head" >共建单位</td>
                            <td class="head" style=" width:100px">审核</td>
                            <td class="head w80" >操作</td>
                        </tr>
                </HeaderTemplate>
                <ItemTemplate>
                    <tr>
                        <td class="td1" style=" height:85px"><input type="checkbox" name="checkbox" value="<%#Eval("JiDiId")%>" /> </td>
                        <td class="td1" align="center">
                            <asp:Label ID="lbJiDiId" runat="server" Text='<%#Eval("JiDiId") %>'></asp:Label>
                        </td>
                        <td class="td1">
                          <p><%#CheckShenHe(Eval("ActiveFlag").ToString()) %> &nbsp; <a href="../JiDi/Detail/<%#Eval("JiDiId") %>" target="_blank"><img src='/uploadpic/<%#Eval("JiDiPic")%>' alt="" width="60"  /></a> &nbsp;  
                          <a href='../JiDi/Detail/<%#Eval("JiDiId") %>' target="_blank"><%#Eval("JiDiName")%></a> &nbsp; <%#CheckTuiJian(Eval("TuiJian").ToString()) %>
                          </p>
                        </td> 
                        <td class="td1" style=" height:85px"><%#Eval("JiDiCompany")%></td>

                        <td class="td1" runat="server" visible="<%#CheckAdminDisplay()%>" align="center">
                           
                              <asp:LinkButton ID="btnApprove" CommandName="approve" runat="server" OnClientClick="javascript:return confirm('您确定要通过审核吗？');" CssClass="red" Visible='<%#CheckApproveButtonShow(Convert.ToInt32(Eval("activeflag"))) %>' Text="">审核</asp:LinkButton>
                              <asp:LinkButton ID="btnUnApprove" CommandName="unapprove" runat="server" OnClientClick="javascript:return confirm('您确定要取消审核吗？');" CssClass="green" Visible='<%#CheckUnApproveButtonShow(Convert.ToInt32(Eval("activeflag"))) %>'>取消审核</asp:LinkButton>
                          
                        </td>

                        <td class="td1" runat="server" visible="<%#CheckMemberDisplay()%>">
                          <p><%#CheckShenHe(Eval("ActiveFlag").ToString()) %></p>
                        </td>                   
                        <td class="td1" align="center">
                            <a href='JiDiAdd.aspx?JiDiId=<%#Eval("JiDiId") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </ItemTemplate>
                <AlternatingItemTemplate>
                <tr>
                        <td class="td2" style=" height:85px"><input type="checkbox" name="checkbox" value="<%#Eval("JiDiId")%>" /> </td>
                        <td class="td2" align="center">
                            <asp:Label ID="lbJiDiId" runat="server" Text='<%#Eval("JiDiId") %>'></asp:Label>
                        </td>
                        <td class="td2">
                          <p><%#CheckShenHe(Eval("ActiveFlag").ToString()) %> &nbsp; <a href="../JiDi/Detail/<%#Eval("JiDiId") %>" target="_blank"><img src='/uploadpic/<%#Eval("JiDiPic")%>' alt="" width="60"  /></a> &nbsp;  
                          <a href='../JiDi/Detail/<%#Eval("JiDiId") %>' target="_blank"><%#Eval("JiDiName")%></a> &nbsp; <%#CheckTuiJian(Eval("TuiJian").ToString()) %>
                          </p>
                        </td> 
                        <td class="td2" style=" height:85px"><%#Eval("JiDiCompany")%></td>

                        <td class="td2" runat="server" visible="<%#CheckAdminDisplay()%>" align="center">
                           
                              <asp:LinkButton ID="btnApprove" CommandName="approve" runat="server" OnClientClick="javascript:return confirm('您确定要通过审核吗？');" CssClass="red" Visible='<%#CheckApproveButtonShow(Convert.ToInt32(Eval("activeflag"))) %>' Text="">审核</asp:LinkButton>
                              <asp:LinkButton ID="btnUnApprove" CommandName="unapprove" runat="server" OnClientClick="javascript:return confirm('您确定要取消审核吗？');" CssClass="green" Visible='<%#CheckUnApproveButtonShow(Convert.ToInt32(Eval("activeflag"))) %>'>取消审核</asp:LinkButton>
                          
                        </td>

                        <td class="td2" runat="server" visible="<%#CheckMemberDisplay()%>">
                          <p><%#CheckShenHe(Eval("ActiveFlag").ToString()) %></p>
                        </td>                   
                        <td class="td1" align="center">
                            <a href='JiDiAdd.aspx?JiDiId=<%#Eval("JiDiId") %>'><img src="images/toolbar_edit.gif" alt="修改" style="border:0" /></a> &nbsp; <asp:ImageButton ID="ImageButton1" CommandName="delete" runat="server" ImageUrl="images/toolbar_del.gif" OnClientClick="javascript:return confirm('您确定要删除么，删除后不可恢复！');"/>
                        </td>
                    </tr>
                </AlternatingItemTemplate>             
                <FooterTemplate>
                    </table>
                </FooterTemplate>
            </asp:Repeater>
          
           <div class="pager" id="pager" runat="server"> </div>
           <div class="clear"></div>

           <div class="search">
               <asp:Label ID="lbResult" runat="server" Text=""></asp:Label>  
           </div>
</div>
</form>
</body>
</html>
