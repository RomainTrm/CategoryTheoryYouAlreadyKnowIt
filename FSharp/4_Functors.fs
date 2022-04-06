namespace Functors 

open FsCheck.Xunit
  
module Functors_Option = 

    let morphism (x : int) = x.ToString "x"

    let map morphism = function
        | Some x -> Some (morphism x)
        | None -> None
     
    [<Property>]
    let preserve_structure (value : int) =
        let elevate_then_map = Some >> map morphism
        let transform_then_elevate = morphism >> Some
        
        elevate_then_map value = transform_then_elevate value
  
  
module Functors_Result =
  
    let isPositive i = if i > 0 then i else failwith "invalid integer"
    let intToString (i : int) = i.ToString()
    let isPositiveString = isPositive >> intToString // not pure, should not compose

    type Error = NotPositive

    let safeIsPositive i = if i > 0 then Ok i else Error NotPositive

    let map f result =
        match result with
        | Ok x -> Ok (f x)
        | Error e -> Error e

    let safePositiveString = safeIsPositive >> map intToString
