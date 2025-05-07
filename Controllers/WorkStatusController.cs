using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CPRCMAPI.Models;
using Microsoft.Data.SqlClient;
using System.Data;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace CPRCMAPI.Controllers
{

   [Authorize]
    [Route("[controller]")]
    [ApiController]


    public class WorkStatusController : Controller
    {

        private readonly IConfiguration _configuration;

        private readonly ILogger<AuthController> _logger;


        public WorkStatusController(IConfiguration configuration, ILogger<AuthController> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }
      
        [HttpGet]
        [Route("Hello")]
        public IActionResult Hello()
        { 
            return Ok("hello world");  
        }

        //[Authorize]
        [HttpGet]
        [Route("GetWorkStatus")]
        public async Task<IActionResult> GetWorkStatus( string grpname ="all",string filetype ="all")
        {
          
            List<Workstatus> workstatus = new List<Workstatus>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CPSqlConnection"));
            SqlCommand cmd = new SqlCommand("claimpower..usp_getWorkStatus_ForAllClients", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@grpname", grpname);
            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return NotFound();
            }

            while (await reader.ReadAsync())
            {
                var objworkstatus = new Workstatus
                {
                    groupname = reader["groupname"].ToString() ?? "",
                    recordid = (int)reader["recordid"],
                    doctor = reader["doctor"].ToString() ?? "",
                    senddate = reader["senddate"].ToString() ?? "",
                    typeoffile = reader["typeoffile"].ToString() ?? "",
                    pdffilename = reader["pdffilename"].ToString() ?? "",
                    noofpdfs = (int)reader["noofpdfs"],
                    printed = reader["printed"].ToString() ?? "",
                    dataentry = reader["dataentry"].ToString() ?? "",
                    proofread = reader["proofread"].ToString() ?? "",
                    validated = reader["validated"].ToString() ?? "",
                    claims = reader["claims"].ToString() ?? "",
                    usvalidated = reader["usvalidated"].ToString() ?? "",
                    notes = reader["notes"].ToString() ?? "",
                    zipstatus = reader["zipstatus"].ToString() ?? "",
                    uploaded = reader["uploaded"].ToString() ?? "",
                    userid = reader["userid"].ToString() ?? "",
                    entrydt = (DateTime)reader["entrydt"],
                    lastactvydt = (DateTime)reader["lastactvydt"],
                    entryuser = reader["entryuser"].ToString() ?? "",
                    pdfmissing = reader["pdfmissing"].ToString() ?? "",
                    pdfcount = (int)reader["pdfcount"],
                    coding = reader["coding"].ToString() ?? ""
                };
                workstatus.Add(objworkstatus);
            }

            return Ok(workstatus);
        }


        [HttpGet]
        [Route("GetFileDetails")]
        public async Task<IActionResult> GetFileDetails(string client="" , string pdffile = "")
        {
            List<Workstatus> workstatus = new List<Workstatus>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CPSqlConnection"));
            SqlCommand cmd = new SqlCommand(client + "..usp_GetPdfFileDetails", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pdffile", pdffile);

            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

            await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return NotFound();
            }

            while (await reader.ReadAsync())
            {
                var objworkstatus = new Workstatus
                {
                    groupname = reader["groupname"].ToString() ?? "",
                    recordid = (int)reader["recordid"],
                    doctor = reader["doctor"].ToString() ?? "",
                    senddate = reader["senddate"].ToString() ?? "",
                    typeoffile = reader["typeoffile"].ToString() ?? "",
                    pdffilename = reader["pdffilename"].ToString() ?? "",
                    noofpdfs = (int)reader["noofpdfs"],
                    printed = reader["printed"].ToString() ?? "",
                    dataentry = reader["dataentry"].ToString() ?? "",
                    proofread = reader["proofread"].ToString() ?? "",
                    validated = reader["validated"].ToString() ?? "",
                    claims = reader["claims"].ToString() ?? "",
                    usvalidated = reader["usvalidated"].ToString() ?? "",
                    notes = reader["notes"].ToString() ?? "",
                    zipstatus = reader["zipstatus"].ToString() ?? "",
                    uploaded = reader["uploaded"].ToString() ?? "",
                    userid = reader["userid"].ToString() ?? "",
                    entrydt = (DateTime)reader["entrydt"],
                    lastactvydt = (DateTime)reader["lastactvydt"],
                    entryuser = reader["entryuser"].ToString() ?? "",
                    pdfmissing = reader["pdfmissing"].ToString() ?? "",
                    pdfcount = (int)reader["pdfcount"],
                    coding = reader["coding"].ToString() ?? ""
                };
                workstatus.Add(objworkstatus);
            }
            return Ok(workstatus);
           
        }

        [HttpGet]
        [Route("GetFileDetailsUpdated")]
        public async Task<IActionResult> GetFileDetailsUpdated(string client = "", string pdffile = "")
        {
            List<Workstatus> workstatus = new List<Workstatus>();

            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CPSqlConnection"));
            SqlCommand cmd = new SqlCommand(client + "..usp_GetPdfFileDetailsUpdated", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@pdffile", pdffile);
           
            //SqlDataAdapter da = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //da.Fill(dt);

             await con.OpenAsync();
            using var reader = await cmd.ExecuteReaderAsync();

            if (!reader.HasRows)
            {
                return NotFound();
            }

            while (await reader.ReadAsync())
            {
                var objworkstatus = new Workstatus
                {
                    groupname = reader["groupname"].ToString() ?? "",
                    recordid = (int)reader["recordid"],
                    doctor = reader["doctor"].ToString() ?? "",
                    senddate = reader["senddate"].ToString() ?? "",
                    typeoffile = reader["typeoffile"].ToString() ?? "",
                    pdffilename = reader["pdffilename"].ToString() ?? "",
                    noofpdfs = (int)reader["noofpdfs"],
                    printed = reader["printed"].ToString() ?? "",
                    dataentry = reader["dataentry"].ToString() ?? "",
                    proofread = reader["proofread"].ToString() ?? "",
                    validated = reader["validated"].ToString() ?? "",
                    claims = reader["claims"].ToString() ?? "",
                    usvalidated = reader["usvalidated"].ToString() ?? "",
                    notes = reader["notes"].ToString() ?? "",
                    zipstatus = reader["zipstatus"].ToString() ?? "",
                    uploaded = reader["uploaded"].ToString() ?? "",
                    userid = reader["userid"].ToString() ?? "",
                    entrydt = (DateTime)reader["entrydt"],
                    lastactvydt = (DateTime)reader["lastactvydt"],
                    entryuser = reader["entryuser"].ToString() ?? "",
                    pdfmissing = reader["pdfmissing"].ToString() ?? "",
                    pdfcount = (int)reader["pdfcount"],
                    coding = reader["coding"].ToString() ?? ""
                };
                workstatus.Add(objworkstatus);
            }
            return Ok(workstatus);

        }

        [HttpPatch]
        [Route("UpdateStatuses")]
        public  async  Task<IActionResult> UpdateStatuses(
        [FromQuery] string filename,
        [FromQuery] string client,
        [FromQuery] string user,
        [FromBody] StatusUpdateRequest request)
        {
            if (request == null || filename == null)
            {
                return BadRequest("Invalid input");
            }

            //// Check if filename contains "post" (case-insensitive)
            //if (filename.ToLower().Contains("post"))
            //{
            //    request.Remove("claims"); // Remove "claims" from the object
            //}

            // Convert request object to JSON
            var updatedStatusJson = JsonConvert.SerializeObject(new Dictionary<string, string>
            {
                { "printed", request.printed ?? "NS" },
                { "coding", request.coding ?? "NS" },
                { "dataentry", request.dataentry ?? "NS" },
                { "proofread", request.proofread ?? "NS" },
                { "validated", request.validated ?? "NS" },
                { "claims", request.claims ?? "NS" },
            });


            SqlConnection con = new SqlConnection(_configuration.GetConnectionString("CPSqlConnection"));
            SqlCommand cmd = new SqlCommand(client + "..usp_UpdateWorkstatusFile", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Filename", filename);
            cmd.Parameters.AddWithValue("@user", user);
            cmd.Parameters.AddWithValue("@updatedstaus", updatedStatusJson);
            await con.OpenAsync();
           

            var result = await cmd.ExecuteScalarAsync(); // should return 'Ok' or 'Error'

            if (result?.ToString() == "Ok")
            {
                return Ok(new { message = "sucess"});
            }
            else
            {
                return StatusCode(500, new { message = "Failed to update status in the database." });
            }
                        
        }
    }
}
