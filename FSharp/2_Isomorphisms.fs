module Isomorphisms

open FsCheck.Xunit

type Either<'TLeft, 'TRight> =
    | Left of 'TLeft
    | Right of 'TRight

let maybe_to_either = function
    | None -> Left ()
    | Some x -> Right x
    
let either_to_maybe = function
    | Left () -> None
    | Right x -> Some x
    
[<Property>]
let leftIsomorphism (maybe : int option) =
    let isomorphism = maybe_to_either >> either_to_maybe
    isomorphism maybe = id maybe
    
[<Property>]
let rightIsomorphism (either : Either<unit, int>) =
    let isomorphism = either_to_maybe >> maybe_to_either 
    isomorphism either = id either