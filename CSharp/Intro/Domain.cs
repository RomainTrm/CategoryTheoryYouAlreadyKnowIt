using System;
using System.Collections.Generic;
using System.Linq;

namespace CSharp.Intro
{
    public class Domain
    {
        public Amount GetTotalAmountOfSuspiciousOperations(IReadOnlyList<AccountLine> lines)
            => GetTotalAmount(GetSuspiciousOperations(lines)); // Composition
        
        public IReadOnlyList<AccountLine> GetSuspiciousOperations(IReadOnlyList<AccountLine> lines) =>
            lines
                .SelectMany(line => line.Amount.Value > 10_000m // Where 
                    ? new List<AccountLine> { line }            // Monad & Bind
                    : new List<AccountLine>())
                .ToList();                                      // Composition

        public Amount GetTotalAmount(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => line.Amount)            // Functor & map
                .Aggregate(Amount.Zero, Amount.Add);    // Monoid
    }

    public record AccountLine(DateTime Date, Amount Amount);

    public record Amount(decimal Value)
    {
        public static Amount Add(Amount left, Amount right) => new(left.Value + right.Value);
        public static readonly Amount Zero = new Amount(0m);  // Monoid's Neutral element
    }
}