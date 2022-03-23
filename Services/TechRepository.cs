using MySite4u.Data;
using MySite4u.Interfaces;
using MySite4u.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Services
{
    public class TechRepository : ITechRepository
    {
        private readonly ApplicationDbContext _context;
        public TechRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddMember(Member member)
        {
            _context.Members.Add(member);
        }

        public List<Member> GetAllMembers()
        {
            return _context.Members.ToList();
        }

        public Member GetMember(int id)
        {
            return _context.Members.Find(id);
        }

        public void RemoveMember(int id)
        {
            _context.Members.Remove(GetMember(id));
        }

        public void UpdateMember(Member member)
        {
            _context.Members.Update(member);
        }

        public async Task<bool> SaveChangesAsync()
        {
            if (await _context.SaveChangesAsync() > 0)
            {
                return true;
            }
            return false;
        }

    }
}
