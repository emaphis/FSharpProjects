(* F# - Sets *)

(* Creating Sets
-   By creating an empty set using Set.empty and adding items using the add function.
-   Converting sequences and lists to sets.
*)

let set1 = Set.empty.Add(1).Add(5).Add(7).Add 9
printfn $"The new set: %A{set1}"

let weekdays = Set.ofList ["mon"; "tues"; "wed"; "thurs"; "fri"]
printfn $"The list set: {weekdays}"

let set2 = Set.ofSeq [ 1 .. 2 .. 10 ]
printfn $"The sequence set: %A{set2}"


(* Basic Operations on Sets *)

(* Example *)

let a = Set.ofSeq [ 1 ..2.. 20 ]
let b = Set.ofSeq [ 1 ..3 .. 20 ]
let c = Set.intersect a b
let d = Set.union a b
let e = Set.difference a b


printfn "Set a: "
Set.iter (fun x -> printf $"{x} ") a
printfn""

printfn "Set b: "
Set.iter (fun x -> printf $"{x} ") b
printfn""

printfn "Set c = set intersect of a and b : "
Set.iter (fun x -> printf $"{x} ") c
printfn""

printfn "Set d = set union of a and b : "
Set.iter (fun x -> printf $"{x} ") d
printfn""

printfn "Set e = set difference of a and b : "
Set.iter (fun x -> printf $"{x} ") e
printfn""
