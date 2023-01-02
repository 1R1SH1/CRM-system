using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_Consulting_CRM_API.Methods
{
    public class ServiceCore
    {
        public static DataContext Context { get; set; }
        public ServiceCore(DataContext dataContext)
        {
            Context = dataContext;
        }
        public async Task PostService(Services data)
        {
            await Context.Service.AddAsync(data);
            await Context.SaveChangesAsync();
        }
        public async Task PutService(Services data)
        {
            Context.Service.Update(data);
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Services>> GetService()
        {
            return await Context.Service.ToListAsync();
        }
        public async Task DeleteService(int id)
        {
            Services service = Context.Service.ToList().Find(u => u.Id == id);
            Context.Service.Remove(service);
            await Context.SaveChangesAsync();
        }
    }
}
