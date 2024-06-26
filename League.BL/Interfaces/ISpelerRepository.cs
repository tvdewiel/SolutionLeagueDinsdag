﻿using League.BL.DTO;
using League.BL.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.Interfaces
{
    public interface ISpelerRepository
    {
        bool BestaatSpeler(Speler s);
        Speler SchrijfSpelerInDB(Speler s);
        IReadOnlyList<SpelerInfo> SelecteerSpelers(int? id, string naam);
    }
}
