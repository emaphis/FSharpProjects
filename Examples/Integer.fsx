(*  F# - Data *)


(* single byte integer *)
let x = 268.97f
let y = 323.58f
let z = x + y

printfn $"x: %f{x}"
printfn $"y: %f{y}"
printfn $"z: %f{z}"


(* unsigned 8-bit natural number *)
let p = 2uy
let q = 4uy
let r = p + q

printfn $"p: %i{p}"
printfn $"q: %i{q}"
printfn $"r: %i{r}"


(* signed 16-bit integer *)
let a = 12s
let b = 24s
let c = a + b

printfn $"a: %i{a}"
printfn $"b: %i{b}"
printfn $"c: %i{c}"

(* signed 32-bit integer *)
let d = 212l
let e = 504l
let f = d + e

printfn $"d: %i{d}"
printfn $"e: %i{e}"
printfn $"f: %i{f}"
