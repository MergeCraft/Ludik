namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T> where T : class
	{
        Task AddAsync(T unObjeto);
        Task RemoveAsync(int id);
        Task RemoveAsync(T unObjeto);
        Task UpdateAsync(T unObjeto);
        Task<T> GetByIdAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();

    }

}

