// F# regex Matches
//The `Matches` method searches an input string for all occurrences of a regular expression and returns all the matches.
// Multiple techniques exist for extracting matches
// and processing results efficiently.

open System.Text.RegularExpressions

// Example 1

let content =
    @"Foxes are omnivorous mammals belonging to several genera
of the family Canidae. Foxes have a flattened skull, upright triangular ears,
a pointed, slightly upturned snout, and a long bushy tail. Foxes live on every
continent except Antarctica. By far the most common and widespread species of
fox is the red fox."


// The pattern "(?i)fox(es)?" uses case-insensitive matching with optional
// plural form, demonstrating flags and optional group constructs.
let found =
    seq {
        for m in Regex.Matches(content, "(?i)fox(es)?") do
            yield m.Value, m.Index
    }

found
|> Seq.iter (fun (e, idx) -> printfn $"%s{e} at %d{idx}")

(*
Foxes at 0
Foxes at 80
Foxes at 194
fox at 292
fox at 307
val it: unit = ()
*)


// Example 2

let found2 =
    Regex.Matches(content, "(?i)fox(es)?")
    |> Seq.map (fun m -> m.Value, m.Index)

found2
|> Seq.iter (fun (e, idx) -> printfn $"%s{e} at %d{idx}")

(*
Foxes at 0
Foxes at 80
Foxes at 194
fox at 292
fox at 307
val it: unit = ()
*)
