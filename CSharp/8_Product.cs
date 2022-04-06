using System;
using FsCheck;
using Xunit;
// ReSharper disable All

namespace CSharp
{
    public class Product
    {
        static TL fst<TL, TR>((TL l, TR _) x) => x.l;
        static TR snd<TL, TR>((TL _, TR r) x) => x.r;
        
        record C(int A, string B);

        static int f(C c) => c.A;
        static string g(C c) => c.B;

        static (int, string) f_and_g(C c) => (f(c), g(c));

        [Fact]
        public void f_is_fst_after_h()
        {
            Prop.ForAll(Arb.From<C>(),
                    c => f(c) == fst(f_and_g(c)))
                .QuickCheckThrowOnFailure();
        }
        
        [Fact]
        public void g_is_snd_after_h()
        {
            Prop.ForAll(Arb.From<C>(), 
                    c => g(c) == snd(f_and_g(c)))
                .QuickCheckThrowOnFailure();
        }

        static Func<C, (int, string)> Factorizer(
            Func<C, int> f,
            Func<C, string> g) =>
            c => (f(c), g(c));
    }
}