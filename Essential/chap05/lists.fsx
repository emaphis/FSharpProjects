// 5 - Introduction to Collections

// The Basics

// Seq - A lazily evaluated collection.
// Array - Great for numerics/data science. There are built-in modules for 2d, 3d, and 4d arrays.
// List - Eagerly evaluated, linked list, with immutable structure and data.

// F# Seq is equivalent to .NET IEnumerable<'T>.'
// The .NET List<'T> is called ResizeArray<'T> in F#.


// Core Functionality

// empty
let items1 = []

// list of 5 items
let items2 = [2;5;3;1;4]

let items3 = [1..5]

// List Comprehension
let items4 = [
    for x in 1..5 do
        yield x
]

let items5 = [ for x in 1..5 do x ]

// (::)
// head :: tail
let extendedItems = 6::items5

// pattern match on list
let readList items =
    match items with
    | []         -> "Empty List"
    | [head]     -> $"Head: {head}"
    | head::tail -> sprintf "Head: %A and Tail %A" head tail


let emptyList = readList []
let multipleList = readList [1;2;3;4;5]
let singleItemList = readList [1]


let readList2 items =
    match items with
    | []         -> "Empty List"
    | head::tail -> sprintf "Head: %A and Tail %A" head tail

let emptyList2 = readList2 []
let multipleList2 = readList2 [1;2;3;4;5]
let singleItemList2 = readList2 [1]


// join concatinate
let list1 = [1..5]
let list2 = [3..7]
let emptyList1 = []

let jointed = list1 @ list2
let joinedEmpty = list1 @ emptyList1
let emptyJoined = emptyList1 @ list1

let joined1 = List.concat [list1; list2]

// filter a list using a predicate function with the signature a -> bool and the List.filter
let myList = [1..9]

let getEvens items =
    items
    |> List.filter (fun x -> x % 2 = 0)

let evens = getEvens myList

// now sum
let sum items =
    items |> List.sum

let mySum = sum myList

// perform an operation on each item in a list
let triple items =
    items
    |> List.map (fun x -> x * 3)

let myTripples = triple [1..5]

// no new list
let print items =
    items
    |> List.iter (fun x -> (printfn "My value is %i" x))

print myList


// sum total price
let items6 = [(1,0.25M);(5,0.25M);(1,2.25M);(1,125M);(7,10.9M)]

let sum1 items =
    items
    |> List.map (fun (q, p) -> decimal q * p)
    |> List.sum

let listSum1 = sum1 items6

let sum2 items =
    items
    |> List.sumBy (fun (q, p) -> decimal q * p)

let listSum2 = sum2 items6

let checkSum = listSum1 = listSum2


// Folding

[1..10]
|> List.fold (fun acc v -> acc + v) 0

[1..10]
|> List.fold (+) 0

[1..10]
|> List.fold (fun acc v -> acc * v) 1

[1..10]
|> List.fold (*) 1

// back to total price example

let getTotal items =
    items
    |> List.fold (fun acc (q, p) -> acc + (decimal q * p)) 0M

let total1 = getTotal items6


// Grouping Data and Uniqueness

let myList2 = [1;2;3;4;5;7;6;5;4;3]

let gbResult = myList2 |> List.groupBy (fun x -> x)

let unique items =
    items
    |> List.groupBy id
    |> List.map (fun (i, _) -> i)

let unResult = unique myList2

let distinct = myList2 |> List.distinct

// using Set
let uniqueSet items =
    items
    |> Set.ofList

let setResult = uniqueSet myList2


// Solving a Problem in Many Ways

// finding the sum of the squares of the odd numbers in a given input list:
let nums = [1..10]

// step by step
nums
|> List.filter (fun v -> v % 2 = 1)
|> List.map (fun v -> v * v)
|> List.sum

// using option and choose
nums
|> List.choose (fun v -> if v % 2 = 1 then Some (v * v) else None)
|> List.sum

// Do not use reduce for this - reduce is a partial function
// so we need to handle empty lists
match nums with
| []    -> 0
| items -> items |> List.reduce (fun acc v -> acc + if v % 2 = 1 then (v * v) else 0)

// fold
nums
|> List.fold (fun acc v -> acc + if v % 2 = 1 then (v * v) else 0) 0

// The recomended version
nums
|> List.sumBy (fun v -> if v % 2 = 1 then (v * v) else 0)


// Working Through a Practical Example
// See MyProgram
