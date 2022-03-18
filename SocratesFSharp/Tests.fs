module Tests

open System
open Xunit


let maybeHead list =
    match list with
    | [] -> None
    | head::tail -> Some head
