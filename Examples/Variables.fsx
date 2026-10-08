(* F# - Variables *)

(* Variable Declaration in F# *)

let x = 10
let y = 20
let z = x + y

printfn $"x: %i{x}"
printfn $"y: %i{y}"
printfn $"z: %i{z}"


(* Variable Definition With Type Declaration *)
let l: int32 = 10
let m: int32 = 20
let n: int32 = x + y

printfn $"l: %i{l}"
printfn $"m: %i{m}"
printfn $"n: %i{n}"


let p: float = 15.99
let q: float = 20.78
let r: float = p + q

printfn $"p: %g{p}"
printfn $"q: %g{q}"
printfn $"r: %g{r}"
