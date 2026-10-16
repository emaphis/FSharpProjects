(* F# - Options *)

(* Using Options *)

let div x y = x / y
let res1 = div 20 5
printfn $"Result: %d{res1}"
// 4

// If second argument is zere program throws an MatchFailureException
// let res2 = div 20 0
// System.DivideByZeroException: Attempted to divide by zero.


(* Example *)

let divO x y =
    match y with
    | 0 -> None
    | _ -> Some(x/y)

let res3 : int option = divO 20 4
printfn $"Result: %A{res3} "
// val res3: int option = Some 5


(* Example 1 *)
let checkPositive (a : int) =
   if a > 0 then
      Some a
   else
      None

let res4 : int option = checkPositive -31
printfn $"Result: %A{res4}"
// val res4: int option = None


(* Example 2 *)

(*
et div x y =
   match y with
   | 0 -> None
   | _ -> Some(x/y)
*)

let res5 : int option = divO 20 4
printfn $"Result: %A{res5}"
printfn $"Result: %A{res5.Value}"
//Result: Some 5
//Result: 5


(* Example 3 *)
let isHundred = function
    | Some 100 -> true
    | Some _ -> false
    | _  -> false

printfn $"%A{isHundred (Some 45)}"
printfn $"%A{isHundred (Some 100)}"
printfn $"%A{isHundred None}"
