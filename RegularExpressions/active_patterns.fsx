// Active patterns
// We can use regular expression with active patterns and match expression.
// Active patterns are named partitions of input data which can be used later
// in a pattern matching expression.

open System.Text.RegularExpressions

let (|RegEx|_|) p i =
    let m = Regex.Match(i, p)

    if m.Success then
        Some m.Groups
    else
        None

let checkrgx (msg) =
    match msg with
    | RegEx @"\d+" g -> printfn $"Digit: %A{g}"
    | RegEx @"\w+" g -> printfn $"Word: %A{g}"
    | _ -> printfn "Not recognized"


let out1 = checkrgx "an old falcon"
let out2 = checkrgx "1984"
let out3 = checkrgx "3 hawks"

(*
Word: seq [an]
Digit: seq [1984]
Digit: seq [3]
*)
