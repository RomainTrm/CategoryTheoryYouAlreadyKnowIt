using System;

namespace CSharp
{
    public enum TransactionStatus { Valid, Invalid }
    
    public record Amount(Decimal Value) {
        public static bool operator <(Amount a, Amount b) => a.Value < b.Value;

        public static bool operator >(Amount a, Amount b) => a.Value > b.Value;

        public static Amount Limit => new Amount(10000);
    }
    
    public record TransactionLine(DateTime Date, Amount Amount);
}