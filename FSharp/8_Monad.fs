module Monad

open System
open FsCheck.Xunit

type Error = NotInteger | NotPositive

let safeStringToInt (x : string) =
    match Int32.TryParse x with
    | true, i -> Ok i
    | _ -> Error NotInteger
    
let safeIsPositive i = if i > 0 then Ok i else Error NotPositive
let intToString (i : int) = i.ToString "x"
    
let map f result =
    match result with
    | Ok x -> Ok (f x)
    | Error e -> Error e
    
let join (result : Result<Result<'a, 'b>, 'b>) =
    match result with
    | Ok r -> r
    | Error e -> Error e
    
//let bind f = function
//    | Ok r -> f r 
//    | Error e -> Error e
    
let bind f = map f >> join

// Does not compose, intToString expect an int, received a Result<int, Error>
//let safeStringIsPositiveInt = safeStringToInt >> map safeIsPositive >> map intToString

//let safeStringIsPositiveInt = safeStringToInt >> map safeIsPositive >> join >> map intToString
let safeStringIsPositiveInt = safeStringToInt >> bind safeIsPositive >> map intToString




[<Property>]
let associativity (x : string) =
    let left = (safeStringToInt >> bind safeIsPositive) >> map intToString
    let right = safeStringToInt >> (bind safeIsPositive >> map intToString)
    left x = right x
    
let neutral = Ok >> map id
    
[<Property>]
let ``Left identity`` (x : int) =
    let idLeft = neutral >> bind safeIsPositive
    idLeft x = safeIsPositive x
    
[<Property>]
let ``Right identity`` (x : int) =
    let idRight = safeIsPositive >> bind neutral
    idRight x = safeIsPositive x
    
// Associativity, neutral element & identity, Result is a Monoid