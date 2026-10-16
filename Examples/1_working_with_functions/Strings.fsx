(* F# - Strings *)


(* Ways of ignoring the Escape Sequence *
Using the `@` symbol.
Enclosing the sting in triple quotes
*)

// Using a verbatim string

let xmlData = @"<book author = ""Lewis, C.S"" title = ""Narnia"">"
printfn $"%s{xmlData}"
// <book author = "Lewis, C.S" title = "Narnia">

(* Basic Operators on String *)

(* Example 1 *)
let collectTesting inputS =
   String.collect (fun c -> $"%c{c} ") inputS
let str1 = collectTesting "Happy New Year!"
printfn $"%s{str1}"
// val str1: string = "H a p p y   N e w   Y e a r ! "


(* Example 2 *)
let strings = [ "Tutorials Point"; "Coding Ground"; "Absolute Classes" ]
let ourProducts = String.concat "\n" strings
printfn $"%s{ourProducts}"


(* Example 3 *)
printfn "%s" <| String.replicate 10 "*! "
// *! *! *! *! *! *! *! *! *! *!
