using Microsoft.EntityFrameworkCore;
using MySite4u.Data;
using MySite4u.Interfaces;
using MySite4u.Models;
using MySite4u.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Services
{
    public class PortRepository : IPortRepository
    {
        private readonly ApplicationDbContext _context;

        public PortRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddPortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Add(portfolio);
        }

        public List<Portfolio> GetAllPortfolios()
        {
            return _context.Portfolios.ToList();
        }

        public Portfolio GetPort(int id)
        {
            return _context.Portfolios.FirstOrDefault(p => p.Id == id);
        }

        public void RemovePort(int id)
        {
            _context.Portfolios.Remove(GetPort(id));
        }

        public void UpdatePortfolio(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

        public IndexViewModel GetAllPortfolios(
                int pageNumber,
                string category,
                string search,
                string orderBy)
        {
            Func<Portfolio, bool> InCategory = (portfolio) => { return portfolio.Category.ToLower().Equals(category.ToLower()); };

            int pageSize = 6;
            int skipAmount = pageSize * (pageNumber - 1);

            var query = _context.Portfolios.AsNoTracking().AsQueryable();
            if (!String.IsNullOrEmpty(category))
                query = query.Where(x => InCategory(x));

            int portfolioCount = query.Count();

            return new IndexViewModel
            {

                PageNumber = pageNumber,
                PageCount = (int)Math.Ceiling((double)portfolioCount / pageSize),
                NextPage = portfolioCount > skipAmount + pageSize,
                Category = category,
                Portfolios = query
                       .Skip(skipAmount)
                       .Take(pageSize)
                       .ToList()
            };
        }
    }
}
