using System;
// ReSharper disable ArrangeTypeMemberModifiers

namespace CSharp
{
    public class Endofunctor 
    {
        static Maybe<TOut> Map<TValue, TOut>(Maybe<TValue> maybe, Func<TValue, TOut> morphism) =>
            maybe.Match(
                value => Maybe<TOut>.Some(morphism(value)),
                Maybe<TOut>.None);

        public static Maybe<Maybe<T>> F<T>(Maybe<T> maybe) => Map(maybe, Maybe<T>.Some);
    }
}