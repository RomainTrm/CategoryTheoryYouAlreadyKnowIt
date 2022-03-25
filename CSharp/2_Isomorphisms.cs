using FsCheck;
using Xunit;

namespace CSharp
{
    public class Isomorphisms
    {
        private static (T2, T1) Reverse<T1, T2>((T1, T2) x) => (x.Item2, x.Item1);
        
        private static T Id<T>(T x) => x;
        
        private static (T1, T2) Isomorphism<T1, T2>((T1, T2) tuple) =>
            Reverse(Reverse(tuple));
        
        [Fact]
        public void LeftIsomorphism()
        {
            Prop.ForAll(
                    Arb.From<int>(), Arb.From<string>(),
                    (i, s) => Isomorphism((i, s)).Equals(Id((i, s))))
                .QuickCheckThrowOnFailure();
        }
        
        [Fact]
        public void RightIsomorphism()
        {
            Prop.ForAll(
                    Arb.From<int>(), Arb.From<string>(),
                    (i, s) => Isomorphism((s, i)).Equals(Id((s, i))))
                .QuickCheckThrowOnFailure();
        }
    }
}