using FsCheck;
using Xunit;

namespace CSharp
{
    public class Isomorphisms
    {
        private static Either<Unit, int> Maybe_to_either(Maybe<int> x) => x.Match(
            Either<Unit, int>.Right,
            () => Either<Unit, int>.Left(new Unit()));

        private static Maybe<int> Either_to_maybe(Either<Unit, int> x) => x.Match(
            _ => Maybe<int>.None(),
            Maybe<int>.Some);

        private static T Id<T>(T x) => x;
        
        [Fact]
        public void LeftIsomorphism()
        {
            Maybe<int> Isomorphism(Maybe<int> maybe) =>
                Either_to_maybe(Maybe_to_either(maybe));

            Prop.ForAll(
                    MaybeGenerator.Gen<int>().ToArbitrary(),
                    maybe => Isomorphism(maybe).Equals(Id(maybe)))
                .QuickCheckThrowOnFailure();
        }
        
        [Fact]
        public void RightIsomorphism()
        {
            Either<Unit, int> Isomorphism(Either<Unit, int> either) =>
                Maybe_to_either(Either_to_maybe(either));
                
            Prop.ForAll(
                    EitherGenerator.Gen<Unit, int>().ToArbitrary(),
                    either => Isomorphism(either).Equals(Id(either)))
                .QuickCheckThrowOnFailure();
        }
    }
}