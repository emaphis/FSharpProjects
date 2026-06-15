// Imperative Programming
// Arrays

// Creating Arrays

// Array literals

let ar1 = [| 1; 2; 3; 4; 5|]

// Array comprehensions

let ar2 = [| 1 .. 10 |]
// val ar2: int array = [|1; 2; 3; 4; 5; 6; 7; 8; 9; 10|]

let ar3 = [| 1 .. 3 .. 10 |]
// al ar3: int array = [|1; 4; 7; 10|]

let ar4 =
    [| for a in 1 .. 5 do
        yield a, a*a, a*a*a|]
// val ar4: (int * int * int) array =
//   [|(1, 1, 1); (2, 4, 8); (3, 9, 27); (4, 16, 64); (5, 25, 125)|]


// System.Array Methods

// val zeroCreate : int arraySize -> 'T []
// Creates an array with arraySize elements. Each element in the array holds
// the default value for the particular data type (0 for numbers, false for
// booleans, null for reference types).

let ar5: int array = Array.zeroCreate 5
// val ar5: int array = [|0; 0; 0; 0; 0|]
ar5
//val it: int array = [|0; 0; 0; 0; 0|


// val create : int -> 'T value -> 'T []
// Creates an array with arraySize elements. Initializes each element in the
// array with value
let ar6 = Array.create 5 "Juliet"
//al ar6: string array = [|"Juliet"; "Juliet"; "Juliet"; "Juliet"; "Juliet"|]


// val init : int arraySize -> (int index -> 'T) initializer -> 'T []
// Creates an array with arraySize elements. Initializes each element in the
//array with the initializer function.

let ar7 = Array.init 5 (fun idx -> $"idx = {idx}")
// al ar7: string array =
//    [|"idx = 0"; "idx = 1"; "idx = 2"; "idx = 3"; "idx = 4"|]


// Working With Arrays

let names = [| "Juliet"; "Monique"; "Rachelle"; "Tara"; "Sophia" |]

let str1 = names[2]
//val str1: string = "Rachelle"

let str2 = names[0]
//al str2: string = "Juliet"

let len1 = names.Length
//val len1: int = 5

for i = 0 to names.Length - 1 do
    printfn  $"{names[i]}"


// arrays are mutable.

names
// val it: string array = [|"Juliet"; "Monique"; "Rachelle"; "Tara"; "Sophia"|]
names[4] <- "Kristen"

names
// val it: string array = [|"Juliet"; "Monique"; "Rachelle"; "Tara"; "Kristen"|]


// Array Slicing

let names2 = [|"0: Juliet"; "1: Monique"; "2: Rachelle"; "3: Tara"; "4: Sophia"|]

let names3 = names2[1..3]
//  val names3: string array = [|"1: Monique"; "2: Rachelle"; "3: Tara"|]

let names4 = names2[2..]
//val names4: string array = [|"2: Rachelle"; "3: Tara"; "4: Sophia"|]

let names5 = names2[..3]
// val names5: string array =
//   [|"0: Juliet"; "1: Monique"; "2: Rachelle"; "3: Tara"|]


// Multi-dimensional Arrays

// Rectangular Arrays

let ar8 = Array2D.zeroCreate<int> 2 3
//  val ar8: int array2d = [[0; 0; 0]   [0; 0; 0]]

let grid = Array2D.init<string> 3 3 (fun row col -> sprintf "row: %i, col: %i" row col)
(*
val grid: string array2d =
  [["row: 0, col: 0"; "row: 0, col: 1"; "row: 0, col: 2"]
   ["row: 1, col: 0"; "row: 1, col: 1"; "row: 1, col: 2"]
   ["row: 2, col: 0"; "row: 2, col: 1"; "row: 2, col: 2"]]
*)

let str3 = grid[0, 1]
//val str3: string = "row: 0, col: 1"

let str4 = grid[1, 2]
//val str4: string = "row: 1, col: 2"


// a simple program to demonstrate how to use and iterate through multidimensional arrays:

open System

let printGrid grid =
    let maxY = Array2D.length1 grid - 1
    let maxX = Array2D.length2 grid - 1

    for row in 0 .. maxY do
        for col in 0 .. maxX do
            if grid[row, col] = true then Console.Write("* ")
            else Console.Write("_")

let toggleGrid (grid: bool[,]) =
    Console.WriteLine()
    Console.WriteLine("Toggle grid:")

    let row =
        Console.Write("Row: ")
        Console.ReadLine() |> int

    let col =
        Console.Write("Col: ")
        Console.ReadLine() |> int

    grid[row, col] <- (not grid[row, col])

let main1() =
    Console.WriteLine("Create a grid:")
    let rows =
        Console.Write("Row: ")
        Console.ReadLine() |> int

    let cols =
        Console.Write("Cols: ")
        Console.ReadLine() |> int

    let grid = Array2D.zeroCreate<bool> rows cols
    printGrid grid

    let mutable go = true
    while go do
        toggleGrid grid
        printGrid grid
        Console.Write("Keep playing (y/n)? ")
        go <- Console.ReadLine() = "y"

    Console.WriteLine("Thanks for playing")


main1()
