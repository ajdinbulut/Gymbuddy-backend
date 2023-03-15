using GymBuddy.Core.Entities;

namespace Gymbuddy.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public string? ImageUrl { get; set; }
        public int? Likes { get; set; }
        public DateTime dateCreated { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Comment> Comments { get; set; }

    }
}
