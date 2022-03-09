using System;
using FsCheck;
using Xunit;

namespace CSharp
{
    public class Functors
    {
        public static Maybe<TOut> Map<TValue, TOut>(Maybe<TValue> maybe, Func<TValue, TOut> morphism) =>
            maybe.Match(
                value => Maybe<TOut>.Some(morphism(value)),
                Maybe<TOut>.None);

        [Fact]
        public void PreserveStructure()
        {
            string Morphism(int value) => value.ToString("X");
            
            Maybe<string> Transform_then_elevate(int value) => Maybe<string>.Some(Morphism(value));
            Maybe<string> Elevate_then_map(int value) => Map(Maybe<int>.Some(value), Morphism);

            Prop.ForAll(
                    Arb.From<int>(),
                    value => Transform_then_elevate(value).Equals(Elevate_then_map(value)))
                .QuickCheckThrowOnFailure();
        }
    }
}