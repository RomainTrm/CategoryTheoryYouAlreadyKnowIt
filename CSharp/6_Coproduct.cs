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
        public void f_is_f_or_g_after_ind()
        {
            Prop.ForAll(Arb.From<string>(),
                x => f(x) == f_or_g(ind<string, int>(x)))
                .QuickCheckThrowOnFailure();
        }
        
        [Fact]
        public void g_is_f_or_g_after_inr()
        {
            Prop.ForAll(Arb.From<int>(),
                    x => g(x) == f_or_g(inr<string, int>(x)))
                .QuickCheckThrowOnFailure();
        }


        static Either<(string, bool), (int, bool)> Convert(
            Either<string, int> coproduct, bool c) =>
            coproduct.Match(
                left => Either<(string, bool), (int, bool)>.Left((left, c)),
                right => Either<(string, bool), (int, bool)>.Right((right, c)));

        [Fact]
        public void Left()
        {
            Prop.ForAll(Arb.From<string>(), Arb.From<bool>(),
                (s, b) =>
                {
                    var convert = Convert(Either<string, int>.Left(s), b);
                    var expected = Either<(string, bool), (int, bool)>.Left((s, b));
                    return convert == expected;
                })
                .QuickCheckThrowOnFailure();
        }
        
        [Fact]
        public void Right()
        {
            Prop.ForAll(Arb.From<int>(), Arb.From<bool>(),
                    (i, b) =>
                    {
                        var convert = Convert(Either<string, int>.Right(i), b);
                        var expected = Either<(string, bool), (int, bool)>.Right((i, b));
                        return convert == expected;
                    })
                .QuickCheckThrowOnFailure();
        }
    }
}