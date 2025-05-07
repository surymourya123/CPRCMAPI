namespace CPRCMAPI.Models
{
    public class Workstatus
    {
        public string groupname        { get; set; }
        public int recordid         { get; set; }
        public string doctor           { get; set; }
        public string senddate         { get; set; }
        public string typeoffile       { get; set; }
        public string pdffilename      { get; set; }
        public int noofpdfs         { get; set; }
        public string printed          { get; set; }
        public string dataentry        { get; set; }
        public string proofread        { get; set; }
        public string validated        { get; set; }
        public string claims           { get; set; }
        public string usvalidated      { get; set; }
        public string notes            { get; set; }
        public string zipstatus        { get; set; }
        public string uploaded         { get; set; }
        public string userid           { get; set; }
        public DateTime entrydt          { get; set; }
        public DateTime lastactvydt      { get; set; }
        public string entryuser        { get; set; }
        public string pdfmissing       { get; set; }
        public int pdfcount         { get; set; }
        public string coding           { get; set; }
    }

    public class StatusUpdateRequest
    {
        public string printed { get; set; }
        public string coding { get; set; }
        public string dataentry { get; set; }
        public string proofread { get; set; }
        public string validated { get; set; }
        public string claims { get; set; }
    }
}
