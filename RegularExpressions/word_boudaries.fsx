// F# regex word boundaries
// The metacharacter \b is an anchor which matches at a position that is called a word boundary.
// It allows to search for whole words.

open System.Text.RegularExpressions

let text = "This island is beautiful"

let rx = Regex(@"\bis\b", RegexOptions.Compiled)

let matches =
    rx.Matches(text)
    |> Seq.map (fun m -> m.Value, m.Index)

matches
|> Seq.iter (fun (e, idx) -> printfn "%s at %d" e idx)

(*
is at 12
val it: unit = ()
*)
