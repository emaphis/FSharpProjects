// Lecture 04 - List

// Comprehensions

let data = [1..3..10]

let data1 = [ for i in 1..10 do i]


// Add items(s)

// added to start
let newData = -2 :: data


// pattern match

let getList stuff =
    match stuff with
    | []  -> "Empty list"
    | [x] -> $"Single item {x}"
    | x::xs -> $"Some items: {x} :: {xs}"

let val2 = getList []
let val3 = getList [1]
let val4 = getList [1; 2 ;3]


// Module vs. dot.

let data2 = [1..3..10]

let val5 = data.Head

let val6 = List.head data2


// init

let rnd = System.Random()

let myList1 = List.init 10 (fun i -> rnd.Next(100))


// filter

let myList2 = [0..20] |> List.filter (fun x -> x % 2 = 0)

let myList3 = List.filter (fun x -> x % 2 = 0) myList1


// map

let myList4 = [1..10] |> List.map (fun i -> i * 2)


// reduce/fold

let myNum1 = [1..10] |> List.reduce (fun acc item -> acc + item)

let myNum2 = [1..10] |> List.fold (fun state item -> state + item) 0


// groupBy

let myList6 =
    [1..10] @ [5..10] |> List.groupBy (fun item -> item)


// Iter  - for side effects

[1..10] |> List.iter (fun i -> printf("%i ") i)


// Partial vs. Complete

let item1 : int = [] |> List.head

let item2 : Option<int> =  [] |> List.tryHead

// Sum of squares of the odd numbers

// filter -> map -> sum
let sum1 = 
    [1..10]
    |> List.filter (fun i -> i % 2 = 1)
    |> List.map (fun i -> i * i)
    |> List.sum

// choose-sum
let sum2 =
    [1..10]
    |> List.choose (fun i ->  if i % 2 = 1 then Some (i * i) else None)
    |> List.sum

// reduce
let sum3 =
    [1..10]
    |> List.reduce (fun acc i -> acc + if i % 2 = 1 then i * i else 0)

// fold
let sum4 =
    [1..10]
    |> List.fold (fun state i -> state + if i % 2 = 1 then i * i else 0) 0

// sumBy
let sum5 =
    [1..10]
    |> List.sumBy (fun i -> if i % 2 = 1 then i * i else 0)


// FizzBuz

let fizzBuzz input =
    [(3, "Fizz"); (5, "Buzz")]
    |> List.map (fun (value, msg) -> if input % value = 0 then msg else "")
    |> List.reduce (fun acc item -> acc + item)
    |> fun value -> if value <> "" then value else string input


let fb8 =
    [1..20] |> List.map fizzBuzz


// Conversions

// List.toArray
// List.toSeq

// Array.ofList
// Seq.ofList


// map and bind

Option.map  // (('a -> 'b) -> 'a option -> 'b option)
Option.bind // (('a -> 'b option) -> 'a option -> 'b option)

Result.map  // (('a -> 'b) -> Result<'a,'c> -> Result<'b,'c>)
Result.bind // (('a -> Result<'b,'c>) -> Result<'a,'c> -> Result<'b,'c>)

List.map    // (('a -> 'b) -> 'a list -> 'b list)
List.collect   //(('a -> 'b list) -> 'a list -> 'b list)

