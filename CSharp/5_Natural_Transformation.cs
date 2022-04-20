using System.Collections.Generic;
using System.Linq;
using FsCheck;
using Xunit;
// ReSharper disable All

namespace CSharp
{
    public class Natural_Transformation
    {
        public static Maybe<T> MaybeHead<T>(List<T> list) =>
            list.Any() ? Maybe<T>.Some(list.First()) : Maybe<T>.None();

        [Fact]
        public void Transform_empty_list()
        {
            Assert.Equal(MaybeHead(new List<int>()), Maybe<int>.None());
        }

        [Fact]
        public void Transform_non_empty_list()
        {
            Prop.ForAll(
                    Arb.From<int>(),
                    Arb.From<List<int>>(),
                    (head, tail) => 
                        MaybeHead(tail.Prepend(head).ToList()).Equals(Maybe<int>.Some(head)))
                .QuickCheckThrowOnFailure();
        }
    }
}