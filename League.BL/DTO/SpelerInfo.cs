using League.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.DTO
{
    public class SpelerInfo
    {
        public SpelerInfo(int id, string naam, int? rugnummer, int? lengte, int? gewicht, string team)
        {
            Id = id;
            Naam = naam;
            Rugnummer = rugnummer;
            Lengte = lengte;
            Gewicht = gewicht;
            Team = team;
        }

        public int Id { get; set; }
        public string Naam { get; set; }
        public int? Rugnummer { get;set; }
        public int? Lengte { get; set; }
        public int? Gewicht { get; set; }
        public string Team { get;  set; }
    }
}
