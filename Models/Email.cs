namespace CPRCMAPI.Models
{
    public class Email
    {
        public string subject { get; set; }
        public string toaddress { get; set; }
        public string ccaddress { get; set; } = "";
        public string emailbody { get; set; }
        public string attachfile { get; set; } = "";
    }
}
