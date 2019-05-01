using System;
using System.Collections.Generic;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.IO;
using System.Net.Mail;
using System.Web;
 
namespace Common
{
    public class BaseClass
    {
        public BaseClass() { }

        //打开数据库
        public SqlConnection OpenConn()
        {
            SqlConnection conn = null;
            
            try
            {
                conn = new SqlConnection(System.Configuration.ConfigurationManager.AppSettings["connstr"]);
            }
            catch
            {
                System.Web.HttpContext.Current.Response.Write("数据库连接出错！");
            }

            conn.Open();
            
            return conn;
        }

        //关闭数据库
        public void CloseConn(SqlConnection conn)
        {
            if (conn != null)
            {
                conn.Close();
            }
        }

        //获取DataSet
        public DataSet GetDataSet(string sql)
        {
            SqlConnection conn = OpenConn();
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                ds = new DataSet();
                SqlCommand cmd = new SqlCommand(sql, conn);
                da = new SqlDataAdapter(cmd);
                da.Fill(ds);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConn(conn);
            }

            return ds;
        }

        //执行SQL语句
        public void ExecuteSQL(string sql)
        {
            SqlConnection conn = OpenConn();
            try
            {
                SqlCommand cmd = conn.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConn(conn);
            }
        }


        //执行SQL并返回数量
        public int ExecuteCommand(string sql)
        {
            SqlConnection conn = OpenConn();
            int count = 0;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConn(conn);
            }

