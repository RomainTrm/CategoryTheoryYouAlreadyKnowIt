using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Intro
{
    public class Domain
    {
        public Amount GetTotalAmountOfSuspiciousOperations(IReadOnlyList<AccountLine> lines)
        {
            var suspiciousOperations = GetSuspiciousOperations(lines);
            return GetTotalAmount(suspiciousOperations); // Composition
        }

        public IReadOnlyList<AccountLine> GetSuspiciousOperations(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => (line, isSuspicious: IsSuspiciousAmount(line))) // Product
                .SelectMany(x => x.isSuspicious                                 // Where 
                    ? new List<AccountLine> { x.line }                          // Monad & Bind
                    : new List<AccountLine>())
                .ToList();                                                      // Composition

        private static bool IsSuspiciousAmount(AccountLine line) => 
            line.Amount.Value > 10_000m;

        public Amount GetTotalAmount(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => line.Amount)            // Functor & map
                .Aggregate(Amount.Zero, Amount.Add);    // Monoid
    }

    public record AccountLine(DateTime Date, Amount Amount);

    public record Amount(decimal Value)
    {
        public static Amount Add(Amount left, Amount right) => new(left.Value + right.Value);
        public static readonly Amount Zero = new(0m);       // Monoid's Neutral element
    }
}