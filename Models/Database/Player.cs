using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenJKLoader.Models.Database
{
    public class Player
    {
        public int Id { get; set; }

        public string Guid { get; set; } = string.Empty;

        public List<string> KnownNames { get; set; } = new List<string>();

        public int Level { get; set; }

        public int Credits { get; set; }

        public int TotalKills { get; set; }

        public int TotalDeaths { get; set; }

        public int DuelsWon { get; set; }

        public int DuelsLost { get; set; }

        public double DuelRank { get; set; }
    }
}
