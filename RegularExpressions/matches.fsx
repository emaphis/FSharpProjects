// F# regex Matches
//The `Matches` method searches an input string for all occurrences of a regular expression and returns all the matches.

open System.Text.RegularExpressions

let content =
    @"Foxes are omnivorous mammals belonging to several genera
of the family Canidae. Foxes have a flattened skull, upright triangular ears,
a pointed, slightly upturned snout, and a long bushy tail. Foxes live on every
continent except Antarctica. By far the most common and widespread species of
fox is the red fox."

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
