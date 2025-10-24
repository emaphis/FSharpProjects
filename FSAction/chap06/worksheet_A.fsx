// Chapter 6 - Functions and modules

// Different ways of declaring functions in F#
// Chaining functions together
// Making flexible functions
// Organizing code
// Moving from scripts to applications

//  6.1 Functions

// 6.1.1 The truth behind F# functions

// Listing 6.2 Calling an F# function
let add (firstNumber: int) (secondNumber: int) =
    firstNumber + secondNumber

let addFive = add 5
let fifeteen = addFive 10


// Exercise 6.1


// 6.1.2 Partially applying functions

// 6.1.3 Pipelines

//  Listing 6.3 Manually threading values in a chain of
//              functions

// Defines two functions
let add' a b = a + b
let multiply' a b = a * b

//  Manually threads values through three function calls
let firstValue' = add 5 10
let secondValue' = add firstValue' 7
let finalValue' = multiply' secondValue' 2
// 44

//  chain functions together directly using parentheses:
let finalValueChained = multiply' (add' (add' 5 10) 7) 2
// val = 44

//  Listing 6.4 Chaining function calls using the pipeline
//  operator

let pipeLineSingleLine = 10 |> add' 5 |> add' 7 |> multiply' 2

let pipeLineMultipleLine =
    10
    |> add' 5
    |> add' 7
    |> multiply' 2


//  6.1.4 Using records and tuples with
//  functions

type Pair = { A: int; B: int }

let addP (p: Pair) = p.A + p.B

let sum1 = addP { A = 3; B = 4 }
// val sum1: int = 7

// 6.1.5 Tupled functions

let addT (x, y) = x + y
//val addT: x: int * y: int -> int

let sum2 = addT (3, 4)
// val sum2: int = 7
