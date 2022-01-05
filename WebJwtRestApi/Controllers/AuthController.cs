using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using WebJwtRestApi.Service;
using WebJwtRestApi.Controllers;
using System.Linq;
using Microsoft.Data.SqlClient;
using System.Configuration;
using System.Xml;
using Newtonsoft.Json;
using System.ServiceModel;
using System.Web;


namespace WebJwtRestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private object databaseManager;



        public AuthController(IConfiguration configuration, IUserService userService)
        {
            _configuration = configuration;
            _userService = userService;
        }


        [AllowAnonymous]
        [HttpPost(nameof(Auth))]
        public IActionResult Auth([FromBody] LoginModel data)
        {
            bool isValid = _userService.IsValidUserInformation(data);
            if (isValid)
            {
                var tokenString = GenerateJwtToken(data.UserName);
                return Ok(new { Token = tokenString, Message = "Success" });
            }
            return BadRequest("Please pass the valid Username and Password");
        }
        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult))]
        public IActionResult GetResult()
        {
            return Ok("API Validated");
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(Get))]
        public string Get()
        {
            // Initialization  
            HttpResponseMessage response = null;
            DataTable responseObj = new DataTable();
            string json = string.Empty;

            SqlConnection conn = new SqlConnection("Data Source=LT-MARKOK\\SQLEXPRESS;Initial Catalog=Jwt;User ID=sa;Password=sa;MultipleActiveResultSets=True");
            conn.Open();

            SqlCommand cmd = new SqlCommand("", conn);

            DataTable dt;
            SqlDataAdapter da;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT Id, Username, Passw FROM dbo.LoginModel";
            dt = new DataTable();
            da = new SqlDataAdapter(cmd);
            da.Fill(dt);

            string jso = JsonConvert.SerializeObject(dt);

            // Loading Data from SQL server.
            //var data1 = this.databaseManager.LoginModel.toList();
            //var data = this.databaseManager.Where(n => n.Username == "mirko" && n.passw == "mirkomail@gmail.com").ToList();
            // Process data  

            // Create HTTP Response.  

            // Info.  
            return jso;
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult1))]
        public string GetResult1()
        {

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:51973/Service1.svc");

            var factory = new ChannelFactory<IService1>(basicHttpBinding, address);
            IService1 channel = factory.CreateChannel();
            string rez =  channel.GetData1();
            //Console.WriteLine(channel.GetData1());
            //Console.ReadLine();

            return rez;
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(WcfPrintoutRdlc))]
        public byte[] WcfPrintoutRdlc()
        {

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            var address = new EndpointAddress("http://localhost:57925/Service1.svc");

            var factory = new ChannelFactory<IService1>(basicHttpBinding, address);
            IService1 channel = factory.CreateChannel();

            string nazivRdlc = "";

            string jsonSt1 = System.IO.File.ReadAllText(@"C:\Users\marko.koljensic\Angular\WebJwtRestApi\WebJwtRestApi\WebJwtRestApi\File\jsonSt1.json");

            string jsonSt2 = System.IO.File.ReadAllText(@"C:\Users\marko.koljensic\Angular\WebJwtRestApi\WebJwtRestApi\WebJwtRestApi\File\jsonSt2.json");

            byte[] bytes = channel.KreirajReport1(nazivRdlc, jsonSt1, jsonSt2);


            //string ime = "Neki_naziv" + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");

            //string fajl = System.IO.Path.Combine(@"C:\Users\marko.koljensic\Angular\WebJwtRestApi\WebJwtRestApi\WebJwtRestApi\downloads\") + ime + ".pdf";

            //using (System.IO.FileStream fs = new System.IO.FileStream(fajl, System.IO.FileMode.Create))
            //{
            //    fs.Write(bytes, fs.Length > 0 ? (int)fs.Length - 1 : 0, bytes.Length);
            //}


            return bytes;
        }


        [HttpGet(nameof(GetResult2))]
        public string GetResult2()
        {

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:51973/Service1.svc");

            var factory = new ChannelFactory<IService1>(basicHttpBinding, address);
            IService1 channel = factory.CreateChannel();
            string rez = channel.GetData(2);
            //Console.WriteLine(channel.GetData1());
            //Console.ReadLine();

            return rez;
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(GetResult3))]
        public string GetResult3()
        {

            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            var address = new EndpointAddress("http://localhost:51973/Service1.svc");

            var factory = new ChannelFactory<IService1>(basicHttpBinding, address);
            IService1 channel = factory.CreateChannel();
            int broj = 5;
            string rez = channel.GetData(broj);
            //Console.WriteLine(channel.GetData1());
            //Console.ReadLine();

            return rez;
        }


        [Authorize(AuthenticationSchemes = Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet(nameof(PrintOutRDLC1))]
        public byte[] PrintOutRDLC1()
        {
            //WcfServicePrintOutApplication1
            BasicHttpBinding basicHttpBinding = new BasicHttpBinding();
            basicHttpBinding.MaxReceivedMessageSize = Int32.MaxValue;
            var address = new EndpointAddress("http://localhost:61949/Service1.svc");

            var factory = new ChannelFactory<IService1>(basicHttpBinding, address);
            IService1 channel = factory.CreateChannel();

            string nazivRdlc = "";

            string jsonDataSet = System.IO.File.ReadAllText(@"C:\Users\marko.koljensic\Angular\WebJwtRestApi\WebJwtRestApi\WebJwtRestApi\File\jsonSve.json");

            string jsonParameters = "";

            byte[] bytes = channel.KreirajReport(nazivRdlc, jsonDataSet, jsonParameters);


            //string ime = "Neki_naziv" + "_" + DateTime.Now.ToString("ddMMyyyy_HHmmss");

            //string fajl = System.IO.Path.Combine(@"C:\Users\marko.koljensic\Angular\WebJwtRestApi\WebJwtRestApi\WebJwtRestApi\downloads\") + ime + ".pdf";

            //using (System.IO.FileStream fs = new System.IO.FileStream(fajl, System.IO.FileMode.Create))
            //{
            //    fs.Write(bytes, fs.Length > 0 ? (int)fs.Length - 1 : 0, bytes.Length);
            //}


            return bytes;
        }


        /// <summary>
        /// Generate JWT Token after successful login.
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns></returns>
        private string GenerateJwtToken(string userName)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", userName) }),
                Expires = DateTime.UtcNow.AddHours(1),
                Issuer = _configuration["Jwt:Issuer"],
                Audience = _configuration["Jwt:Audience"],
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }


    public class LoginModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }
        [Required]
        public string Passw { get; set; }


    }

}
