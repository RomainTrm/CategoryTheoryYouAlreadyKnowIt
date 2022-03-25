module Coproduct

open FsCheck.Xunit

type Either<'TLeft, 'TRight> =
    | Left of 'TLeft
    | Right of 'TRight
    
let ind = Left
let inr = Right

type C = A of A | B of B
and A = string
and B = int

let f = C.A
let g = C.B

let ``[f; g]`` either : C =
    match either with
    | Left x -> f x
    | Right x -> g x
    
[<Property>]
let ``f is [f; g] after ind`` (x : A) =
    f x = (ind >> ``[f; g]``)  x

[<Property>]
let ``g is [f; g] after inr`` (x : B) =
    g x = (inr >> ``[f; g]``) x


let convert (coproduct, c) = 
    match coproduct with
    | Left a -> Left (a, c)
    | Right b -> Right (b, c)

[<Property>]
let left (a : int) (c : bool) =
    convert (Left a, c) = Left (a, c)

[<Property>]
let right (b : string) (c : bool) =
    convert (Right b, c) = Right (b, c)
    
// Example: convert a product (Amount, CheckNumber|CardNumber) into (Amount, CheckNumber)|(Amount, CardNumber)