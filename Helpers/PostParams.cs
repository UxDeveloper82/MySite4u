using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySite4u.Helpers
{
    public class PostParams : PaginationParams
    {
        public string OrderBy { get; set; } = "Created";
    }
}
