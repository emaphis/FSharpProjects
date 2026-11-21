// F# regex alternations
// The alternation operator | enables to create a regular expression with several choices.

open System.Text.RegularExpressions

let users = ["Jane"; "Thomas"; "Robert";
    "Lucy"; "Beky"; "John"; "Peter"; "Andy"]

let rx = Regex("Jane|Beky|Robert", RegexOptions.Compiled)

users
|> List.filter rx.IsMatch
|> List.iter (printfn "%s")

(*
Jane
Robert
Beky
val it: unit = ()
*)
