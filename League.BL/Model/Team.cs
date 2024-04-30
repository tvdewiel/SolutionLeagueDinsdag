using League.BL.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.Model
{
    public class Team
    {
        public int Stamnummer {  get;private set; }
        public string Naam {  get; private set; }
        public string Bijnaam { get; private set; }
        private List<Speler> _spelers = new List<Speler>();
        
        internal Team(int stamnummer, string naam)
        {
            ZetStamnummer(stamnummer);
            ZetNaam(naam);
        }

        public void ZetStamnummer(int stamnummer)
        {
            if (stamnummer <= 0) throw new TeamException("ZetTeam");
            Stamnummer = stamnummer;
        }
        public void ZetNaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new TeamException("ZetNaam");
            Naam = naam;
        }
        public void ZetBijnaam(string naam)
        {
            if (string.IsNullOrWhiteSpace(naam)) throw new TeamException("ZetBijnaam");
            Bijnaam = naam;
        }
        public IReadOnlyList<Speler> Spelers()
        {
            return _spelers.AsReadOnly();
        }
        internal void VoegSpelerToe(Speler speler)
        {
            if (speler==null) throw new SpelerException("VoegSpelerToe");
            if (_spelers.Contains(speler)) throw new SpelerException("VoegSpelerToe");
            _spelers.Add(speler);
            if (speler.Team != this) speler.ZetTeam(this);
        }
        internal void VerwijderSpeler(Speler speler)
        {
            if (speler == null) throw new SpelerException("VerwijderSpelerToe");
            if (!_spelers.Contains(speler)) throw new SpelerException("VerwijderSpelerToe");
            _spelers.Remove(speler);
            if (speler.Team == this) speler.VerwijderTeam();
        }
        public bool HeeftSpeler(Speler speler)
        {
            return _spelers.Contains(speler);
        }
    }
}
