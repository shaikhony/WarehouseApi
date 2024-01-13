
namespace WarehouseApi.Services
{
    public class GroupsService : IGroupsService
    {
        private readonly ApplicationDbContext _context;

        public GroupsService(ApplicationDbContext context)
        {
            _context = context;
        }

   
        public async Task<Group> Add(Group group)
        {
          await _context.Groups.AddAsync(group);
          _context.SaveChanges();
           return group;
        }
  
        public Group Delete(Group group)
        {
            _context.Remove(group);
            _context.SaveChanges();
            return group; 
        }

        public  async Task<IEnumerable<Group>> GetAll()
        {
            return await _context.Groups.OrderBy(g => g.GroupName).ToListAsync();
        }

        public async Task<Group> GetByID(int id)
        {
            return await _context.Groups.SingleOrDefaultAsync(g => g.Id == id);
        }

        public Task<bool> IsvalidGroup(int id)
        {
            return _context.Groups.AnyAsync(g => g.Id == id);
        }

        public Group Update(Group group)
        {
            _context.Update(group);
            _context.SaveChanges();
            return group;
        }
    }
}
