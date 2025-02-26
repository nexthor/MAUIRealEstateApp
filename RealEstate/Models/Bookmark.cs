using System.Text.Json.Serialization;

namespace RealEstate.Models
{
    public class Bookmark
    {
        public int Id { get; set; }
        public bool Status { get; set; }
        public int User_Id { get; set; }
        public int PropertyId { get; set; }
    }
}
