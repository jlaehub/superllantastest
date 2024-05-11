namespace pruebaSuperllantas.List
{
    public interface genericList<T> where T : class
    {
        Task<List<T>> List();
        Task<bool> Create(T model);
        Task<bool> Update(T model);
        Task<bool> Delete(int id);
    }
}
