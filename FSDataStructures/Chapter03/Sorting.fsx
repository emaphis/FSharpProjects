// F# implementation of sorting algorithms.

// The data
let rand = System.Random()
let data = List.init 10 (fun _  -> rand.Next())
let result = List.sort data

/// Quick sort
let rec quickSort = function
    | [] -> []
    | n::ns -> let lessThan, greaterEqual = List.partition ((>) n) ns
               quickSort lessThan @ n :: quickSort greaterEqual

let result2 = quickSort data
let bool1 = result = result

// Functional Quicksort - Lazy and incremental.
let rec quickSort_func (pxs:seq<_>) =
    seq {
        match Seq.toList pxs with
        | p::xs -> let lessThan, greaterEqual = List.partition ((>=) p) xs
                   yield! quickSort lessThan; yield p; yield! quickSort greaterEqual
        | _ -> ()
    }

let result3 = Seq.toList (quickSort data)
let bool2 = result = result3

// Sorting processes
let sortedProcs =
    query {
        for proc in System.Diagnostics.Process.GetProcesses() do
        let isWindowed = proc.MainWindowHandle <> nativeint 0
        sortBy isWindowed
        thenBy proc.ProcessName
        select proc }


// Bubble sort

/// Swaps two elements of an array.
let swap x y (array : 'arr []) =
    let temp = array[x]
    array[x] <- array[y]
    array[y] <- temp

/// BubbleSort algorithm
let bubbleSort array =
    let rec loop (array : 'arr []) =
        let mutable swaps = 0
        for i = 0 to array.Length - 2 do
            if array[i] > array[i+1] then
                swap i (i+1) array
                swaps <- swaps + 1

        if swaps > 0 then loop array else array
    loop array

let arr = [| 5.0; 4.0; 8.0; 20.0; 1.0 |]

bubbleSort arr
// val it: float array = [|1.0; 4.0; 5.0; 8.0; 20.0|]

let result4 = bubbleSort (List.toArray data)
let bool3 = result = (Array.toList result4)


let rec getHighest list =
    match list with
    | head1 :: head2 :: tail when head1 > head2 -> getHighest (head1 :: tail)
    | _ :: head2 :: tail -> getHighest (head2 :: tail)
    | [ head1 ] -> head1
    | _ -> failwith "Unrecognized pattern"

/// Functional bubble sort
let bubbleSort_func list =
    let rec innerBubbleSort sorted = function
       | [] -> sorted
       | l ->
           let h = getHighest l
           let x, y = List.partition (fun i -> i = h) l
           innerBubbleSort (x @ sorted) y
    innerBubbleSort [] list

let result5 = bubbleSort_func data
let bool4 = result = result5


// Merge Sort

let split list =
    let rec loop lst acc1 acc2 =
        match lst with
        | []  -> acc1, acc2
        | [x] -> (x::acc1, acc2)
        | x::y::tail ->
            loop tail (x::acc1) (y::acc2)
    loop list [] []

let rec merge lst1 lst2 =
    match lst1, lst2 with
    | x, [] -> x
    | [], y -> y
    | x::tx, y::ty ->
        if x <= y then x::merge tx lst2
        else y::merge lst1 ty

let rec mergeSort list =
    match list with
    | []  -> []
    | [x] -> [x]
    | _   ->
        let (lst1, lst2) = split list
        merge (mergeSort lst1) (mergeSort lst2)

let result6 = mergeSort data
let bool5 = result = result6

let testData = List.init 10000 (fun _ -> rand.Next())
let result7 = mergeSort testData
