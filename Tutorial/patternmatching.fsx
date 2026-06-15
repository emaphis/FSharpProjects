// Pattern matching

/// <summary>type with possible expressions ... note recursion for all expressions except True</summary>
type Proposition =
    | True
    | Not of Proposition
    | And of Proposition * Proposition
    | Or of Proposition * Proposition

let rec eval x =
    match x with
    | True -> true
    | Not prop -> not (eval prop)
    | And(prop1, prop2) -> eval prop1 && eval prop2
    | Or(prop1, prop2) -> eval prop1 || eval prop2

let shouldBeFalse = And(Not True, Not True)

let shouldBeTrue = Or(True, Not True)

let complexLogic =
    And(And(True, Or(Not True, True)),
        Or(And(True, Not True), Not True))

printfn $"shouldBeFalse: %b{eval shouldBeFalse}"    // prints False
printfn $"shouldBeTrue: %b{eval shouldBeTrue}"      // prints True
printfn $"complexLogic: %b{eval complexLogic}"      // prints False
