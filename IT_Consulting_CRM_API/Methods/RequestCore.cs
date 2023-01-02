using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_Consulting_CRM_API.Methods
{
    public class RequestCore
    {
        public static DataContext Context { get; set; }
        public RequestCore(DataContext dataContext)
        {
            Context = dataContext;
        }
        public async Task PostRequest(Requests data)
        {
            await Context.Request.AddAsync(data);
            await Context.SaveChangesAsync();
        }
        public async Task PutRequest(Requests data)
        {
            Context.Request.Update(data);
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Requests>> GetRequest()
        {
            return await Context.Request.ToListAsync();
        }
        public async Task DeleteRequest(int id)
        {
            Requests requests = Context.Request.ToList().Find(u => u.Id == id);
            Context.Request.Remove(requests);
            await Context.SaveChangesAsync();
        }
    }
}
