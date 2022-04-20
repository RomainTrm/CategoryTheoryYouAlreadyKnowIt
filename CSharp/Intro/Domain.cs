using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
// ReSharper disable All

namespace CSharp.Intro
{
    public static class Domain
    {
        public static Amount GetTotalAmountOfSuspiciousOperations(IReadOnlyList<AccountLine> lines)
        {
            var suspiciousOperations = GetSuspiciousOperations(lines);
            return GetTotalAmount(suspiciousOperations);                            
        }

        private static IReadOnlyList<AccountLine> GetSuspiciousOperations(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => EvaluateAmountState(line) == AmountState.Suspicious
                    ? new List<AccountLine>{ line }
                    : new List<AccountLine>())
                .SelectMany(x => x)
                // Previous Select & SelectMany equivalent to : .Where(line => EvaluateAmountState(line) == AmountState.Suspicious)
                .ToList();                                                          

        private static AmountState EvaluateAmountState(AccountLine line) =>         
            line.Amount.Value > 10_000m ? AmountState.Suspicious : AmountState.Valid;

        private static Amount GetTotalAmount(IReadOnlyList<AccountLine> lines) =>
            lines
                .Select(line => line.Amount)                                        
                .Aggregate(Amount.Zero, Amount.Add);                                
    }

    public record AccountLine(DateTime Date, Amount Amount);                        
    
    public enum AmountState { Valid, Suspicious }                                   

    public record Amount(decimal Value)
    {
        public static Amount Add(Amount left, Amount right) => new(left.Value + right.Value);
        public static readonly Amount Zero = new(0m);                           
    }
    
    public class DomainTests
    {
        [Fact]
        public void AmountEquals()
        {
            Assert.True(new Amount(3m) == new Amount(3m));
        }
        
        [Fact]
        public void CaseOfEmptyList()
        {
            var lines = new List<AccountLine>();
            var total = Domain.GetTotalAmountOfSuspiciousOperations(lines);
            Assert.Equal(total, Amount.Zero);
        }

        [Fact]
        public void CaseOfNoSuspicious()
        {
            var lines = new List<AccountLine>
            {
                new (DateTime.Today, new Amount(5_000m)),
                new (DateTime.Today, new Amount(1_000.1m)),
                new (DateTime.Today, new Amount(9_000m))
            };
            
            var total = Domain.GetTotalAmountOfSuspiciousOperations(lines);
            Assert.Equal(total, Amount.Zero);
        }

        [Fact]
        public void CaseOfAllSuspicious()
        {
            var lines = new List<AccountLine>
            {
                new (DateTime.Today, new Amount(15_000m)),
                new (DateTime.Today, new Amount(10_000.1m)),
                new (DateTime.Today, new Amount(19_000m))
            };
            
            var total = Domain.GetTotalAmountOfSuspiciousOperations(lines);
            Assert.Equal(total, new Amount(15_000m + 10_000.1m + 19_000m));
        }

        [Fact]
        public void CaseOfSomeSuspicious()
        {
            var lines = new List<AccountLine>
            {
                new (DateTime.Today, new Amount(15_000m)),
                new (DateTime.Today, new Amount(10_000.1m)),
                new (DateTime.Today, new Amount(9_999.999m))
            };
            
            var total = Domain.GetTotalAmountOfSuspiciousOperations(lines);
            Assert.Equal(total, new Amount(15_000m + 10_000.1m));
        }
    }
}