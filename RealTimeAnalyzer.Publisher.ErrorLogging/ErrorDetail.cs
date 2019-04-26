using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAnalyzer.Publisher.ErrorLogging
{
    class ErrorDetail
    {
        public string AppName { get; set; }
        public string Username { get; set; }
        public string Description { get; set; }
        public string AppType { get; set; }
        public string Region { get; set; }
        public DateTime Date { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        public override string ToString()
        {
            return $"{AppName},{Username},{AppType},{Region},{Date},{Description}";
        }
    }
}
