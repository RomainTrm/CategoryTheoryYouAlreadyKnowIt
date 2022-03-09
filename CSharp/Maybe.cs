using System;
using FsCheck;

namespace CSharp
{
    public abstract record Maybe<TValue> 
    {
        public static Maybe<TValue> Some(TValue value) => new Some<TValue>(value);
        public static Maybe<TValue> None() => new None<TValue>();

        public abstract TOut Match<TOut>(
            Func<TValue, TOut> someMorphism,
            Func<TOut> noneMorphism);
    }

    public sealed record Some<TValue>(TValue Value) : Maybe<TValue>
    {
        public override TOut Match<TOut>(
            Func<TValue, TOut> someMorphism,
            Func<TOut> noneMorphism) =>
            someMorphism(Value);
    } 

    public sealed record None<TValue> : Maybe<TValue>
    {
        public override TOut Match<TOut>(
            Func<TValue, TOut> someMorphism,
            Func<TOut> noneMorphism) =>
            noneMorphism();
    }

    public static class MaybeGenerator
    {
        public static Gen<Maybe<T>> Gen<T>() =>
            from isSome in Arb.Generate<bool>()
            from value in Arb.Generate<T>()
            select isSome ? Maybe<T>.Some(value) : Maybe<T>.None();
    } 
}