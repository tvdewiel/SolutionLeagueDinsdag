using League.BL.Exceptions;
using League.BL.Interfaces;
using League.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.Managers
{
    public class TeamManager
    {
        private ITeamRepository repo;

        public TeamManager(ITeamRepository repo)
        {
            this.repo = repo;
        }
        public void RegistreerTeam(int stamnummer,string naam, string bijnaam)
        {
            try
            {
                Team t = new Team(stamnummer, naam);
                if (!string.IsNullOrWhiteSpace(bijnaam)) { t.ZetBijnaam(bijnaam); }
                if (!repo.BestaatTeam(t))
                {
                    repo.SchrijfTeamInDB(t);
                }
                else
                {
                    throw new ManagerException("Team bestaat al");
                }
            }
            catch(ManagerException) { throw; }
            catch(Exception ex) { }
        }
        public Team SelecteerTeam(int stamnummer)
        {
            try
            {
                Team team = repo.SelecteerTeam(stamnummer);
                if (team == null) throw new ManagerException("Team bestaat niet");
                return team;
            }
            catch(ManagerException) { throw; }
            catch(Exception ex) { throw new ManagerException("SelecteerTeam", ex); }
        }
    }
}
