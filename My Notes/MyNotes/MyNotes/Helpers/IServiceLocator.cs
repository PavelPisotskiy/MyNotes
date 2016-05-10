using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyNotes.Helpers
{
    public interface IServiceLocator
    {
        T Resolve<T>();
        void Register<T>(T service);
    }
}
