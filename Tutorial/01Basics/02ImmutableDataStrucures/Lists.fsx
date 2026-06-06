// Immutable Data Structures
// Lists

// Creating Lists

// Using List Literals

let numbers = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]

numbers


// Using the `::` ("cons") Operator

let numbers1 = 1 :: 2 :: 3 :: []

// '::' does not mutate lists

let x = 1 :: 2 :: 3 :: 4 :: []
let y = 12 :: x
x
y


// Using List.init

List.init 5 (fun index -> (index * 3))
//val it: int list = [0; 3; 6; 9; 12]

List.init 5 (fun index -> (index, index * index, index * index * index))
//[(0, 0, 0); (1, 1, 1); (2, 4, 8); (3, 9, 27); (4, 16, 64)]


// Using List Comprehensions

// Ranges have the constructs [start .. end] and [start .. step .. end]

[1 .. 10]
//val it: int list = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]

[1 .. 2 .. 10]
//[1 .. 2 .. 10]

['a' .. 's']

// Ranges have the constructs [start .. end] and [start .. step .. end]

[ for a in 1 .. 10 do
    yield (a * a) ]
// val it: int list = [1; 4; 9; 16; 25; 36; 49; 64; 81; 100]

[ for a in 1 .. 3 do
    for b in 3 .. 7 do
        yield (a, b) ]
// val it: (int * int) list =
//   [(1, 3); (1, 4); (1, 5); (1, 6); (1, 7); (2, 3); (2, 4); (2, 5); (2, 6);
//    (2, 7); (3, 3); (3, 4); (3, 5); (3, 6); (3, 7)]

[ for a in 1 .. 100 do
    if a % 3 = 0 && a % 5 = 0 then yield a]
//val it: int list = [15; 30; 45; 60; 75; 90]

// Loop over any collection, not just numbers

let cl = [ 'a' .. 'f' ]
[for a in cl do yield [a; a; a] ]
//val it: char list list =
//  [['a'; 'a'; 'a']; ['b'; 'b'; 'b']; ['c'; 'c'; 'c']; ['d'; 'd'; 'd'];
//   ['e'; 'e'; 'e']; ['f'; 'f'; 'f']]

// yield keyword pushes a single value into a list. Another keyword, yield!, pushes a collection of values into the list.

[for a in 1 .. 5 do
   yield! [ a .. a + 3 ] ]
//val it: int list =
//  [1; 2; 3; 4; 2; 3; 4; 5; 3; 4; 5; 6; 4; 5; 6; 7; 5; 6; 7; 8]

// mix the yield and yield! keywords:

[for a in 1 .. 5 do
    match a with
    | 3 -> yield! ["hello"; "world"]
    | _ -> yield a.ToString() ]
//val it: string list = ["1"; "2"; "hello"; "world"; "4"; "5"]

// Alternative List Comprehension Syntax using '->'

[ for a in 1 .. 5 -> a * a ]
//[ for a in 1 .. 5 -> a * a]

[ for a in 1 .. 5 do
  for b in 1 .. 3 -> a, b]
//val it: (int * int) list =
//  [(1, 1); (1, 2); (1, 3); (2, 1); (2, 2); (2, 3); (3, 1); (3, 2); (3, 3);
//   (4, 1); (4, 2); (4, 3); (5, 1); (5, 2); (5, 3)]


// Pattern Matching Lists

let rec sum total = function
    | [] -> total
    | hd :: tl -> sum (hd + total) tl

let main1() =
    let numbers = [ 1 .. 5 ]
    let sumOfNumbers = sum 0 numbers
    printfn $"sumOfNumbers: %i{sumOfNumbers}"

main1 ()    // 15

// Reversing Lists

let reverse l =
    let rec loop acc = function
        | [] -> acc
        | hd :: tl -> loop (hd :: acc) tl
    loop [] l

reverse [1 .. 5]
// val it: int list = [5; 4; 3; 2; 1]

// Filtering Lists

let rec filter predicate = function
    | [] -> []
    | hd :: tl ->
        match predicate hd with
        | true -> hd :: filter predicate tl
        | false -> filter predicate tl

let main2() =
    let filteredNumbers = [1 .. 10] |> filter (fun x -> x % 2 = 0)
    printfn $"filteredNumbers: %A{filteredNumbers}"

