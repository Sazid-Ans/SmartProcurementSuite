namespace VendorService.Models.Dtos
{
    public class VendorDto
    {
        public int VendorID { get; set; }                  // int
        public string CompanyName { get; set; }            // nvarchar
        public string ContactEmail { get; set; }           // nvarchar
        public string Phone { get; set; }                  // nvarchar
        public int Rating { get; set; }                    // int
        public bool Active { get; set; }                   // bit
        public DateTime CreatedDate { get; set; }          // datetime2
    }
}
