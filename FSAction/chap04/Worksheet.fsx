// Chapter 4 - F# Fundamentals
// Understanding the difference between expressions and statements
// Working with immutable data

// 4.1 Expressions

// 4.1.4 Expressions in F#

//  Listing 4.3 Working in an expression-oriented language

let describeAge age =
    let ageDescription =
        if age < 18 then "Child"
        elif age < 65 then "Adult"
        else "DAP"
    let greeting = "Hello"
    $"{greeting}! You are a '{ageDescription}'"


describeAge 15
describeAge 45
describeAge 70


// 4.1.5 Composability

// Refactoring to Functions

// Listing 4.5 Refactoring to functions

open System

let calculateAgeDescription age =
    if age < 18 then "Child"
    elif age < 65 then "Adult"
    else "OAP"

let describeAge2 age =
    let ageDescription = calculateAgeDescription age
    let greeting = "Hello"
    printfn $"{greeting}! You are a '{ageDescription}'."


//  4.1.6 Unit

let printAdditon a b =
    let answer = a + b
    printfn $"{a} plus {b} equals {answer}"

// val printAdditon: a: int -> b: int -> unit

printAdditon 3 4
// 3 plus 4 equals 7


// Unit as an input

let getTheCurrentTime0 = System.DateTime.Now
// val getTheCurrentTime0: System.DateTime = 8/7/2024 2:36:58 PM
let x0 = getTheCurrentTime0
let y0 = getTheCurrentTime0
// Outputs the same

let getTheCurrentTime1 () = System.DateTime.Now
// val getTheCurrentTime1: unit -> System.DateTime
let x1 = getTheCurrentTime1 ()
let y1 = getTheCurrentTime1 ()
// Different output


// Unit and Side Effects

// But doesn't enforec purity
let addDays days =
    let newDays = System.DateTime.Today.AddDays days // Side effrect 1
    printfn  $"You gave me {days} days and I gave you {newDays}"       // Side effect 2
    newDays

// val addDays: days: float -> System.DateTime

let result1 = addDays 5


// 4.1.7 Ignore
// For impure functions

let addSeveralDays () =
    ignore (addDays 3)
    ignore (addDays 5)
    addDays 7

let result2 = addSeveralDays ()


// 4.2 Immutable data

// Imutable state

let name = "isaac"
let mutable age = 42
age <- 43
if age = 43 then
    printfn "%s" name


// Working with immutable Data: Worked Example

// Driving a Mutable Car
// Listing 4.6 Managing state with mutable variables

let mutable gas1 = 100.0

let drive1 distance =
    if distance = "far" then gas1 <- gas1 / 2.0
    elif distance = "medium" then gas1 <- gas1 - 10.0
    else gas1 <- gas1 - 1.0

// val drive1: distance: string -> unit

drive1 "far"
drive1 "medium"
drive1 "near"
gas1


// Driving a Immutable Car
// Listing 4.7 Managing state with immutable values

let drive2 gas distance =
    if distance = "far" then  gas / 2.0
    elif distance = "medium" then gas - 10.0
    else gas - 1.0

// val drive2: gas: float -> distance: string -> float

let gas2 = 100.0
let state1 = drive2 gas2 "far"
let state2 = drive2 state1 "medium"
let state3 = drive2 state2 "near"
