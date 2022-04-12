module Monad

open System
open FsCheck.Xunit

let stringToInt (x : string) =
    match Int32.TryParse x with
    | true, i -> i
    | _ -> failwith "not integer"
let filterPositive i = if i > 0 then i else failwith "invalid integer"
let intToString (i : int) = i.ToString()
let filterPositiveString = stringToInt >> filterPositive >> intToString // compose but not pure




type Error = NotInteger | NotPositive

let safeStringToInt (x : string) =
    match Int32.TryParse x with
    | true, i -> Ok i
    | _ -> Error NotInteger
    
let safeFilterPositive i = if i > 0 then Ok i else Error NotPositive
    
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
//let safeStringIsPositiveInt = safeStringToInt >> map safeFilterPositive >> map intToString

//let safeStringIsPositiveInt = safeStringToInt >> map safeFilterPositive >> join >> map intToString
let safeFilterPositiveString = safeStringToInt >> bind safeFilterPositive >> map intToString




[<Property>]
let associativity (x : string) =
    let left = (safeStringToInt >> bind safeFilterPositive) >> map intToString
    let right = safeStringToInt >> (bind safeFilterPositive >> map intToString)
    left x = right x
    
let neutral = Ok >> map id
    
[<Property>]
let ``Left identity`` (x : int) =
    let idLeft = neutral >> bind safeFilterPositive
    idLeft x = safeFilterPositive x
    
[<Property>]
let ``Right identity`` (x : int) =
    let idRight = safeFilterPositive >> bind neutral
    idRight x = safeFilterPositive x
    
// Associativity, neutral element & identity, Result is a Monoid