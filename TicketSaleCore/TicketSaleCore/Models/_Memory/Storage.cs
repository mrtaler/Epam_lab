using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using TicketSaleCore.Models.IRepository;

namespace TicketSaleCore.Models._Memory
{
    public class StorageMemory : IStorage
    {
        public StorageContext StorageContext { get; private set; }

        public StorageMemory()
        {
            this.StorageContext = new StorageContext();
        }

        public T GetRepository<T>() where T : IRepository.IRepository
        {
            foreach (Type type in this.GetType().GetTypeInfo().Assembly.GetTypes())
            {
                if (typeof(T).GetTypeInfo().IsAssignableFrom(type) && type.GetTypeInfo().IsClass)
                {
                    T repository = (T)Activator.CreateInstance(type);

                    repository.SetStorageContext(this.StorageContext);
                    return repository;
                }
            }

            return default(T);
        }

        public void Save()
        {
            // Do nothing
        }
    }
}
