﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace League.BL.DTO
{
    public class TeamInfo
    {
        public TeamInfo(int stamnummer, string naam, string bijnaam)
        {
            Stamnummer = stamnummer;
            Naam = naam;
            Bijnaam = bijnaam;
        }

        public int Stamnummer {  get; set; }
        public string Naam { get; set; }
        public string Bijnaam { get; set; }
    }
}
