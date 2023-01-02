using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.Build.Framework;
using Microsoft.EntityFrameworkCore;

namespace IT_Consulting_CRM_API.Methods
{
    public class BlogCore
    {
        public static DataContext Context { get; set; }
        public BlogCore(DataContext dataContext)
        {
            Context = dataContext;
        }
        public async Task PostBlog(Blogs data)
        {
            await Context.Blog.AddAsync(data);
            await Context.SaveChangesAsync();
        }
        public async Task PutBlog(Blogs data)
        {
            Context.Blog.Update(data);
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Blogs>> GetBlog()
        {
            return await Context.Blog.ToListAsync();
        }
        public async Task DeleteBlog(int id)
        {
            Blogs blog = Context.Blog.ToList().Find(u => u.Id == id);
            Context.Blog.Remove(blog);
            await Context.SaveChangesAsync();
        }
    }
}
