using MySite4u.Models;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Interfaces
{
    public interface IPortRepository
    {
        Portfolio GetPort(int id);
        List<Portfolio> GetAllPortfolios();
        IndexViewModel GetAllPortfolios(int pageNumber, string category, string search, string orderBy);
        void AddPortfolio(Portfolio portfolio);
        void UpdatePortfolio(Portfolio portfolio);
        void RemovePort(int id);
        Task<bool> SaveChangesAsync();

    }
}
