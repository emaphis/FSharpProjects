// 7 Working with collections

// 7.1 Higher-order functions

open System

//  simple calculation function that logs
//  its output to the console:
let executeCalculation' a b =
    let answer = a + b
    printfn $"Adding {a} abd {b} = {answer}"
    answer

let ans1 = executeCalculation' 3 4
// Adding 3 abd 4 = 7
// val ans1: int = 7

// generalized printing
let executeCalculation logger a b =
    let answer = a + b
    logger $"Adding {a} abd {b} = {answer}"
    answer
// val executeCalculation: logger: (string -> unit) -> a: int -> b: int -> int

let writeToFile (text: string) =
    System.IO.File.AppendAllText("log.txt", text)

executeCalculation writeToFile 10 20

