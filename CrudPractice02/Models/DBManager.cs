using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace CrudPractice02.Models
{
    public class DBManager
    {
        public static DataTable ExcuteProcedure(string procedure,string[,] param)
        {
            SqlConnection con = new SqlConnection("Data Source=KAIF\\SQLEXPRESS;Initial Catalog=practice;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            for(int i=0;i<param.Length/2;i++)
            {
                cmd.Parameters.AddWithValue(param[i, 0], param[i, 1]);
            }
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
        public static DataTable ExcuteProcedure(string procedure)
        {
            SqlConnection con = new SqlConnection("Data Source=KAIF\\SQLEXPRESS;Initial Catalog=practice;Integrated Security=True");
            SqlCommand cmd = new SqlCommand(procedure, con);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            return dt;
        }
    }
}