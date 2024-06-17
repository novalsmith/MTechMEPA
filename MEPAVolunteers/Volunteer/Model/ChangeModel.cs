namespace Volunteer.Model
{
    public class ChangeModel
    {
        public required string CreatedBy { get; set; }
        public required DateTime CreatedDate { get; set; }
        public required string UpdatedBy { get; set; }
        public required DateTime UpdatedDate { get; set; }
    }
}
