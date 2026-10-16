(* 32-bit signed floating point number *)
(* 7 significant digits *)

let d = 212.098f
let e = 504.768f
let f = d + e

printfn $"d: %f{d}"
printfn $"e: %f{e}"
printfn $"f: %f{f}"

(* 64-bit signed floating point number *)
(* 15-16 significant digits *)

let x = 21290.098
let y = 50446.768
let z = x + y

printfn $"x: %g{x}"
printfn $"y: %g{y}"
printfn $"z: %g{z}"
