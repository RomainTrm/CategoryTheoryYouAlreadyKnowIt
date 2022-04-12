using System;
using System.Collections.Generic;
using System.Linq;
// ReSharper disable All

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


        
        
        
        
        
        
        
        
        
        
        
        
        
        
        // Join is not related to endofunctors
        record Client(List<Address> Addresses);

        record Address(string Value);

        static List<T> Join<T>(List<List<T>> list) => list.SelectMany(x => x).ToList();

        static List<Address> GetAddresses(List<Client> clients) => 
            Join<Address>(clients.Select(client => client.Addresses).ToList());
    }
}