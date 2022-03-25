module Composition

let f (x: decimal) = int x
let g (x: int) = x % 2 = 0
let ``g.f`` = f >> g