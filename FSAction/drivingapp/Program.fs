// Exercise 6.3
// Use the Car module

// 3.
open Car

let exercise_6_3 () =
    let gas = 100.0

    // Using the pipe operator
    let endGas =
        gas
        |> drive 60
        |> drive 30
        |> drive 5

    printfn $"Gas at the end of the trip {endGas}"


// Exercise 6.4

// 2.
open System

let exercise_6_4 () =

    // 1. 
    printfn "How far do you want to dirve?"
    let dist = Console.ReadLine() |> int

    let beginGas = 8.0
    let driveResult = driveRec dist beginGas
    
    if driveResult.OutOfGas then
        printfn "You are out of gas!"
    else
        printfn $"You have {driveResult.GasRemaining} gas remaining."


//do exercise_6_3 ()
do exercise_6_4 ()
