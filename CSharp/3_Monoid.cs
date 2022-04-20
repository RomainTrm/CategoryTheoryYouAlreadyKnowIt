using FsCheck;
using Xunit;

// ReSharper disable All
namespace CSharp
{
    public class Monoid 
    {
        public enum Validation { Valid, Invalid }

        public static Validation NeutralElement => Validation.Valid;

        public static Validation ComposeValidation(Validation left, Validation right) =>
            (left, right) switch
            {
                (Validation.Valid, Validation.Valid) => Validation.Valid,
                _ => Validation.Invalid
            };

        
        public record Amount(decimal Value)
        {
            public static Amount Add(Amount left, Amount right) => new(left.Value + right.Value);
            public static readonly Amount Zero = new(0m);                           
        }

        public record ValidableAmount(Validation Validation, Amount Amount)
        {
            public static ValidableAmount Neutral => new ValidableAmount(NeutralElement, Amount.Zero);

            public static ValidableAmount Compose(ValidableAmount left, ValidableAmount right) =>
                new ValidableAmount(
                    ComposeValidation(left.Validation, right.Validation), 
                    Amount.Add(left.Amount, right.Amount));
        }
        
        [Fact]
        public void Identity()
        {
            Prop.ForAll(Arb.From<Validation>(), Arb.From<decimal>(), 
                (v, d) =>
                {
                    var validableAmount = new ValidableAmount(v, new Amount(d));
                    return ValidableAmount.Compose(ValidableAmount.Neutral, validableAmount) == validableAmount;
                })
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Associativity()
        {
            Prop.ForAll(Arb.From<Validation>(), Arb.From<Validation>(), Arb.From<Validation>(),
                    (x, y, z) =>
                    {
                        var left = ComposeValidation(ComposeValidation(x, y), z);
                        var right = ComposeValidation(x, ComposeValidation(y, z));
                        return left == right;
                    })
                .QuickCheckThrowOnFailure();
        }
    }
}