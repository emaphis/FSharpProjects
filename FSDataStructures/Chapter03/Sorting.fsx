// F# implementation of sorting algorithms.

let rand = new System.Random()

let data = Array.init 10 (fun _  -> rand.Next())

// Bubble sort

/// Swaps two elements of an array.
let swap x y (array : 'arr []) =
    let temp = array[x]
    array[x] <- array[y]
    array[y] <- temp

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

let res1 = bubbleSort data

let rec getHighest list =
    match list with
    | head1 :: head2 :: tail when head1 > head2 -> getHighest (head1 :: tail)
    | head1 :: head2 :: tail -> getHighest (head2 :: tail)
    | head1 :: [] -> head1
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

bubbleSort_func data