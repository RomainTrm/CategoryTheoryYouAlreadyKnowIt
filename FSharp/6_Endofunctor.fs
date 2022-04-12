module Endofunctor

let map morphism = function
    | Some x -> Some (morphism x)
    | None -> None
 
let f (x : 'a option) = map Some x // Maybe is an endofunctor

















// Join is not related to endofunctors
type Client = { Addresses : Address list }
and Address = { Value : string }

let join = List.collect id

let getAddresses (clients : Client list) =
    let listOfListOfAddresses : Address list list = clients |> List.map (fun client -> client.Addresses)
    listOfListOfAddresses |> join