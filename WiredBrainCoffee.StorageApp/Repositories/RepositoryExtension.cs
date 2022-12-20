namespace WiredBrainCoffee.StorageApp.Repositories
{
    public static class RepositoryExtension
    {
        public static void AddBatch<T>(this IWriteRepository<T> repository, T[] items) //where T : IEntity
        {
            foreach (var item in items)
            {
                repository.Add(item);
            }
            repository.Save();
        }
    }
}
