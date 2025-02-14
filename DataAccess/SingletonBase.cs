using Models;
using System;

namespace DataAccess
{
    public class SingletonBase<T> where T : class, new()
    {
        private static T? _instance;
        private static readonly object _lock = new object();
        protected static readonly ApplicationDbContext _context = new ApplicationDbContext();

        public static T Instance
        {
            get
            {
                lock (_lock)
                {
                    return _instance ??= new T();
                }
            }
        }
    }
}
