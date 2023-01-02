using IT_Consulting_CRM_API.Data;
using IT_Consulting_CRM_API.Models;
using Microsoft.EntityFrameworkCore;

namespace IT_Consulting_CRM_API.Methods
{
    public class ContactCore
    {
        public static DataContext Context { get; set; }
        public ContactCore(DataContext dataContext)
        {
            Context = dataContext;
        }
        public async Task PostContact(Contacts data)
        {
            await Context.Contact.AddAsync(data);
            await Context.SaveChangesAsync();
        }
        public async Task PutContact(Contacts data)
        {
            Context.Contact.Update(data);
            await Context.SaveChangesAsync();
        }
        public async Task<IEnumerable<Contacts>> GetContact()
        {
            return await Context.Contact.ToListAsync();
        }
        public async Task DeleteContact(int id)
        {
            Contacts contacts = Context.Contact.ToList().Find(u => u.Id == id);
            Context.Contact.Remove(contacts);
            await Context.SaveChangesAsync();
        }
    }
}