            return count;
        }

        //根据SQL语句获得SqlDataReader
        public SqlDataReader GetDataReader(string sql)
        {
            SqlConnection conn = OpenConn();
            SqlDataReader reader = null;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                reader = cmd.ExecuteReader();
                
                return reader;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        //获取特定值
        public string GetSingle(string sql)
        {
            SqlConnection conn = OpenConn();
            string obj = string.Empty;

            try
            {
                SqlCommand cmd = new SqlCommand(sql, conn);
                obj = cmd.ExecuteScalar().ToString();
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                CloseConn(conn);
            }
 
            return obj;
        }

        //绑定Repeater
        public void BindRepeater(Repeater rep, string sql)
        {
            DataSet ds = GetDataSet(sql);
            rep.DataSource = ds.Tables[0].DefaultView;
            rep.DataBind();
        }

        //弹出对话框带跳转地址
        public void MessageBox(string message, string targetPage)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + message + "');location.href='" + targetPage + "'</script>");
        }

        //弹出对话框
        public void MessageBox1(string message)
        {
            System.Web.HttpContext.Current.Response.Write("<script>alert('" + message + "');history.go(-1)</script>");
        }

        //截取字符串
        public string CutString(string str, int length, int type)
        {
            string newString = "";
            if (str != "")
            {
                if (str.Length > length)
                {
                    if (type == 1) //(带省略号)
                    {
                        newString = str.Substring(0, length) + "...";
                    }
                    else if (type == 2) //不带省略号
                    {
                        newString = str.Substring(0, length);
                    }
                }
                else
                {
                    newString = str;
                }
            }
            return newString;
        }
 
        //检查管理员是否登录
        public bool CheckAdminLogin(Page page)
        {
            bool returnValue;

            if (page.Request.Cookies["AdminUser"] != null && page.Request.Cookies["AdminUser"]["userid"] != null)
            {
                returnValue = true;
            }
            else
            {
                returnValue = false;
            }

            return returnValue;
        }


        public int GetAdminUserID()
        {
            int userId = 0;
            if (System.Web.HttpContext.Current.Request.Cookies["AdminUser"] != null)
            {
                userId = Convert.ToInt32(System.Web.HttpContext.Current.Request.Cookies["AdminUser"]["userid"]);
            }

            return userId;
        }

        //获取管理员用户名
        public string GetAdminUserName(Page page)
        {
            string userName = "";
            if (page.Request.Cookies["AdminUser"] != null)
            {
                userName= page.Request.Cookies["AdminUser"]["username"].ToString();
            }

            return userName;
        }

        public int GetAdminGrade()
        {
            int userGrade = 0;
            if (System.Web.HttpContext.Current.Request.Cookies["AdminUser"] != null)
            {
                userGrade = Convert.ToInt32(System.Web.HttpContext.Current.Request.Cookies["AdminUser"]["usergrade"]);
            }

            return userGrade;
        }

        //加密
        public string EncryptPassword(string rs)
        {
            byte[] desKey = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };
            byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                byte[] inputByteArray = Encoding.Default.GetBytes(rs);

                des.Key = desKey;
                des.IV = desIV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(),
                 CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();

                StringBuilder ret = new StringBuilder();
                foreach (byte b in ms.ToArray())
                {
                    ret.AppendFormat("{0:X2}", b);
                }
                ret.ToString();
                return ret.ToString();
            }
            catch
            {
                return rs;
            }
            finally
            {
                des = null;
            }
        }

        //解密
        public string DecryptPassword(string rs)
        {
            byte[] desKey = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };
            byte[] desIV = new byte[] { 0x16, 0x09, 0x14, 0x15, 0x07, 0x01, 0x05, 0x08 };

            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            try
            {
                byte[] inputByteArray = new byte[rs.Length / 2];
                for (int x = 0; x < rs.Length / 2; x++)
                {
                    int i = (Convert.ToInt32(rs.Substring(x * 2, 2), 16));
                    inputByteArray[x] = (byte)i;
                }

                des.Key = desKey;
                des.IV = desIV;
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                StringBuilder ret = new StringBuilder();
                return System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            catch
            {
                return rs;
            }
            finally
            {
                des = null;
            }
        }

        //发送邮件
        public void SendEmail(string ToEmail, string Subject, string body)
        {
            string SmtpServer = System.Configuration.ConfigurationManager.AppSettings["SmtpServer"].ToString();
            string FromEmail = System.Configuration.ConfigurationManager.AppSettings["FromEmail"].ToString();
            string EmailUser = System.Configuration.ConfigurationManager.AppSettings["EmailUser"].ToString();
            string EmailPassword = DecryptPassword(System.Configuration.ConfigurationManager.AppSettings["EmailPassword"].ToString());

            SmtpClient smtp = new SmtpClient(SmtpServer, 25);
            smtp.Credentials = new System.Net.NetworkCredential(EmailUser, EmailPassword); //提供发信smtp认证信息
            MailAddress from = new MailAddress(FromEmail, "中视影光", System.Text.Encoding.UTF8); //发信人信息
            MailAddress to = new MailAddress(ToEmail); //收信人信息
            MailMessage message = new MailMessage(from, to);
            message.Subject = Subject;
            message.Body = body;
            message.IsBodyHtml = true;
            message.SubjectEncoding = System.Text.Encoding.UTF8;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            smtp.Send(message);
            message.Dispose();
        }

        //根据页码显示数据
        public DataSet GetRecordByPage(string tblName, string strGetFields, string ID, int Pagesize, int Curpage, string Where, string OrderField)
        {
            string sql = "select top " + Pagesize + " " + strGetFields + " from " + tblName + " where " + Where + "  and  " + ID + " not in (select top " + Pagesize * (Curpage - 1) + " " + @ID + " from " + tblName + "  where " + Where + " order by " + OrderField + " ) order by " + OrderField + " ";
            DataSet ds = GetDataSet(sql);
            return ds;
        }

        //执行存储过程返回一个记录集 
        public DataSet GetDataSetByPage(string tblName, string strGetFields, string fldOrder, int PageSize, int PageIndex, int doCount, int OrderType, string strWhere)
        {
            SqlConnection conn = OpenConn();
            SqlDataAdapter da = null;
            DataSet ds = null;
            try
            {
                SqlCommand cmd = new SqlCommand("pagelink", conn);
                da = new SqlDataAdapter(cmd);
                ds = new DataSet();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "pagelink";

                SqlParameter[] par ={ new SqlParameter("@tblName",SqlDbType.VarChar,200),
                            new SqlParameter("@strGetFields",SqlDbType.VarChar,1000),
                            new SqlParameter("@fldOrder",SqlDbType.VarChar,250),
                            new SqlParameter("@PageSize",SqlDbType.Int),
                            new SqlParameter("@PageIndex",SqlDbType.Int),
                            new SqlParameter("@doCount",SqlDbType.Bit),
                            new SqlParameter("@OrderType",SqlDbType.Bit),
                            new SqlParameter("@strWhere",SqlDbType.VarChar,500)
                            };
                par[0].Value = tblName;
                par[1].Value = strGetFields;
                par[2].Value = fldOrder;
                par[3].Value = PageSize;
                par[4].Value = PageIndex;
                par[5].Value = doCount;
                par[6].Value = OrderType;
                par[7].Value = strWhere;

                foreach (SqlParameter parameter in par)
                {
                    cmd.Parameters.Add(parameter);
                }

                da.Fill(ds);
            }
            catch (System.Data.SqlClient.SqlException ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

                da.Dispose();
                CloseConn(conn);
            }

            return ds;
        }

        //分页数据源（不带参数）
        public string GetPageLink(int PageSize, int RecordCount)
        {
            int curpage = 1;
            if (HttpContext.Current.Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(HttpContext.Current.Request.QueryString["curpage"]);
            }

            int PageCount;
            if ((RecordCount % PageSize) != 0)
            {
                PageCount = (RecordCount / PageSize) + 1;
            }
            else
            {
                PageCount = RecordCount / PageSize;
            }

            StringBuilder str = new StringBuilder();

            if (curpage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='?curpage=" + (curpage - 1) + "'>上页</a> ");
            }


            if (PageCount <= 10 && curpage <= 10)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?curpage=" + i + "'>" + i + "</a> ");
                    }
                }
            }
            else if (PageCount > 10 && PageCount - curpage <= 6)
            {
                str.Append("<a class='link' href='?curpage=1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 9; i <= PageCount; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?curpage=" + i + "'>" + i + "</a> ");
                    }
                }
            }
            else if (PageCount > 10 && curpage > 6)
            {
                str.Append("<a class='link' href='?curpage=1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = curpage - 4; i <= curpage + 4; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?curpage=" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                str.Append("<a class='link' href='?curpage=" + PageCount + "'>" + PageCount + "</a> ");
            }
            else if (PageCount > 10 && curpage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?curpage=" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (curpage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='?curpage=" + (curpage + 1) + "'>下页</a>");
            }

            return str.ToString();
        }

        //分页数据源（带参数）
        public string GetPageLink(int PageSize, int RecordCount, string param, string value)
        {
            int curpage = 1;
            if (HttpContext.Current.Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(HttpContext.Current.Request.QueryString["curpage"]);
            }

            int PageCount;
            if ((RecordCount % PageSize) != 0)
            {
                PageCount = (RecordCount / PageSize) + 1;
            }
            else
            {
                PageCount = RecordCount / PageSize;
            }

            StringBuilder str = new StringBuilder();

            if (curpage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=" + (curpage - 1) + "'>上页</a> ");
            }


            if (PageCount <= 10 && curpage <= 10)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && PageCount - curpage <= 6)
            {
                str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 9; i <= PageCount; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && curpage > 6)
            {
                str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = curpage - 4; i <= curpage + 4; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                str.Append("<a class='link' href='?curpage=" + PageCount + "'>" + PageCount + "</a> ");
            }

            else if (PageCount > 10 && curpage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (curpage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='?" + param + "=" + value + "&curpage=" + (curpage + 1) + "'>下页</a>");
            }
            return str.ToString();
        }

        //分页数据源（带参数）
        public string GetAjaxPageLink(int pageSize, int recordCount, int curPage)
        {
            int PageCount;
            if ((recordCount % pageSize) != 0)
            {
                PageCount = (recordCount / pageSize) + 1;
            }
            else
            {
                PageCount = recordCount / pageSize;
            }

            StringBuilder str = new StringBuilder();

            if (curPage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + "," + (curPage - 1) + ")'>上页</a> ");
            }


            if (PageCount <= 10 && curPage <= 10)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == curPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + "," + i + ")'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && PageCount - curPage <= 6)
            {
                str.Append("<a class='link' href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + ",1)'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 9; i <= PageCount; i++)
                {
                    if (i == curPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link'  href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + "," + i + ")'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && curPage > 6)
            {
                str.Append("<a class='link'  href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + ",1)'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = curPage - 4; i <= curPage + 4; i++)
                {
                    if (i == curPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + "," + i + ")'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                str.Append("<a class='link' href='javascript:void(0)'  onclick='GetPage(" + pageSize + "," + recordCount + "," + PageCount + ")'>" + PageCount + "</a> ");
            }

            else if (PageCount > 10 && curPage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == curPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + "," + i + ")'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (curPage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='javascript:void(0)' onclick='GetPage(" + pageSize + "," + recordCount + "," + (curPage + 1) + ")'>下页</a>");
            }
            return str.ToString();
        }

        //分页数据源（带参数）
        public string GetPageLink(int PageSize, int RecordCount, string param)
        {
            int curpage = 1;
            if (HttpContext.Current.Request.QueryString["curpage"] != null)
            {
                curpage = int.Parse(HttpContext.Current.Request.QueryString["curpage"]);
            }

            int PageCount;
            if ((RecordCount % PageSize) != 0)
            {
                PageCount = (RecordCount / PageSize) + 1;
            }
            else
            {
                PageCount = RecordCount / PageSize;
            }

            StringBuilder str = new StringBuilder();

            if (curpage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='?" + param + "&curpage=" + (curpage - 1) + "'>上页</a> ");
            }

            if (PageCount <= 10 && curpage <= 10)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && PageCount - curpage <= 6)
            {
                str.Append("<a class='link' href='?" + param + "&curpage=1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 9; i <= PageCount; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && curpage > 6)
            {
                str.Append("<a class='link' href='?" + param + "&curpage=1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = curpage - 4; i <= curpage + 4; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                str.Append("<a class='link' href='?curpage=" + PageCount + "'>" + PageCount + "</a> ");
            }

            else if (PageCount > 10 && curpage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == curpage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='?" + param + "&curpage=" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (curpage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='?" + param + "&curpage=" + (curpage + 1) + "'>下页</a>");
            }
            return str.ToString();
        }

        //分页数据源（带classid）
        public string GetPageLink(int classid, int pageSize, int totalCount, int? pageIndex, string controllerName)
        {
            int PageCount;
            if ((totalCount % pageSize) != 0)
            {
                PageCount = (totalCount / pageSize) + 1;
            }
            else
            {
                PageCount = totalCount / pageSize;
            }

            int currentPage = 1;
            if (pageIndex != null)
            {
                currentPage = (int)pageIndex;
            }

            StringBuilder str = new StringBuilder();

            if (currentPage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + (currentPage - 1) + "'>上页</a> ");
            }


            if (PageCount <= 10 && currentPage <= 10)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && PageCount - currentPage <= 6)
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 9; i <= PageCount; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && currentPage > 6)
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = currentPage - 4; i <= currentPage + 4; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + PageCount + "</a> ");
            }

            else if (PageCount > 10 && currentPage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (currentPage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + (currentPage + 1) + "'>下页</a>");
            }
            return str.ToString();
        }

        //分页数据源（不带classid）
        public string GetPageLink(int pageSize, int totalCount, int? pageIndex, string controllerName)
        {
            int PageCount;
            if ((totalCount % pageSize) != 0)
            {
                PageCount = (totalCount / pageSize) + 1;
            }
            else
            {
                PageCount = totalCount / pageSize;
            }

            int currentPage = 1;
            if (pageIndex != null)
            {
                currentPage = (int)pageIndex;
            }

            StringBuilder str = new StringBuilder();

            if (currentPage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + (currentPage - 1) + "'>上页</a> ");
            }


            if (PageCount <= 10 && currentPage <= 10)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && PageCount - currentPage <= 6)
            {
                str.Append("<a class='link' href='../../" + controllerName + "/1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 9; i <= PageCount; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 10 && currentPage > 6)
            {
                str.Append("<a class='link' href='../../" + controllerName + "/1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = currentPage - 4; i <= currentPage + 4; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                str.Append("<a class='link' href='../../" + controllerName + "/" + PageCount + "</a> ");
            }

            else if (PageCount > 10 && currentPage <= 6)
            {
                for (int i = 1; i <= 10; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (currentPage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + (currentPage + 1) + "'>下页</a>");
            }
            return str.ToString();
        }

        //分页数据源（带classid）
        public string GetMobilePageLink(int classid, int pageSize, int totalCount, int? pageIndex, string controllerName)
        {
            int PageCount;
            if ((totalCount % pageSize) != 0)
            {
                PageCount = (totalCount / pageSize) + 1;
            }
            else
            {
                PageCount = totalCount / pageSize;
            }

            int currentPage = 1;
            if (pageIndex != null)
            {
                currentPage = (int)pageIndex;
            }

            StringBuilder str = new StringBuilder();

            if (currentPage == 1)
            {
                str.Append("<span class='beginend'>上页</span> ");
            }
            else
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + (currentPage - 1) + "'>上页</a> ");
            }


            if (PageCount <= 5 && currentPage <= 5)
            {
                for (int i = 1; i <= PageCount; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 5 && PageCount - currentPage <= 2)
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = PageCount - 4; i <= PageCount; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
            }

            else if (PageCount > 5 && currentPage > 2)
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/1'>1</a> ");
                str.Append("<span class='dotted'>…</span> ");
                for (int i = currentPage - 1; i <= currentPage + 1; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
                //str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + PageCount + "'>" + PageCount + "</a> ");
            }

            else if (PageCount > 5 && currentPage <= 2)
            {
                for (int i = 1; i <= 5; i++)
                {
                    if (i == currentPage)
                    {
                        str.Append("<span class='current'>" + i + "</span> ");
                    }
                    else
                    {
                        str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + i + "'>" + i + "</a> ");
                    }
                }
                str.Append("<span class='dotted'>…</span> ");
            }

            if (currentPage == PageCount)
            {
                str.Append("<span class='beginend'>下页</span>");
            }
            else
            {
                str.Append("<a class='link' href='../../" + controllerName + "/" + classid + "/" + (currentPage + 1) + "'>下页</a>");
            }
            return str.ToString();
        }


        //获取查询总数
        public int GetTotalCount(string tblName, string strWhere)
        {
            string sql = string.Empty;
            if (strWhere != null)
            {
                sql = "select count(*) from " + tblName + " where " + strWhere;
            }
            else
            {
                sql = "select count(*) from " + tblName;
            }

            int count = 0;
            SqlDataReader reader = GetDataReader(sql);
            if (reader.Read())
            {
                count = int.Parse(reader[0].ToString());
                reader.Close();
            }

            return count;
        }

        public bool CheckMobile()
        {
            bool ismobile = false;

            String ua = System.Web.HttpContext.Current.Request.UserAgent.ToString().ToLower();
            if (ua.Contains("android") || ua.Contains("iphone") || ua.Contains("ipad") || ua.Contains("Windows Phone"))
            {
                ismobile = true;
            }

             return false;
        }
    }
}
