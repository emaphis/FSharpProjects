namespace FSCollectionExtensions

open System.Collections.Generic


module List =

    /// Skips the first `n` elements of the list
    let rec skip n list =
        match n, list with
        | _, []     -> []
        | 0, list -> list
        | n, _ :: tl -> skip (n - 1) tl


module Seq =

    /// Reverse the elements in the sequence
    let rec rev (s: seq<'a>) =
        let stack = Stack<'a>()
        s |> Seq.iter stack.Push
        seq {
            while stack.Count > 0 do
                yield stack.Pop()
        }
