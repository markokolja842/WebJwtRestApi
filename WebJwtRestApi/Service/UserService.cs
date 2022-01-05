using Microsoft.Data.SqlClient;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using WebJwtRestApi.Controllers;

namespace WebJwtRestApi.Service
{
    public class UserService : IUserService
    {
        public bool IsValidUserInformation(LoginModel model)
        {

            SqlConnection conn = new SqlConnection("Data Source=LT-MARKOK\\SQLEXPRESS;Initial Catalog=Jwt;User ID=sa;Password=sa;MultipleActiveResultSets=True");
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);

            DataTable dt;
            SqlDataAdapter da;
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT Id, Username, Passw FROM dbo.LoginModel";
            //cmd.CommandText = "SELECT * from dbo.LoginModel where Username = '" +model.UserName.ToString() + "'"  + " and Passw = '" + model.Passw.ToString() + "'" ;
            cmd.CommandText = "SELECT * from dbo.Logovanje where Username = '" + model.UserName.ToString() + "'" + " and Passw = '" + model.Passw.ToString() + "'";
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            if (dt.Rows.Count > 0)
                return true;
            else return false;

            //string jso = JsonConvert.SerializeObject(dt);

            //if (model.UserName.Equals("Jay") && model.Passw.Equals("123456")) return true;
            //else return false;
        }

        public object GetUserDetails()
        {

            return "Jay";
        }

    }
}
