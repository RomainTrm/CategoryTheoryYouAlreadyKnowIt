module Endofunctor

let map morphism = function
    | Some x -> Some (morphism x)
    | None -> None
 
let f (x : 'a option) = map Some x // Maybe is an endofunctor