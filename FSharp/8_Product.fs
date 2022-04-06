module Product

open FsCheck.Xunit

type C = { A: int; B: string }

let f x = x.A
let g x = x.B

let ``<f; g>`` x = (f x, g x)

[<Property>]
let ``f is fst after <f; g>`` (c: C) =
    f c = (``<f; g>`` >> fst) c

[<Property>]
let ``g is snd after <f; g>`` (c: C) =
    g c = (``<f; g>`` >> snd) c

// if fst . h = f && snd . h = g, then h = <f; g> 

let factorizer (f: C -> int) (g: C -> string) = fun c -> (f c, g c)
