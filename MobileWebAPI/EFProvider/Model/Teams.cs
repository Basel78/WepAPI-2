using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MobileWebAPI.Models
{
    public class Team
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string State { get; set; }
        public string Logo { get; set; }
        public string shorts { get; set; }

        public virtual ICollection<Match> Matchs { get; set; }
    }

    public class Match
    {
        public int ID { get; set; }
        public string Date { get; set; }
        public string Field { get; set; }
        public string City { get; set; }
        public string state { get; set; }
        public int TeamAID { get; set; }
        public int TeamBID { get; set; }

        public virtual Team TeamA { get; set; }
        public virtual Team TeamB { get; set; }

        public virtual Tournament Tournament { get; set; }
    }

    public class Tournament
    {

        public string Week { get; set; }
        public virtual Match Match { get; set; }
    }
}