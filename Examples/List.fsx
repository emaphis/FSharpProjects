(* F# - Lists *)


(* Creating and Initializing a List *)

(*
Using list literals.

Using cons (::) operator.

Using the List.init method of List module.

Using some syntactic constructs called List Comprehensions
*)



(* List Literals *)

let list1 = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
printfn $"This list: %A{list1}"


(* The cons (::) operator *)

let list2 = 1::2::3::4::5::6::7::8::9::10::[]
printfn $"This list: %A{list2}"

let empty = []
printfn $"This list is empty: %A{empty}"



(* List Comprehensions *)

let list3 = [ 1 .. 10 ]
printfn $"This list: %A{list3}"

let list4 = [ 'a' .. 'm' ]
printfn $"This list: %A{list4}"


(* List `init` Method *)

let list5 =
    List.init 5 (fun index -> index, index * index, index * index * index)
printfn $"This list: %A{list5}"

(* using yield operator *)

let list6 = [ for a in 1 .. 10  do yield a * a]
printfn $"This list: %A{list6}"

let list7 = [ for a in 1 .. 100 do if a % 3 = 0 && a % 5 = 0 then yield a]
printfn $"The list: %A{list7}"

let list8 = [for a in 1 .. 3 do yield! [ a .. a + 3 ] ]
printfn $"The list: %A{list8}"


(* Properties of List Data Type *)
printfn $"list1 is %A{list1}"
printfn $"list1.IsEmpty is %b{list1.IsEmpty}"
printfn $"list1.Length is %d{list1.Length}"
printfn $"list1.Head is %d{list1.Head}"
printfn $"list1.Tail.Head is %d{list1.Tail.Head}"
printfn $"list1.Tail.Tail.Head is %d{list1.Tail.Tail.Head}"
printfn $"list1.Item(1) is %d{list1.Item 1}"


(* Basic Operators on List *)

(* Example 1 - Rev *)

let reverse lt =
    let rec loop acc = function
       | [] -> acc
       | hd :: tl -> loop (hd :: acc) tl
    loop [] lt

printfn $"The original list: %A{list1}"
printfn $"The reversed list: %A{reverse list1}"
printfn $"Use the `rev` method: %A{List.rev list1}"


(* Example 2 - Filter *)

let list9 = list1 |> List.filter (fun x -> x % 2 = 0)
printfn $"The original list: %A{list1}"
printfn $"The Filtered list: %A{list9}"


(* Example 3 - Map *)

let list10 = list1 |> List.map (fun x -> (x * x).ToString())
printfn $"The original list: %A{list1}"
printfn $"The Mapped list: %A{list10}"


(* Example 4 - Append *)

let list11 = [1; 2; 3; 4; 5 ]
let list12 = [6; 7; 8; 9; 10]
let list13 = List.append list11 list12

printfn $"The first list: %A{list11}"
printfn $"The second list: %A{list12}"
printfn $"The append list: %A{list13}"

let lt1 = ['a'; 'b';'c' ]
let lt2 = ['e'; 'f';'g' ]
let lt3 = lt1 @ lt2

printfn $"The first list: %A{lt1}"
printfn $"The second list: %A{lt2}"
printfn $"The append list: %A{lt3}"


(* Example 5 - Sort, Sum *)

let list14 =  [9.0; 0.0; 2.0; -4.5; 11.2; 8.0; -10.0]
printfn $"The list: %A{list14}"

let list15 = List.sort list14
printfn $"The sorted list: %A{list15}"

let s = List.sum list14
let avg = List.average list14
printfn $"The sum: %f{s}"
printfn $"The average: %f{avg}"


(* Example 6 - Fold *)

let sumList list = List.fold (fun acc elem -> acc + elem) 0 list
let list16 = [ 1 .. 10]
let sum1 = sumList list16
printfn $"Sum of the elements of list %A{list16} is %d{sum1}."
// let sumList list = List.fold (fun acc elem -> acc + elem) 0 list
