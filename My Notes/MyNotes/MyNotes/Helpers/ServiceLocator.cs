using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Helpers
{
    public class ServiceLocator : IServiceLocator
    {
        private static ServiceLocator instance;

        private ServiceLocator()
        {
            services = new Dictionary<Type, object>();
        }

        public static ServiceLocator Instance
        {
            get
            {
                if (instance == null)
                    instance = new ServiceLocator();
                return instance;
            }
        }

        private IDictionary<Type, object> services;

        public void Register<T>(T service)
        {
            services.Add(typeof(T), service);
        }

        public T Resolve<T>()
        {
            return (T)services[typeof(T)];
        }
    }
}
