using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniSqlProject
{
    public class ProjectPersonModel
    {
        public int id { get; set; }
        public int project_id { get; set; }
        public int person_id { get; set; }
        public int hours { get; set; }
    }
}
