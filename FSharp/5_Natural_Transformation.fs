module Natural_Transformation

open FsCheck.Xunit
open Xunit

let maybeHead = function
    | [] -> None
    | head::_ -> Some head
    
[<Fact>]
let transform_empty_list =
    Assert.Equal(maybeHead [], None)
    
[<Property>]
let transform_non_empty_list (head : int) tail =
    maybeHead (head::tail) = Some head