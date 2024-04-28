// Lesson 4  - Arrays.

// Basic comprehension.

let data1 = [| 1..10 |]


// Get value at index

let int1 = data1[0]
let int2 = data1[3]


// Equality and updating

let data2 = [| 1..10 |]

let bool0 = data1 = data2  // true

let bool1 = data2[0] = 5  // false

// Mutate array 
data2[0] <- 5  // arrays are mutable .Net entities

let bool2 = data2[0] = 5  // true

// Create an array
let data3 = Array.create 10 0

// Init array
let rnd = System.Random()

let data4 = Array.init 10 (fun i -> (i, rnd.Next(100)))


// Pattern matching

let countItems input =
    match input with
    | [||] -> "no items"
    | [|item|] -> $"one item: {item}"
    | [|item1; item2|] -> $"two items: {item1}, {item2}"
    | arry -> $"has {arry.Length} items"

let cnt1 = countItems [||]
let cnt2 = countItems [| "item" |]
let cnt3 = countItems [|"item1"; "item2" |]
let cnt4 = countItems [|"item1"; "item2"; "item3" |]
