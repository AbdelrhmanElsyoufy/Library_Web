using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.IRepository
{
    public interface IServicesRepository<E> where E : class
    {
        List<E> GetAll();

        E FindBy(Guid Id);

        E FindBy(string Name);

        bool Save(E model);

        bool Delete(Guid Id);
    }
}
