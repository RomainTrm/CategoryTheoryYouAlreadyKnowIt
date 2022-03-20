using System;
using FsCheck;

namespace CSharp
{
    public abstract record Either<TLeft, TRight> 
    {
        public static Either<TLeft, TRight> Left(TLeft value) => 
            new Left<TLeft, TRight>(value);
        public static Either<TLeft, TRight> Right(TRight value) => 
            new Right<TLeft, TRight>(value);

        public abstract TOut Match<TOut>(
            Func<TLeft, TOut> leftMorphism,
            Func<TRight, TOut> rightMorphism);
    }

    public sealed record Left<TLeft, TRight>(TLeft Value) : Either<TLeft, TRight>
    {
        public override TOut Match<TOut>(
            Func<TLeft, TOut> leftMorphism, 
            Func<TRight, TOut> rightMorphism) =>
            leftMorphism(Value);
    }

    public sealed record Right<TLeft, TRight>(TRight Value) : Either<TLeft, TRight>
    {
        public override TOut Match<TOut>(
            Func<TLeft, TOut> leftMorphism, 
            Func<TRight, TOut> rightMorphism) =>
            rightMorphism(Value);
    }

    public static class EitherGenerator
    {
        public static Gen<Either<TLeft, TRight>> Gen<TLeft, TRight>() =>
            from isLeft in Arb.Generate<bool>()
            from left in Arb.Generate<TLeft>()
            from right in Arb.Generate<TRight>()
            select isLeft 
                ? Either<TLeft, TRight>.Left(left) 
                : Either<TLeft, TRight>.Right(right);
    } 
}