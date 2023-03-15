namespace Gymbuddy.Models
{
    public class CommentModel
    {
        public int UserId { get; set; }
        public int PostId { get; set; }
        public string Description { get; set; }
    }
}
