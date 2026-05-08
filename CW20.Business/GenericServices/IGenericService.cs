
namespace CW20.Business.GenericServices;
public interface IGenericService<T>
{

    public Task SoftDelete(int id);
    public Task<T> GetById(int id);
    public Task<List<T>> GetAll();









}
