namespace LogicaNegocio.InterfacesRepositorio
{
    public interface IRepositorio<T> where T : class
	{
		void Add(T unObjeto);

		void Remove(int id);

		void Remove(T unObjeto);

		void Update(T unObjeto);

		T GetById(int id);
		IEnumerable<T> GetAll();

	}

}

