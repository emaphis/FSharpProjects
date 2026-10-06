// Sorting lazily

// Quicksort
let rec quickSort = function
  | [] -> []
  | n::ns -> 
    let lessThan, greaterEqual = List.partition (( > ) n) ns
    quickSort lessThan @ n :: quickSort greaterEqual

let rand = new System.Random()
let data = List.init 10 (fun _  -> rand.Next())
let result = quickSort data
let result2 = List.sort data


// Functional implementation of quick sort - lazy and incremental.
let rec quickSort_func (pxs:seq<_>) = 
    seq {
        match Seq.toList pxs with
        | p::xs -> let lessThan, greaterEqual = List.partition ((>=) p) xs
                   yield! quickSort lessThan; yield p; yield! quickSort greaterEqual
        | _ -> ()
    }

let result3 = Seq.toList (quickSort data)
