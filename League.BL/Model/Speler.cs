using League.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.Model
{
    public class Speler
    {
        internal Speler(string naam, int? lengte, int? gewicht) 
        {
            ZetNaam(naam);
            if (lengte!=null) ZetLengte(lengte.Value);
            if (gewicht.HasValue) ZetGewicht(gewicht.Value);
        }

        internal Speler(int id, string naam, int? lengte, int? gewicht) :this(naam,lengte,gewicht)
        {
            ZetId(id);
        }

        public int Id { get; private set; }
        public string Naam { get; private set; }
        public int? Rugnummer { get; private set; }
        public int? Lengte { get; private set; }
        public int? Gewicht { get; private set; }
        public Team Team { get; private set; }
        public void ZetId(int id)
        {
            if (id <= 0) throw new SpelerException("ZetId");
            Id=id;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrEmpty(naam)) { throw new SpelerException("ZetNaam"); }
            Naam = naam;
        }
        public void ZetLengte(int lengte)
        {
            if (lengte < 150) throw new SpelerException("ZetLengte");
            Lengte = lengte;
        }
        public void ZetGewicht(int gewicht)
        {
            if (gewicht < 50) throw new SpelerException("ZetGewicht");
            Gewicht=gewicht;
        }
        public void ZetRugnummer(int rugnummer)
        {
            if ((rugnummer <= 0) || (rugnummer > 99)) throw new SpelerException("ZetRugnummer");
            Rugnummer = rugnummer;
        }
        public void ZetTeam(Team team)
        {
            //TODO zetteam
        }

        public override bool Equals(object? obj)
        {
            return obj is Speler speler &&
                   Id == speler.Id;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id);
        }
    }
}
