// Intro to functions

let myFunction (a: float) (b: float) = a + b

myFunction 3 4
// 7.0

// partialy applied
let myAdd1 = myFunction 1

myAdd1 2.0

let myOtherFunction (a: float, b: float) = myFunction a b

myOtherFunction (3.0, 4.0)


type MyTupleAlias = float * float

let myFunctionAgain (myTuple: MyTupleAlias) =
    let a, b = myTuple
    a + b

let myInput = 3.0, 4.0

myFunctionAgain myInput

myOtherFunction myInput
