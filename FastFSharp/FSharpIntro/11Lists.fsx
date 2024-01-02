// Intro to Lists
// Creation

let myList = [ 1; 2; 3 ]
let myList2 =
    [
        1
        2
        3
    ]

// equivalent
myList = myList2
let myElement = myList[1]

// Construction - destruction
let myList3 = 4 :: myList
let four :: theRest = myList3  // what if myList is empty
 
 // Concatenation
let combinedList = myList @ myList2

let coolList =
    [ for i in 1 .. 3 do
        for j  in 1 .. 3 do
            i * j ]

// Processing and Recursion

let myValues = [ 1 .. 10 ]

for v in myValues do
    if v % 2 = 0 then
        printfn $" {v}"

let myPrinter (values: list<int>) =
   
    let rec loop (values: list<int>) =
        match values with
        | []  -> ()
        | head::tail  ->
            if head % 2 = 0 then
                printfn $"{head}"
            loop tail

    loop values

myPrinter myValues

// returning a list
let mutable acc = []

for v in myValues do
    if v % 2 = 0 then
        acc <- v :: acc


let myBuilder (values: list<int>) =
   
    let rec loop (values: list<int>) (acc: list<int>) =
        match values with
        | []  -> acc
        | head::tail  ->
            if head % 2 = 0 then
                loop tail (head :: acc)
            else
                loop tail acc

    loop values []

myBuilder myValues

// Build in functions  - Piping and Processing

let myEvens =
    myValues |> List.filter (fun v -> v % 2 = 0)

let myEvensDouble =
    myEvens |> List.map(fun v -> v * 2)


let mySolution =
    myValues
    |> List.filter (fun v -> v % 2 = 0)
    |> List.map (fun v -> v * 2)
    |> List.filter (fun v -> v > 10)
    |> List.map (fun v -> v + 2)
