using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
//using System.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Data;
using CPRCMAPI.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Reflection;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Authorization;

namespace CPRCMAPI.Controllers
{

    [Route("[controller]")]
    [ApiController]

    public class AuthController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        private readonly ILogger<AuthController> _logger;

        // dictionary to store refresh tokens 
       private static Dictionary<string, string> _refreshTokens = new Dictionary<string, string>();
       private DataTableCollection T;

        public AuthController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        //[Authorize]
        [HttpGet]
        [Route("GetUserInfo")]
        public ActionResult<List<User>> GetUserInfo(string userid)
        {
            List<User> Lst = new List<User>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CPSqlConnection"));
            SqlCommand cmd = new SqlCommand("appusers..usp_ValidateUserNew", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@userid", userid);
            cmd.Parameters.AddWithValue("@validateuser", "N");           
            SqlDataAdapter da = new SqlDataAdapter(cmd);       
            DataTable dt = new DataTable();
            da.Fill(dt);

            if (dt.Rows.Count == 0) {
                return NotFound();
             }

            foreach (DataRow row in dt.Rows)
            {
                User objuser = new User();
                objuser.userid = row["userid"].ToString() ?? "";                
                objuser.welcomename = row["welcomename"].ToString() ?? "";
                objuser.usertype = row["usertype"].ToString() ?? "";
                objuser.email = row["email"].ToString() ?? "";
                objuser.doctorname = row["doctorname"].ToString() ?? "";
                objuser.firstname = row["firstname"].ToString() ?? "";
                objuser.lastname = row["lastname"].ToString() ?? "";
                Lst.Add(objuser);
            }         
            return Ok(Lst);
        }

        [HttpPost]
        [Route("Login")]
        public ActionResult Login(Login obj)
        {
          
                SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CPSqlConnection"));
                SqlCommand cmd = new SqlCommand("appusers..usp_ValidateUserNew", con);
                cmd.CommandType = CommandType.StoredProcedure;              
                cmd.Parameters.AddWithValue("@userid", obj.userid);
                 cmd.Parameters.AddWithValue("@validateuser", "Y");
                 cmd.Parameters.AddWithValue("@password", obj.password);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);

                if (ds.Tables[0].Rows.Count > 0)
                {
                    //string output = JsonConvert.SerializeObject(ds.Tables, Formatting.Indented);
                    // generate token for user
                    var token = GenerateAccessToken(obj.userid);

                    // Generate refresh token
                     var refreshToken = Guid.NewGuid().ToString();

                    // Store the refresh token (in-memory for simplicity)
                     _refreshTokens[refreshToken] = obj.userid;

                    // return access token for user's use
                    return Ok(new
                    {
                        accesstoken = new JwtSecurityTokenHandler().WriteToken(token),
                        refreshtoken = refreshToken

                    });
                }

                // unauthorized user
                return Unauthorized("Invalid credentials");

                //return JsonConvert.SerializeObject(ds.Tables , Formatting.Indented); 
        }

        // Endpoint to refresh the access token
        [HttpPost("refresh-token")]
        public ActionResult RefreshToken([FromBody] string refreshToken)
        {
            if (string.IsNullOrEmpty(refreshToken) || !_refreshTokens.ContainsKey(refreshToken))
            {
                return Unauthorized("Invalid refresh token");
            }

            // Retrieve user based on the refresh token
            var username = _refreshTokens[refreshToken];

            // Generate a new access token
            var newAccessToken = GenerateAccessToken(username);

            // Optionally, generate a new refresh token and store it
            var newRefreshToken = Guid.NewGuid().ToString();
            _refreshTokens[newRefreshToken] = username;

            // Optionally, remove the old refresh token from storage
            _refreshTokens.Remove(refreshToken);

            // Return the new access and refresh tokens
            return Ok(new
            {
                accesstoken = new JwtSecurityTokenHandler().WriteToken(newAccessToken),
                refreshtoken = newRefreshToken
            });
        }




        // Generating token based on user information
        private JwtSecurityToken GenerateAccessToken(string userName)
        {
            // Create user claims
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, userName),
            // Add additional claims as needed (e.g., roles, etc.)
        };

            // Create a JWT
            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(60), // Token expiration time
                signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:SecretKey"])),
                    SecurityAlgorithms.HmacSha256)
            );

            return token;
        }

    }
}
