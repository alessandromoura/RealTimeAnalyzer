using System;
using System.Collections.Generic;
using System.Text;

namespace RealTimeAnalyzer.Publisher.ErrorLogging
{
    class Transaction
    {
        public string AppName { get; set; }
        public string Region { get; set; }
        public decimal Amount { get; set; }
        public string Account { get; set; }
        public DateTime Created { get; set; }

        public override string ToString()
        {
            return $"{Account},{Region},{Amount},{Created}";
        }
    }
}
