using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace CSharp.Intro
{
    public static class Domain
    {
        public static Amount GetTotalAmountOfSuspiciousOperations(IReadOnlyList<AccountLine> lines)
        {
            var suspiciousOperations = GetSuspiciousOperations(lines);
            return GetTotalAmount(suspiciousOperations);                        // # 1 Composition
        }

        private static IReadOnlyList<AccountLine> GetSuspiciousOperations(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => (line, isSuspicious: IsSuspiciousAmount(line))) // # 3 Functor & map
                                                                                // Product
                .SelectMany(x => x.isSuspicious                                 // Where 
                    ? new List<AccountLine> { x.line }                          // Monad & Bind
                    : new List<AccountLine>())
                .ToList();                                                      // # 1 Composition

        private static bool IsSuspiciousAmount(AccountLine line) =>             // # 2 Morphisms: Loss of information
            line.Amount.Value > 10_000m;

        private static Amount GetTotalAmount(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => line.Amount)                                    // # 3 Functor & map
                .Aggregate(Amount.Zero, Amount.Add);                            // Monoid
    }

    public record AccountLine(DateTime Date, Amount Amount);

    public record Amount(decimal Value)
    {
        public static Amount Add(Amount left, Amount right) => new(left.Value + right.Value);
        public static readonly Amount Zero = new(0m);                       // Monoid's Neutral element
    }
    
    // Todo : add tests

    public class DomainTests
    {
        [Fact]
        public void CaseOfEmptyList()
        {
            var lines = new List<AccountLine>();
            var total = Domain.GetTotalAmountOfSuspiciousOperations(lines);
            Assert.Equal(total, Amount.Zero);
        }
    }
}