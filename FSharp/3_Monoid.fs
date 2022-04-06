namespace Monoid

open FsCheck.Xunit

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