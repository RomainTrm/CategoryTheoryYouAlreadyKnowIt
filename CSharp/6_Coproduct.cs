// ReSharper disable All

using FsCheck;
using Xunit;

namespace CSharp
{
    public class Coproduct
    {
        static Either<TL, TR> ind<TL, TR>(TL l) => Either<TL, TR>.Left(l);
        static Either<TL, TR> inr<TL, TR>(TR r) => Either<TL, TR>.Right(r);

        abstract record C { }
        sealed record A(string Value) : C;
        sealed record B(int Value) : C;

        static C f(string x) => new A(x);
        static C g(int x) => new B(x);

        static C f_or_g(Either<string, int> either) => either.Match(f, g);

        [Fact]
        public void f_is_h_after_ind()
        {
            Prop.ForAll(Arb.From<string>(),
                x => f(x) == f_or_g(ind<string, int>(x)))
                .QuickCheckThrowOnFailure();
        }
        
        [Fact]
        public void g_is_h_after_inr()
        {
            Prop.ForAll(Arb.From<int>(),
                    x => g(x) == f_or_g(inr<string, int>(x)))
                .QuickCheckThrowOnFailure();
        }
    }
}