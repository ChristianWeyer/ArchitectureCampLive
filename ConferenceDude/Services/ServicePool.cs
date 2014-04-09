using Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConferenceDude.Services
{
    public class ServicePool : IServicePool
    {
        private static ServicePool _current;
        private static object _lockObject = new object();
        private Dictionary<Type, object> _services;

        private ServicePool()
        {
            _services = new Dictionary<Type, object>();
        }

        public static ServicePool Current
        {
            get
            {
                lock (_lockObject)
                {
                    if (_current == null)
                    {
                        _current = new ServicePool();
                    }
                }
                return _current;
            }
        }

        public void AddService<T>(T service)
        {
            if (!_services.ContainsKey(typeof(T)))
            {
                _services.Add(typeof(T), service);
            }
        }

        public T GetService<T>()
        {
            if (_services.ContainsKey(typeof(T)))
            {
                return (T)_services[typeof(T)];
            }
            return default(T);
        }
    }
}
