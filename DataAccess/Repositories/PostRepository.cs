using Application.Abstractions;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class PostRepository : IPostRepository
    {
        private readonly SocialDbContext _ctx;

        public PostRepository(SocialDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<Post> CreatePost(Post post)
        {
            post.DateCreated = DateTime.Now;
            post.LastModified = DateTime.Now;
            _ctx.Posts.Add(post);
            await _ctx.SaveChangesAsync();
            return post;
        }

        public async Task DeletePost(int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            if (post == null)
                return;

            _ctx.Posts.Remove(post);
            await _ctx.SaveChangesAsync();
        }

        public async Task<ICollection<Post>> GetAllPosts()
        {
            return await _ctx.Posts.ToListAsync();
        }

        public async Task<Post> GetPostById(int postId)
        {
            return await _ctx.Posts.FirstOrDefaultAsync(x => x.Id == postId);
        }

        public async Task<Post> UpdatePost(string content, int postId)
        {
            var post = await _ctx.Posts.FirstOrDefaultAsync(x => x.Id == postId);
            post.LastModified = DateTime.Now;
            post.Content = content;
            await _ctx.SaveChangesAsync();
            return post;
        }
    }
}
