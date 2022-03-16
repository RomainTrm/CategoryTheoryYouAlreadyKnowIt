module Product

open FsCheck.Xunit

type C = { A: int; B: string }

let f x = x.A
let g x = x.B

let h x = (f x, g x)

[<Property>]
let ``f is fst after h`` (c: C) =
    f c = (h >> fst) c

[<Property>]
let ``g is snd after h`` (c: C) =
    g c = (h >> snd) c

// if fst . h = f && snd . h = g, then h = <f; g> 

let factorizer (f: C -> int) (g: C -> string) = fun c -> (f c, g c)