main2()
// filteredNumbers: [2; 4; 6; 8; 10]

// tail recursive

let filterTR predicate l =
    let rec loop acc = function
        | []  -> acc
        | hd :: tl ->
            match predicate hd with
            | true  -> loop (hd :: acc) tl
            | false -> loop (acc) tl
    List.rev (loop [] l)

let filteredNumbers2 = [1 .. 10] |> filterTR (fun x -> x % 2 = 0)
//filteredNumbers = [1 .. 10] |> filter (fun x -> x % 2 = 0)


// Mapping Lists

let rec map converter = function
    | []  -> []
    | hd :: tl ->
        converter hd :: map converter tl

let main3 () =
    let mappedNumbers = [1 .. 10] |> map ( fun x -> (x * x).ToString() )
    printfn $"mappedNumbers: %A{mappedNumbers}"

main3()
//mappedNumbers: ["1"; "4"; "9"; "16"; "25"; "36"; "49"; "64"; "81"; "100"]

// Tail recursive

let mapTR converter lst =
    let rec loop acc = function
        | []  -> acc
        | hd :: tl ->
            loop (converter hd :: acc) tl
    List.rev (loop [] lst)

let mappedNumbers2 = [1 .. 10] |> mapTR ( fun x -> (x * x).ToString() )
// ["1"; "4"; "9"; "16"; "25"; "36"; "49"; "64"; "81"; "100"]


// Using the List Module

// List.rev - reverses a list

List.rev [1 .. 5]
// val it: int list = [5; 4; 3; 2; 1]

// val it: int list = [5; 4; 3; 2; 1]


// List.filter filters a list:

[1 .. 10] |> List.filter (fun x -> x % 2 = 0)
//[2; 4; 6; 8; 10]


// List.map maps a list from one type to another:

[1 .. 10] |> List.map ( fun x -> (x * x).ToString() )
// ["1"; "4"; "9"; "16"; "25"; "36"; "49"; "64"; "81"; "100"]


// List.append and the @ Operator

let first = [1; 2; 3]
let second = [4; 5; 6]

let combined1 = first @ second
let combined2 = List.append first second


// List.choose

[1 .. 10] |> List.choose (fun x ->
    match x % 2 with
    | 0 -> Some(x, x*x, x*x*x)
    | _ -> None)                                                                                                      | _ -> None)
//[(2, 4, 8); (4, 16, 64); (6, 36, 216); (8, 64, 512); (10, 100, 1000)]


// List.fold and List.foldBack

(* List.fold implementation *)
let rec fold' (f : 'State -> 'T -> 'State) (seed : 'State) = function
    | [] -> seed
    | hd :: tl -> fold' f (f seed hd) tl

(* List.foldBack implementation *)
let rec foldBack' (f : 'T -> 'State -> 'State) (items : 'T list) (seed : 'State) =
    match items with
    | [] -> seed
    | hd :: tl -> f hd (foldBack' f tl seed)


let input = [ 2; 4; 6; 8; 10 ]
let f accumulator input = accumulator * input
let seed = 1
let output = List.fold f seed input
//val output: int = 3840

// Summing the numbers 1 - 100
let x1 = [ 1 .. 100 ] |> List.fold ( + ) 0

// Computing a factorial
let factorial n = [ 1I .. n ] |> List.fold ( * ) 1I
let x2 = factorial 13I


// Computing population standard deviation

let stddev (input : float list) =
    let sampleSize = float input.Length
    let mean = (input |> List.fold ( + ) 0.0) / sampleSize
    let differenceOfSquares =
        input |> List.fold
            ( fun sum item -> sum + Math.Pow(item - mean, 2.0) ) 0.0
    let variance = differenceOfSquares / sampleSize
    Math.Sqrt(variance)

let x3 = stddev [ 5.0; 6.0; 8.0; 9.0 ]

// List.find and List.tryFind
let cities = ["Bellevue"; "Omaha"; "Lincoln"; "Papillion"; "Fremont"]

let findStringContaining (text: string) (items : string list) =
    items |> List.find(fun item -> item.Contains(text))

let findStringContaining2 (text: string) (items : string list) =
    items |> List.tryFind(fun item -> item.Contains(text))

findStringContaining "Papi" cities
findStringContaining2 "Hastings" cities

