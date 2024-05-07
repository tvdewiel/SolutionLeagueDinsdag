using League.BL.Exceptions;
using League.BL.Interfaces;
using League.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.Managers
{
    public class TransferManager
    {
        private ITransferRepository repo;

        public TransferManager(ITransferRepository repo)
        {
            this.repo = repo;
        }
        public Transfer RegistreerTransfer(Speler speler,Team nieuwTeam,int prijs)
        {
            Transfer transfer = null;
            if (speler == null) throw new ManagerException("registreertransfer - speler is null");
            try
            {
                //speler stopt
                if (nieuwTeam == null)
                {
                    if (speler.Team == null) throw new ManagerException("registreertransfer - team is null");
                    transfer = new Transfer(speler, speler.Team);
                    speler.VerwijderTeam();
                }
                //speler is nieuw
                else if (speler.Team == null)
                {
                    speler.ZetTeam(nieuwTeam);
                    transfer = new Transfer(speler, nieuwTeam, prijs);
                }
                //klassieke transfer
                else
                {
                    transfer = new Transfer(speler, nieuwTeam, speler.Team, prijs);
                    speler.ZetTeam(nieuwTeam);
                }
                return repo.SchrijfTransferInDB(transfer);
            }
            catch (ManagerException) { throw; }
            catch (Exception e) { throw new ManagerException("RegistreerTransfer", e); }
        }
    }
}
