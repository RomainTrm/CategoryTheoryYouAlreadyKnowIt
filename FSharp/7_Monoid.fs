namespace Monoid

open Xunit

module Hours =
    type Hour = int // from 0 to 23
    let neutralElement = 24
    let addHour left right = left + right % 24
    let id x = addHour x neutralElement

    [<Fact>]
    let Identity () =
        id 5 = 5
        
    [<Fact>]
    let Associativity () =
        addHour 5 (addHour 4 10) = addHour (addHour 5 4) 10
        
