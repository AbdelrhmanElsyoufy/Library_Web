using Domain.Entity;
using Infarstuructre.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infarstuructre.IRepository.ServicesRepository
{
    public class ServicesLogCategory : IServicesRepositoryLog<CategoryLog>
    {
        private readonly BookDBContext _context;

        public ServicesLogCategory(BookDBContext context)
        {
            _context = context;
        }
        public bool Delete(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new CategoryLog
                {
                    Id = Guid.NewGuid(),
                    Action = BooksHelper.Delete,
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.CategoryLogs.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool DeleteLog(Guid Id)
        {
            try
            {
                var result = FindBy(Id);
                if (!result.Equals(null))
                {
                    _context.CategoryLogs.Remove(result);
                    _context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public CategoryLog FindBy(Guid Id)
        {
            try
            {
                return _context.CategoryLogs.Include(x => x.Category).FirstOrDefault(x => x.Id.Equals(Id));
            }
            catch (Exception)
            {
                return null;
            }
        }

        public List<CategoryLog> GetAll()
        {
            try
            {
                return _context.CategoryLogs.Include(x => x.Category).OrderByDescending(x => x.Date).ToList();
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool Save(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new CategoryLog
                {
                    Id = Guid.NewGuid(),
                    Action = BooksHelper.Save,
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.CategoryLogs.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Update(Guid Id, Guid UserId)
        {
            try
            {
                var logCategory = new CategoryLog
                {
                    Id = Guid.NewGuid(),
                    Action = BooksHelper.Update,
                    Date = DateTime.Now,
                    UserId = UserId,
                    CategoryId = Id
                };
                _context.CategoryLogs.Add(logCategory);
                _context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

       
    }
}
