using System;
using FsCheck;
using Xunit;

// ReSharper disable All
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


        public int IsPositive(int value) =>
            value > 0 ? value : throw new Exception("invalid integer");

        public string IntToString(int i) => i.ToString();
        public string IsPositiveString(int value) => IntToString(IsPositive(value));
        
        public enum Error { NotPositive }
        
        public Either<int, Error> SafeIsPositive(int value) =>
            value > 0 
                ? Either<int, Error>.Left(value) 
                : Either<int, Error>.Right(Error.NotPositive);

        public Either<TLeftOut, TRight> MapLeft<TLeftIn, TLeftOut, TRight>(
            Either<TLeftIn, TRight> either, Func<TLeftIn, TLeftOut> morphism) =>
            either.Match(
                left => Either<TLeftOut, TRight>.Left(morphism(left)), 
                right => Either<TLeftOut, TRight>.Right(right));
        
        public Either<string, Error> SafeIsPositiveString(int value) => 
            MapLeft(SafeIsPositive(value), IntToString);
    }
}