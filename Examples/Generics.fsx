(* F# - Generics *)

(* Syntax *)

(* Generic Function *)
let printFunc x y =
    printfn $"1: %A{x}, %A{y}"


printFunc 10.0 20.0


let printFunction (x: 'a) (y: 'a) =
   printfn "2: %A %A" x y

printFunction 10.0 20.0


(* Generic Class *)

type genericClass (x: 'a) =
    do printfn $"%A{x}"


let gr = new genericClass "zara"
let gs = genericClass( seq { for i: int in 1 .. 10 -> i, i*i }  )

// "zara"
//seq [(1, 1); (2, 4); (3, 9); (4, 16); ...]
