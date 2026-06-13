// Imperative Programming
// Control Flow

(*
 Imperative programming can be useful in the following situations:

 * Interacting with many objects in the .NET Framework, most of which
   are inherently imperative.
 * Interacting with components that depend heavily on side-effects,
   such as GUIs, I/O, and sockets.
 * Scripting and prototyping snippets of code.
 * Initializing complex data structures.
 * Optimizing blocks of code where an imperative version of an
   algorithm is more efficient than the functional version.
*)

// if/then Decisions

// scopes

let printMessage condition =
    if condition then
        printfn "condition = true: inside the 'if'"
    printfn "outside the 'if' block"

let main1() =
    printMessage true
    printfn "--------"
    printMessage false
    //Console.ReadKey(true) |> ignore
 
main1()


// a demonstration of short-circuiting in F#

let alwaysTrue() =
    printfn "Always true"
    true
    
let alwaysFalse() =
    printfn "Always false"
    false

let main2() =
    let testCases = 
        ["alwaysTrue && alwaysFalse", fun() -> alwaysTrue() && alwaysFalse();
         "alwaysFalse && alwaysTrue", fun() -> alwaysFalse() && alwaysTrue();
         "alwaysTrue || alwaysFalse", fun() -> alwaysTrue() || alwaysFalse();
         "alwaysFalse || alwaysTrue", fun() -> alwaysFalse() || alwaysTrue();]
    
    testCases |> List.iter (fun (msg, res) ->
        printfn "%s: %b" msg (res())
        printfn "-------")


main2()


// for Loops Over Ranges

(*
for var = start-expr to end-expr do
... // loop body
*)


let main3() =
    for i = 1 to 10 do
        printfn $"i: {i}"


main3()


// This code takes input from the user to compute an average:

open System

let main4() =
    Console.WriteLine("This program averages numbers input by the user.")
    Console.Write("How many numbers do you want to add? ")

    let mutable sum = 0
    let numbersToAdd =  Console.ReadLine() |> int

    for i = 1 to numbersToAdd do
        Console.Write("Input #{0}: ", i)
        let input = Console.ReadLine() |> int
        sum <- sum + input

    let average = sum / numbersToAdd
    Console.WriteLine("Average: {0}", average)


main4()


// for Loops Over Collections and Sequences

(*
for pattern in expr do
... // loop body
*)

// print out a shopping list

let shoppingList =
    ["Tofu", 2, 1.99;
    "Seitan", 2, 3.99;
    "Tempeh", 3, 2.69;
    "Rice milk", 1, 2.95;]

for (food, quantity, price) in shoppingList do
    printfn $"foord: {food}, quantity: {quantity}, price: %g{price}"


// while Loops
(*
while expr do
... // loop body
*)

open System

let main5() =
    let password = "monkey"
    let mutable guess = String.Empty
    let mutable attempts = 0

    while password <> guess && attempts < 3 do
        Console.Write("What is the password? ")
        attempts <- attempts + 1
        guess <- Console.ReadLine()

    if password = guess then
        Console.WriteLine("You got the password right!")
    else
        Console.WriteLine("You didn't get the passord")


main5()
