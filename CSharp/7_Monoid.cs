using FsCheck;
using Xunit;

// ReSharper disable All
namespace CSharp
{
    public class Monoid 
    {
        public enum Validation 
        {
            Valid, Invalid
        }

        public static readonly Validation NeutralElement = Validation.Valid;

        public Validation Fold(Validation left, Validation right) =>
            (left, right) switch
            {
                (Validation.Valid, Validation.Valid) => Validation.Valid,
                _ => Validation.Invalid
            };

        [Fact]
        public void Identity()
        {
            Prop.ForAll(Arb.From<Validation>(), 
                v => Fold(NeutralElement, v) == v)
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Associativity()
        {
            Prop.ForAll(Arb.From<Validation>(), Arb.From<Validation>(), Arb.From<Validation>(),
                    (x, y, z) =>
                    {
                        var left = Fold(Fold(x, y), z);
                        var right = Fold(x, Fold(y, z));
                        return left == right;
                    })
                .QuickCheckThrowOnFailure();
        }
    }
}