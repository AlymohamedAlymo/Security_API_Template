using Microsoft.EntityFrameworkCore;
using Security_API_Template.Data.Context;
using Security_API_Template.Data.Entites;
using Security_API_Template.Interfaces;

namespace Security_API_Template.Repository
{
    public class UserRepository(DataContext context) : IUser
    {
        public async Task<AppUsers?> GetUserByIdAsync(int id)
        {
            return await context.Users.FindAsync(id);
        }

        public async Task<AppUsers?> GetUserByUserNameAsync(string userName)
        {
            return await context.Users.SingleOrDefaultAsync(u => u.UserName == userName);
        }

        public async Task<IEnumerable<AppUsers>> GetUsersAsync()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }

        public void UpdateUser(AppUsers users)
        {
            context.Entry(users).State = EntityState.Modified;
        }
    }
}
