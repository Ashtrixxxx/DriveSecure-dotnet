namespace Backend.Models
{
    public class TempFormData
    {
        public int FormId { get; set; }
        public string FormData { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public TempFormData()
        { }
    }
}
