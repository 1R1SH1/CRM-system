using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_Consulting_CRM_API.Methods
{
    public class ProjectCore
    {
        public static DataContext Context { get; set; }
        public ProjectCore(DataContext dataContext)
        {
            Context = dataContext;
        }
        public async Task PostProject(Projects data)
        {
            await Context.Project.AddAsync(data);
            await Context.SaveChangesAsync();
        }
        public async Task PutProject(Projects data)
        {
            Context.Project.Update(data);
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Projects>> GetProject()
        {
            return await Context.Project.ToListAsync();
        }
        public async Task DeleteProject(int id)
        {
            Projects project = Context.Project.ToList().Find(u => u.Id == id);
            Context.Project.Remove(project);
            await Context.SaveChangesAsync();
        }
    }
}
