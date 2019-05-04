<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Left.aspx.cs" Inherits="ShiYiJiShu.Web_Master.Left" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="css/style.css" type="text/css" rel="Stylesheet" />

<script language="javascript" type="text/javascript">
    function tupian(idt) {
        var nametu = "xiaotu" + idt;
        var tp = document.getElementById(nametu);
        tp.src = "images/ico05.gif"; //图片ico04为白色的正方形

        for (var i = 1; i < 200; i++) {
            var nametu2 = "xiaotu" + i;
            if (i != idt * 1) {
                var tp2 = document.getElementById('xiaotu' + i);
                if (tp2 != undefined)
                { tp2.src = "images/ico06.gif"; } //图片ico06为蓝色的正方形
            }
        }
    }

    function list(idstr) {
        var name1 = "subtree" + idstr;
        var name2 = "img" + idstr;
        var objectobj = document.getElementById(name1);
        var imgobj = document.getElementById(name2);

        if (objectobj.style.display == "none") {
            objectobj.style.display = "";
            imgobj.src = "images/ico03.gif";
        }
        else {
            objectobj.style.display = "none";
            imgobj.src = "images/ico04.gif";
        }
    }

</script>
</head>
<body>
    <form id="form1" runat="server">
    <div class="left">
  


    <div id="divGuanYuWoMen" runat="server">
    <div class="submenu" ><img id="img1" src="images/ico03.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('1');" hidefocus="true">关于我们</a></div>
    <div class="menu" id="subtree1">    
        <ul>
            <asp:Repeater ID="repGuanYuWoMen" runat="server">
                <ItemTemplate>
                    <li>
                        <img id="xiaotu<%#Eval("ClassID") %>" src='images/ico06.gif' alt='' />
                        <a href="Info.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');"><%#Eval("ClassName") %></a></li>
                </ItemTemplate>
            </asp:Repeater>

             <li><img id="xiaotu16" src='images/ico06.gif' alt='' />
                        <a href="Info.aspx?classid=16" target="mainFrame" onclick="tupian('16');">联系我们</a></li>
        </ul>
    </div>
    </div>    
 
    <div id="divZhuanJiaWeiYuanHui" runat="server">
        <div class="submenu mt5" ><img id="img25" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('25');" hidefocus="true">专家委员会</a></div>
        <div class="menu" id="subtree25" style=" display:none">    
            <ul>
                <li>
                            <img id="xiaotu25" src='images/ico06.gif' alt='' />
                            <a href="DoctorList.aspx" target="mainFrame" onclick="tupian('25');">专家委员</a> &nbsp; | 
                            <a href="DoctorAdd.aspx" target="mainFrame" onclick="tupian('25');">添加</a></li>
                <li  id="divZhuanJiaLeiBie" runat="server">
                            <img id="xiaotu99" src='images/ico06.gif' alt='' />
                            <a href="DoctorClass.aspx" target="mainFrame" onclick="tupian('99');">专家类别管理</a> </li>
            </ul>
        </div>
    </div>

    <div id="divZhiNengBuMen" runat="server">
        <div class="submenu mt5" ><img id="img28" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('28');" hidefocus="true">职能部门</a></div>
        <div class="menu" id="subtree28" style=" display:none">    
            <ul>
                <asp:Repeater ID="repZhiNengBuMen" runat="server">
                    <ItemTemplate>
                        <li>
                            <img id="xiaotu<%#Eval("ClassID") %>" src='images/ico06.gif' alt='' />
                            <a href="Info.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');"><%#Eval("ClassName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
            </ul>
        </div>
    </div>

    <div id="divWenZhangGuanLi" runat="server">
        <div class="submenu mt5" ><img id="img2" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('2');" hidefocus="true">文章管理</a></div>
        <div class="menu" id="subtree2" style=" display:none">    
            <ul>
                <asp:Repeater ID="repWenZhangGuanLi" runat="server">
                <ItemTemplate>
                    <li><img id="xiaotu<%#Eval("ClassID") %>" src='images/ico06.gif' alt='' /> <a href="List.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');"><%#Eval("ClassName") %></a> &nbsp; | 
                    <a href="NewsAdd.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');">添加</a></li>
                </ItemTemplate>
                </asp:Repeater>      
            </ul>
        </div>
    </div>


    <div id="divShiYiJiShu" runat="server">
        <div class="submenu mt5" ><img id="img6" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('6');" hidefocus="true">适宜技术</a></div>
        <div class="menu" id="subtree6" style=" display:none">    
            <ul>
                <asp:Repeater ID="repGongZuoJieShao" runat="server">
                    <ItemTemplate>
                        <li>
                            <img id="xiaotu<%#Eval("ClassID") %>" src='images/ico06.gif' alt='' />
                            <a href="Info.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');"><%#Eval("ClassName") %></a></li>
                    </ItemTemplate>
                </asp:Repeater>
 
                <asp:Repeater ID="repRuXuanJiShu" runat="server">
                <ItemTemplate>
                    <li><img id="xiaotu<%#Eval("ClassID") %>" src='images/ico06.gif' alt='' /> <a href="List.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');"><%#Eval("ClassName") %></a> &nbsp; | 
                    <a href="NewsAdd.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');">添加</a></li>
                </ItemTemplate>
                </asp:Repeater>      
            </ul>
        </div>
    </div>


    <div id="divHuiYuanZhongXin" runat="server">
        <div class="submenu mt5" ><img id="img9" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('9');" hidefocus="true">会员中心</a></div>
        <div class="menu" id="subtree9" style=" display:none">    
            <ul>
                <asp:Repeater ID="repHuiYuanZhongXin" runat="server">
                <ItemTemplate>
                    <li><img id="xiaotu<%#Eval("ClassID") %>" src='images/ico06.gif' alt='' /> <a href="List.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');"><%#Eval("ClassName") %></a> &nbsp; | 
                    <a href="NewsAdd.aspx?classid=<%#Eval("ClassID") %>" target="mainFrame" onclick="tupian('<%#Eval("ClassID") %>');">添加</a></li>
                </ItemTemplate>
                </asp:Repeater>      
            </ul>
        </div>
    </div>
  
    <div id="divXiangMuTuiGuang" runat="server">
        <div class="submenu mt5" ><img id="img54" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('54');" hidefocus="true">项目展示</a></div>
        <div class="menu" id="subtree54"  style=" display:none">    
            <ul>
                    <li><img id="xiaotu54" src='images/ico06.gif' alt='' /> <a href="ProjectList.aspx" target="mainFrame" onclick="tupian('54');">项目管理</a> &nbsp; | 
                        <a href="ProjectAdd.aspx" target="mainFrame" onclick="tupian('54');">添加</a></li>
                    <li id="divXiangMuLeiBie" runat="server">
                            <img id="xiaotu104" src='images/ico06.gif' alt='' />
                            <a href="NewsClass.aspx?firstclassid=19" target="mainFrame" onclick="tupian('104');">项目类别管理</a> </li>
            </ul>
        </div>
    </div>
 
    <div id="divShiPinZhongXin" runat="server">
        <div class="submenu mt5" ><img id="img11" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('11');" hidefocus="true">视频中心</a></div>
        <div class="menu" id="subtree11"  style=" display:none">    
            <ul>
                    <li><img id="xiaotu11" src='images/ico06.gif' alt='' /> <a href="VideoList.aspx" target="mainFrame" onclick="tupian('11');">视频管理</a> &nbsp; | 
                    <a href="VideoAdd.aspx" target="mainFrame" onclick="tupian('11');">添加</a></li>
                    <li id="divShiPinLeiBie" runat="server"> <img id="xiaotu110" src='images/ico06.gif' alt='' /> <a href="NewsClass.aspx?firstclassid=11" target="mainFrame" onclick="tupian('110');">视频类别管理</a> </li>
            </ul>
        </div>
    </div>

    <div id="divTouPiaoGuanLi" runat="server">
        <div class="submenu mt5" ><img id="img12" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('12');" hidefocus="true">投票管理</a></div>
        <div class="menu" id="subtree12"  style=" display:none">    
            <ul>
                    <li><img id="xiaotu13" src='images/ico06.gif' alt='' /> <a href="VoteStaffList.aspx" target="mainFrame" onclick="tupian('13');">投票管理</a> &nbsp; | 
                    <a href="VoteStaffAdd.aspx" target="mainFrame" onclick="tupian('912');">添加</a></li>

                    <li id="divTouPiaoLeiBie" runat="server"> <img id="xiaotu111" src='images/ico06.gif' alt='' /> <a href="NewsClass.aspx?firstclassid=13" target="mainFrame" onclick="tupian('111');">投票类别管理</a> </li>
            </ul>
        </div>
    </div>

    <div id="divJiDiJianShe" runat="server">
        <div class="submenu mt5" ><img id="img5" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('5');" hidefocus="true">基地建设</a></div>
        <div class="menu" id="subtree5"  style=" display:none">    
            <ul>
                    <li><img id="xiaotu5" src='images/ico06.gif' alt='' /> <a href="JiDiList.aspx" target="mainFrame" onclick="tupian('5');">基地管理</a> &nbsp; | 
                        <a href="JiDiAdd.aspx" target="mainFrame" onclick="tupian('5');">添加</a></li>
                    <li id="divJiDiLeiBie" runat="server">
                            <img id="xiaotu105" src='images/ico06.gif' alt='' />
                            <a href="NewsClass.aspx?firstclassid=5" target="mainFrame" onclick="tupian('105');">基地类别管理</a> </li>
            </ul>
        </div>
    </div>

    <div id="divZhengShuGuanLi" runat="server">
        <div class="submenu mt5" ><img id="img20" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('20');" hidefocus="true">证书管理</a></div>
        <div class="menu" id="subtree20"  style=" display:none">    
            <ul>
                    <li>
                        <img id="xiaotu20" src='images/ico06.gif' alt='' /> <a href="CertificateList.aspx" target="mainFrame" onclick="tupian('20');">证书管理</a>  &nbsp; | 
                        <a href="CertificateAdd.aspx" target="mainFrame" onclick="tupian('20');">添加</a>
                    </li>
            </ul>
        </div>
    </div>

    <div id="divWangZhanGuanLi" runat="server">
        <div class="submenu mt5" ><img id="img13" src="images/ico04.gif" alt="" /> <a href="javascript:void(0)" target="mainFrame" onclick="list('13');" hidefocus="true">网站管理</a></div>
        <div class="menu" id="subtree13"  style=" display:none">    
            <ul>
                <li><img id="xiaotu97" src='images/ico06.gif' alt='' /> <a href="EditPassword.aspx" target="mainFrame" onclick="tupian('97');">密码修改</a> </li>  
                <li><img id="xiaotu98" src='images/ico06.gif' alt='' /> <a href="FriendLinkList.aspx" target="mainFrame" onclick="tupian('98');">友情链接</a> </li>  
                <li><img id="xiaotu101" src='images/ico06.gif' alt='' /> <a href="FocusPic.aspx" target="mainFrame" onclick="tupian('101');">焦点图管理</a> </li>
                <li><img id="xiaotu102" src='images/ico06.gif' alt='' /> <a href="AdminList.aspx" target="mainFrame" onclick="tupian('102');">管理员管理</a> &nbsp; | <a href="AdminAdd.aspx" target="mainFrame" onclick="tupian('102');">添加</a></li>
            </ul>
        </div>
    </div>
    </div>
    </form>
</body>
</html>
