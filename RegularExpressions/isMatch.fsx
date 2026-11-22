// F# isMatch
// Basic pattern matching tests whether text contains a specific pattern
// without extracting matched content. IsMatch returns boolean results
// for validation and filtering operations.

open System.Text.RegularExpressions

let words =
    [ "Seven"
      "even"
      "Maven"
      "Amen"
      "eleven" ]

let rx = Regex(@".even", RegexOptions.Compiled)

words
|> List.map
    (fun e ->
        if rx.IsMatch(e) then
            printfn $"{e} matches:"
        else
            printfn $"{e} does not match")

// Seven matches:
// even does not match
// Maven does not match
// Amen does not match
// eleven matches:

