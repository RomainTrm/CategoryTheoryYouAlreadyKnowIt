module Composition

let f (x: decimal) = int x
let g (x: int) = x % 2 = 0
let g_after_f = f >> g