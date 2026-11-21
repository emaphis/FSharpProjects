// F# regex currency symbols
// he \p{Sc} regular expression can be used to look for currency symbols.

open System
open System.Text.RegularExpressions

Console.OutputEncoding = Text.Encoding.UTF8

let content = @"Currency symbols: ฿ Thailand bath, ₹  Indian rupee,
    ₾ Georgian lari, $ Dollar, € Euro, ¥ Yen, £ Pound Sterling";

let pattern = @"\p{Sc}";

let rx = Regex(pattern, RegexOptions.Compiled)

let matches =
    rx.Matches(content)
    |> Seq.map (fun m -> m.Value, m.Index)

matches
|> Seq.iter (fun (e, idx) -> printfn "%s at %d" e idx)

(*
฿ at 18
₹ at 35
₾ at 56
$ at 73
€ at 83
¥ at 91
£ at 98
*)
