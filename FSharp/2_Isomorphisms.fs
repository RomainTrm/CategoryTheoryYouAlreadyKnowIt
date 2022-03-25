module Isomorphisms

open FsCheck.Xunit

let reverse (a, b) = (b, a)

[<Property>]
let leftIsomorphism (a : int) (b : string) =
    let isomorphism = reverse >> reverse
    isomorphism (a, b) = id (a, b)
    
[<Property>]
let rightIsomorphism (a : int) (b : string) =
    let isomorphism = reverse >> reverse 
    isomorphism (b, a) = id (b, a)