using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using CrudPractice02.Models;

namespace CrudPractice02.Controllers
{
    public class RagistationController : Controller
    {
        // GET: Ragistation
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UserLog()
        {
            return View();
        }
        public ActionResult InsertLoginPage(string UserName, string PassWord)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Focus"] = "0";
            dic["Status"] = "";
            try
            {
                if (string.IsNullOrEmpty(UserName))
                {
                    dic["Message"] = "Please Enter UserName";
                    dic["Focus"] = "username";
                }
                else if (string.IsNullOrEmpty(PassWord))
                {
                    dic["Message"] = "Please Enter Password";
                    dic["Focus"] = "password";
                }
                else
                {
                    string[,] param = new string[,]
                    {
                        {"@username",UserName},
                        {"@password",PassWord}
                    };
                    DataTable dt = DBManager.ExcuteProcedure("sp_LoginUser", param);
                    if (dt.Rows.Count > 0)
                    {
                        dic["Message"] = dt.Rows[0]["Msg"].ToString();
                        dic["Status"] = dt.Rows[0]["Status"].ToString();
                        dic["Focus"] = dt.Rows[0]["Focus"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                dic["Message"] = e.Message;
            }
            return Json(dic);
        }
        public ActionResult Form()
        {
            return View();
        }
        public ActionResult InsertUpdateFormData(string id,string fname,string lname, string number,string email)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Focus"] = "";
            dic["Status"] = "";
            try
            {
                if (fname.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your first Name !";
                    dic["Focus"] = "#fname";
                }else if (lname.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your Last Name !";
                    dic["Focus"] = "#lname";
                }else if (number.Trim() == "")
                {
                    dic["Message"] = "Please Enter Your Number !";
                    dic["Focus"] = "#number";
                }else if(email.Trim()=="")
                {
                    dic["Message"] = "Please Enter Your Mail !";
                    dic["Focus"] = "#email";
                }else
                {
                    string[,] param = new string[,]
                    {
                      {"@id",id.ToString()},
                      {"@fname",fname },
                      {"@lname",lname },
                      {"@email",email },
                      {"@number",number }
                    };
                    DataTable dt = DBManager.ExcuteProcedure("sp_insertupdateregistation", param);
                    if (dt.Rows.Count > 0)
                    {
                        dic["Message"] = dt.Rows[0]["Msg"].ToString();
                        dic["Status"] = dt.Rows[0]["Status"].ToString();
                        dic["Focus"] = dt.Rows[0]["Focus"].ToString();
                    }
                }
            }
            catch (Exception e)
            {
                dic["Message"] = e.Message;
            }
            return Json(dic);
        }
        public ActionResult ShowFormData(string id)
        {
            StringBuilder sb = new StringBuilder();
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Grid"] = "";
            try
            {
                string[,] param = new string[,]
                {
                   {"@id",id},
                   {"@Type","S"}
                };
                DataTable dt = DBManager.ExcuteProcedure("sp_ShowData", param);
                if (dt.Rows.Count > 0)
                {
                    sb.Append("<table style='padding:20px;' border='1' id='tbl' class='table-bordered table table-striped table-responsive'><tr>");
                    sb.Append("<th class='table-dark'>Action</th>");
                    sb.Append("<th class='table-dark'>Full Name</th>");
                    sb.Append("<th class='table-dark'>Email</th>");
                    sb.Append("<th class='table-dark'>Number</th></tr>");
                    for(int i = 0; i < dt.Rows.Count; i++)
                    {
                        sb.Append("<tr><td><button type='button'  onclick='EditData(" + dt.Rows[i]["id"] + ")' class='btn btn-success'>Edit</button>&nbsp;<button type='button' onclick='DeleteData(" + dt.Rows[i]["id"] + ")' class='btn btn-danger'>Delete</button></td>");
                        sb.Append("<td>"+dt.Rows[i]["Fullname"].ToString() +"</td>");
                        sb.Append("<td>"+dt.Rows[i]["email"].ToString()+"</td>");
                        sb.Append("<td>"+dt.Rows[i]["number"].ToString()+"</td></tr>");
                    }
                    sb.Append("</table>");
                    dic["Grid"] = sb.ToString();
                };
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }
        public ActionResult DeleteFormData(string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            try
            {
                string[,] param = new string[,]
                {
                    {"@id",id.ToString() }
                };
                DataTable dt = DBManager.ExcuteProcedure("sp_DeleteRagistation", param);
                if (dt.Rows.Count > 0)
                {
                    dic["Message"] = dt.Rows[0]["Msg"].ToString();
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }

            return Json(dic);
        }
        public ActionResult EditFormData(string id)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic["Message"] = "";
            dic["Status"] = "0";
            try
            {
                string[,] param = new string[,]
                {
                    { "@id",id.ToString()},
                    {"@Type","E" }
                };
                DataTable dt = DBManager.ExcuteProcedure("sp_ShowData", param);
                if (dt.Rows.Count > 0)
                {
                    dic["id"] = dt.Rows[0]["id"].ToString();
                    dic["fname"] = dt.Rows[0]["fname"].ToString();
                    dic["lname"] = dt.Rows[0]["lname"].ToString();
                    dic["email"] = dt.Rows[0]["email"].ToString();
                    dic["number"] = dt.Rows[0]["number"].ToString();
                    dic["Status"] = "1";
                }
            }
            catch (Exception ex)
            {
                dic["Message"] = ex.Message;
            }
            return Json(dic);
        }
    
    }
}