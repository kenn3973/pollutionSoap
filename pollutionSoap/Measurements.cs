using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace pollutionSoap
{
    public class Measurements
    {
        public int Id { get; set; }
        public int Measurement { get; set; }
        public DateTime DateCreated { get; set; }

        public Measurements()
        {

        }
    }
}