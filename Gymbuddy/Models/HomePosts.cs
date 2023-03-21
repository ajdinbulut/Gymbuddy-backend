using Gymbuddy.Entities;
using GymBuddy.Core.Entities;

namespace Gymbuddy.Models
{
    public class HomePosts
    {
        public IEnumerable<isPostLiked> Posts { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<PostLikes> PostLikes { get; set; }
    }
}
