module Functors

open FsCheck.Xunit

let morphism (x : int) = x.ToString "x"

let map morphism = function
    | Some x -> Some (morphism x)
    | None -> None
 
[<Property>]
let preserve_structure (value : int) =
    let elevate_then_map = Some >> map morphism
    let transform_then_elevate = morphism >> Some
    
    elevate_then_map value = transform_then_elevate value