// 5 - Introduction to Collections

// The Basics

// Seq - A lazily evaluated collection.
// Array - Great for numerics/data science. There are built-in modules for 2d, 3d, and 4d arrays.
// List - Eagerly evaluated, linked list, with immutable structure and data.

// F# Seq is equivalent to .NET IEnumerable<'T>.'
// The .NET List<'T> is called ResizeArray<'T> in F#.


// Core Functionality

// Create an empty list
let items1 = []

// A list of 5 items
let items2 = [2;5;3;1;4]

// A list of ordered integers
let items3 = [1..5]

// Using a list Comprehension
let items4 = [
    for x in 1..5 do
        yield x
]

// F# 5, we have been able to drop the need for the yield keyword in most cases.
// We can also define it in one line:
let items5 = [ for x in 1..5 do x ]

// (::) - head :: tail
// Add an item to a list, we use the cons operator (::)
let extendedItems = 6::items5


// A non-empty list is made up of a single item called the head and a list of
// items called the tail which could be an empty list []. We can pattern match
// on a list to show this:
let readList items =
    match items with
    | []         -> "Empty List"
    | [head]     -> $"Head: {head}"  // list containing one item.
    | head::tail -> sprintf "Head: %A and Tail %A" head tail


let emptyList = readList []
let multipleList = readList [1;2;3;4;5]
let singleItemList = readList [1]


// If we remove the pattern match for the single item list, the code still works:
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

// use the List.concat function to do the same job as the @ operator:
let joined1 = List.concat [list1; list2]

// filter a list using a predicate function with the signature a -> bool
// and the List.filter
let myList = [1..9]

let getEvens items =
    items
    |> List.filter (fun x -> x % 2 = 0)

let evens = getEvens myList

// Add up the items in a list using the List.sum function
let sum items =
    items |> List.sum

let mySum = sum myList

// perform an operation on each item in a list List.map.
let triple items =
    items
    |> List.map (fun x -> x * 3)

let myTripples = triple [1..5]

// If we don't want to return a new list, we use the List.iter function:
let print items =
    items
    |> List.iter (fun x -> (printfn "My value is %i" x))

print myList


// a more complicated example using List.map that changes the structure of the 
// output list.
// We will use a list of tuples (int * decimal) which might represent quantity and unit price.
let items6 = [(1,0.25M);(5,0.25M);(1,2.25M);(1,125M);(7,10.9M)]

// Sum total price using List.map to convert the list ot a list of integers
// then sum for a total using List.sum
let sum1 items =
    items
    |> List.map (fun (q, p) -> decimal q * p)
    |> List.sum

let listSum1 = sum1 items6

// a more complicated example using List.map that changes the structure of the
//  output list. We will use a list of tuples (int * decimal) which might represent quantity and unit price.
let sum2 items =
    items
    |> List.sumBy (fun (q, p) -> decimal q * p)

let listSum2 = sum2 items6

// should be true
let checkSum = listSum1 = listSum2


// Folding for agregation tasks
// folder -> initial value -> input list -> output value
// ('a -> 'b -> 'a) -> 'a -> 'b list -> 'a

// sum the numbers from one to ten:
[1..10]
|> List.fold (fun acc v -> acc + v) 0

// The same.
[1..10]
|> List.fold ( + ) 0

// The product of [1..10] using List.fold would be:
[1..10]
|> List.fold (fun acc v -> acc * v) 1

// The same
[1..10]
|> List.fold ( * ) 1

// back to total price example using List.fold:
let getTotal items =
    items
    |> List.fold (fun acc (q, p) -> acc + (decimal q * p)) 0M

let total1 = getTotal items6

// An alternative style is to use another of the forward-pipe operators, (||>).
// This version supports a pair tupled of inputs:
let getTotal1 items =
    (0M, items) ||> List.fold (fun acc (q, p) -> acc + decimal q * p)

let total2 = getTotal1 items6


// Grouping Data and Uniqueness

// use the List.groupBy function to return a tuple for each distinct value:
let myList2 = [1;2;3;4;5;7;6;5;4;3]

let gbResult = myList2 |> List.groupBy (fun x -> x)

// Get the list of unique items from the result list, we can use the List.map function:
let unique items =
    items
    |> List.groupBy id
    |> List.map (fun (i, _) -> i)

let unResult = unique myList2

// The function called List.distinct that will do the same
let distinct = myList2 |> List.distinct

// using Set to find distinct
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
