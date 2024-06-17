namespace Volunteer.Model
{
    public class AttachmentModel : ChangeModel
    {
        public required string VolunteerID { get; set; }
        public required string AttachmentID { get; set; }
        public required string Name { get; set; }
        public required string File { get; set; }
    }
}
