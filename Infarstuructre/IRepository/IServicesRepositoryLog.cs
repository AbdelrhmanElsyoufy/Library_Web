using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.IRepository
{
    public interface IServicesRepositoryLog<E> where E : class
    {
        List<E> GetAll();

        E FindBy(Guid Id);

        bool Save(Guid Id, Guid UserId);

        bool Update(Guid Id, Guid UserId);

        bool Delete(Guid Id, Guid UserId);

        bool DeleteLog(Guid Id);
    }
}
