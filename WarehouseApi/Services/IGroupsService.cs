namespace WarehouseApi.Services
{
    public interface IGroupsService
    {
        Task<IEnumerable<Group>> GetAll();
        Task<Group> GetByID(int id);
        Task<Group> Add(Group group);
        Group Update(Group group);
        Group Delete(Group group);
        Task<bool> IsvalidGroup(int id);
    }
}
