// Functions Currying and Closure


// Partial application

let myFunction a b c =
    a + b + c

myFunction 1 2 3
// 6

// partial application
let myOtherFunction =
    myFunction 1

myOtherFunction 2 3
// 6

let yetAnotherFunction =
    myOtherFunction 2

yetAnotherFunction 3

let myLastExpression = yetAnotherFunction 3

myLastExpression

// A use - a logger function

let logSomething (logginParams: string) (myMessage: string) =
    ()

let myLogger =
    logSomething "someParams"


// Closures

let mutable myChickenName = "Clucky"

let myClosure (n: int) =
    $"{myChickenName} - {n}"

myClosure 10

let notAClosure (name: string) (n: int) =
    $"{name} - {n}"

notAClosure myChickenName 10
