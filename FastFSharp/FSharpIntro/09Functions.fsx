// Functions - Higher Order and Composition

let myAdder a b =
    a + b

let myValidator (trueHandler: int -> unit)
                (falseHanlder: int -> unit)
                 (a: int) : bool =
    if a > 10 then
        trueHandler a
        true
    else
        falseHanlder a
        false

let myTrueHandler x =
    printfn $"Hit True Handler: {x}"

let myFalseHanlder (x: int) : unit =
    printfn $"Hit FALSE Handler: {x}"

myValidator myTrueHandler myFalseHanlder 19
myValidator myTrueHandler myFalseHanlder 9


// Composition
let myAdd10 (a: int) =
    a + 10

let myMultiply10 a =
    a * 10

// two chained opperations
let x = myAdd10 2
let y = myMultiply10 x

let myCombo = myAdd10 >> myMultiply10
let y2 = myCombo 2

let myOtherCombo = myMultiply10 >> myAdd10
let y3 = myOtherCombo 2
