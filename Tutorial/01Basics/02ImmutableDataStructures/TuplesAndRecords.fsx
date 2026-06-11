// Immutable Data Structures
// Tuples and Records


// Defining Tuples

let Tuple_2 = (10, "hello")
let Tuple_3 = 10, "hello", 3

// Takes a 2 tuple and returns a float
let average (a, b) =
    (a + b) / 2.0

let avg1 = average (10.0, 20.0)

// Example 1 - a function which multiplies a 3-tuple by a scalar value to return another 3-tuple.

let scalarMultiply (s: float) (a, b, c) =
    (a * s, b * s, c * s)

let sc1 = scalarMultiply  5.0 (6.0, 10.0, 20.0)

// Example 2 - a function which reverses the input of whatever is passed into the function.

let swap (a, b) = (b, a)

let swp1 = ("web", 2.0)

let swp2 = swap (30, 20)

// Example 3 - a function which divides two numbers and returns the remainder simultaneously.

let divrem x y =
    match y with
    | 0 -> None
    | _ -> Some (x / y, x % y)

let div1 = divrem 100 20
let div2 = divrem 6 4
let div3 = divrem 7 0


// Pattern Matching Tuples

// Example 1
/// prints out a custom greeting based on the specified name and/or language.
let greeting (name, language) =
    match (name, language) with
    | "Steve", _ -> "Howdy, Steve"
    | name, "English" -> "Hello, " + name
    | name, _ when language.StartsWith("Span") -> "Hola, " + name
    | _, "French" -> "Bonjour!"
    | _ -> "DOES NOT COMPUTE"


greeting ("Steve", "English")
greeting ("Pierre", "French")
greeting ("Maria", "Spanish")
greeting ("Rocko", "Esperanto")

// Example 2
/// match against the shape of a tuple using the alternative pattern matching syntax
let getLocation = function
    | 0, 0 -> "origin"
    | 0, y -> "on the y-axis at y=" + y.ToString()
    | x, 0 -> "on the x-axis at x=" + x.ToString()
    | x, y -> "at x=" + x.ToString() + ", y=" + y.ToString()

getLocation (0, 0)
getLocation (0, -1)
getLocation (5, -10)
getLocation (7, 0)


// fst and snd

let fst (a, _) = a
let snd (_, b) = b

fst (1, 10)
snd (1, 10)
fst ("hello", "world")

// Assigning Multiple Variables Simultaneously
let x, y = (1, 2)
x
y

//let x1, y1 = (1, 2, 3)
// tuples of differing lengths.


// Defining Records

(*
type recordName =
    { [ fieldName : dataType ] + }
*)

type website =
    { Title : string;
        Url : string }

let homepage = { Title = "Google"; Url = "https://www.google.com" }

homepage.Title
homepage.Url


// Cloning Records

type coords = { X : float; Y : float }

let setX item newX =
    { item with X = newX }

let start = { X = 1.0; Y = 2.0 }

let finish = setX start 15.5


// Here's a more complete program:

type TransactionItem =
    { Name : string;
      ID : int;
      ProcessedText : string;
      IsProcessed : bool }

let getItem name id =
    { Name = name; ID = id; ProcessedText = null; IsProcessed = false }

let processItem item =
    { item with
        ProcessedText = "Done"
        IsProcessed = true }

let printItem msg item =
    printfn $"%s{msg}: %A{item}"

let main1() =
    let preProcessedItem = getItem "Steve" 5
    let postProcessedItem = processItem preProcessedItem

    printItem "preProcessed" preProcessedItem
    printItem "postProcessed" postProcessedItem


// Pattern Matching Records

open System

type coords' = { X : float; Y : float }

let getQuadrant = function
    | { X = 0.0; Y = 0.0 } -> "Origin"
    | item when item.X >= 0.0 && item.Y >= 0.0 -> "I"
    | item when item.X <= 0.0 && item.Y >= 0.0 -> "II"
    | item when item.X <= 0.0 && item.Y <= 0.0 -> "III"
    | item when item.X >= 0.0 && item.Y <= 0.0 -> "IV"

let testCoords (x, y) =
    let item = { X = x; Y = y }
    printfn $"(%f{x}, %f{y}) is in quadrant %s{getQuadrant item}"

let main2() =
    testCoords(0.0, 0.0)
    testCoords(1.0, 1.0)
    testCoords(-1.0, 1.0)
    testCoords(-1.0, -1.0)
    testCoords(1.0, -1.0)
    Console.Read |> ignore
