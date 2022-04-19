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

        public static Validation NeutralElement => throw new System.NotImplementedException();

        public static Validation Compose(Validation left, Validation right) =>
            throw new System.NotImplementedException();

        [Fact]
        public void Identity()
        {
            Prop.ForAll(Arb.From<Validation>(), 
                v => Compose(NeutralElement, v) == v)
                .QuickCheckThrowOnFailure();
        }

        [Fact]
        public void Associativity()
        {
            Prop.ForAll(Arb.From<Validation>(), Arb.From<Validation>(), Arb.From<Validation>(),
                    (x, y, z) =>
                    {
                        var left = Compose(Compose(x, y), z);
                        var right = Compose(x, Compose(y, z));
                        return left == right;
                    })
                .QuickCheckThrowOnFailure();
        }
    }
}