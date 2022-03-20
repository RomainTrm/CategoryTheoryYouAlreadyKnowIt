namespace Monoid

open FsCheck.Xunit
open Xunit

module Hours =
    type Hour = int // from 0 to 23
    let neutralElement = 24
    let addHour left right = left + right % 24

    [<Fact>]
    let Identity () =
        let id x = addHour x neutralElement
        id 5 = 5
        
    [<Fact>]
    let Associativity () =
        addHour 5 (addHour 4 10) = addHour (addHour 5 4) 10
        
module ValidationEnum =
    type Validation = Valid | Invalid
    let neutralElement = Valid
    
    let foldValidations left right =
        match left, right with
        | Valid, Valid -> Valid
        | _ -> Invalid
        
    [<Property>]
    let ``Identity`` validation =
        let id x = foldValidations x neutralElement
        id validation = validation
        
    [<Property>]
    let Associativity x y z =
        foldValidations x (foldValidations y z) = foldValidations (foldValidations x y) z
      
module Lists =
    let neutralElement = []
        
    [<Property>]
    let ``Identity`` (list : int list) =
        let id l = l@neutralElement
        id list = list
        
    [<Property>]
    let Associativity (x : int list) y z =
        x@(y@z) = (x@y)@z