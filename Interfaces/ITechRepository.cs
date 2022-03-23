using MySite4u.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Interfaces
{
    public interface ITechRepository
    {
        Member GetMember(int id);
        List<Member> GetAllMembers();
        void AddMember(Member member);
        void UpdateMember(Member member);
        void RemoveMember(int id);
        Task<bool> SaveChangesAsync();
    }
}
