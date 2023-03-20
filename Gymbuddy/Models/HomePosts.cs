using Gymbuddy.Entities;
using GymBuddy.Core.Entities;

namespace Gymbuddy.Models
{
    public class HomePosts
    {
        public List<Post>? Posts { get; set; }
        public List<PostLikes> Likes { get; set; }
    }
}
