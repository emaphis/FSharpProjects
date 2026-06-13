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
        yield (a, a*a, a*a*a)|]
// val ar4: (int * int * int) array =
//   [|(1, 1, 1); (2, 4, 8); (3, 9, 27); (4, 16, 64); (5, 25, 125)|]


// System.Array Methods

// val zeroCreate : int arraySize -> 'T []
// Creates an array with arraySize elements. Each element in the array holds the default value for the particular data type (0 for numbers, false for bools, null for reference types).

let (ar5: int array) = Array.zeroCreate 5
// val ar5: int array = [|0; 0; 0; 0; 0|]
ar5
//val it: int array = [|0; 0; 0; 0; 0|


// val create : int -> 'T value -> 'T []
// Creates an array with arraySize elements. Initializes each element in the array with value
let ar6 = Array.create 5 "Juliet"
//al ar6: string array = [|"Juliet"; "Juliet"; "Juliet"; "Juliet"; "Juliet"|]


// val init : int arraySize -> (int index -> 'T) initializer -> 'T []
// Creates an array with arraySize elements. Initializes each element in the array with the initializer function.

let ar7 = Array.init 5 (fun idx -> sprintf $"idx = {idx}")
// al ar7: string array =
//    |"idx = 0"; "idx = 1"; "idx = 2"; "idx = 3"; "idx = 4"|]


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

names[4] <- "Kristen"

names

