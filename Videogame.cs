using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace adonet_db_videogame
{
    public record Videogame
    {
        public Videogame(long id, string name, string overview)
        {
            this.Id = id;
            this.Name = name;
            this.Overview = overview;
        }

        public long Id { get; set; }
        public string Name { get; set; }
        public string Overview { get; set; }

    }
}
