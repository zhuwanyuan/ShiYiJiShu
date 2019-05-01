<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="ShiYiJiShu.Web_Manage.index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>网站后台管理系统</title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
</head>
<frameset rows="124,*" frameborder="no" border="0" framespacing="0" scrolling="no">
  <frame src="top.aspx" name="topFrame" scrolling="No" noresize="noresize" id="topFrame" title="topFrame" />
  <frameset cols="200,*" scrolling="no">
    <frame src="left.aspx" name="leftFrame" id="leftFrame" title="leftFrame" scrolling="no"/>
    <frame src="Home.aspx" name="mainFrame" id="mainFrame" title="mainFrame"  />
  </frameset>
  
  </frameset>
<noframes></noframes>
<body></body>
</html>
