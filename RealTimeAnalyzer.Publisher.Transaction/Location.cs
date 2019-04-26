using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAnalyzer.Publisher.ErrorLogging
{
    enum City
    {
        Bluff,
        Invercargill,
        Queenstown,
        Christhchurch,
        Nelson,
        Blenheim,
        Dunedin,
        Wellington,
        Napier,
        NewPlymouth,
        Tauranga,
        Hamilton,
        Auckland,
        HobsonvillePoint,
        ASBCDrive,
        Whangarei,
        TeHapua,
        SaoPaulo,
        Virginia,
        London
    }
    class Location
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Region { get; set; }
    }
}
