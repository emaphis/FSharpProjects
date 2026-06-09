// Immutable Data Structures
// Sequences

// Defining Sequences

(*
Sequences are defined using the syntax:
seq { expr }
*)

seq { 1 .. 10 }

seq { 1 .. 2 .. 10 }

seq {10 .. -1 .. 0}

seq { for a in 1 .. 10 do yield a, a*a, a*a*a }


// sequence are lazily evaluated

let intList =
    [ for a in 1 .. 10 do
        printfn $"intList: %i{a}"
        yield a ]

// lazy
let intSeq =
    seq { for a in 1 .. 10 do
            printfn $"intSeq: %i{a}"
            yield a }

Seq.item 3 intSeq
Seq.item 7 intSeq

// trillion lazy bignums
seq { 1I .. 1000000000000I }

// sequences can represent an infinite number of elements:

let allEvens =
    let rec loop x = seq { yield x; yield! loop (x + 2) }
    loop 0

for a in (Seq.take 5 allEvens) do
    printfn $"{a}"


// Iterating Through Sequences Manually

open System
open System.Collections

let evens = seq { 0 .. 2 .. 10 } 

let main1() =
    let evensEnumerator = evens.GetEnumerator()  // returns IEnumerator<int>
    while evensEnumerator.MoveNext() do
        printfn $"evensEnumerator.Current: %i{evensEnumerator.Current}"

main1()


// The Seq Module

// val append : seq<'T> -> seq<'T> -> seq<'T>
// Appends one sequence onto another sequence.

let test = Seq.append (seq{1..3}) (seq{4..7})
test
// val it: int seq = seq [1; 2; 3; 4; ...]


//val choose : ('T -> 'U option) -> seq<'T> -> seq<'U>
//Filters and maps a sequence to another sequence.

let thisworks = seq { for nm in [ Some("James"); None; Some("John") ] |> Seq.choose id -> nm.Length }
printfn $"this works = %A{thisworks}"
//this works = seq [5; 4]

// val distinct : seq<'T> -> seq<'T>
// Returns a sequence that filters out duplicate entries.

let dist = Seq.distinct (seq [1; 2; 2; 6; 3; 2])
printfn $"dist = %A{dist}"
// let dist = Seq.distinct (seq[1;2;2;6;3;2])

// val exists : ('T -> bool) -> seq<'T> -> bool
// Determines if an element exists in a sequence.

let equalsTwo x = x = 2
let exist = Seq.exists equalsTwo (seq [3 .. 9])
// false

// val filter : ('T -> bool) -> seq<'T> -> seq<'T>
// Builds a new sequence consisting of elements filtered from the input sequence.

let evens' = Seq.filter (fun x-> x%2 = 0) (seq{0..9})
printfn $"evens = %A{evens'}"
// evens = seq [0; 2; 4; 6; ...]

// val fold : ('State -> 'T -> 'State) -> 'State -> seq<'T> -> 'State
// Repeatedly applies a function to each element in the sequence from left to right.

let sumSeq sequence1 = Seq.fold (fun acc elem -> acc + elem) 0 sequence1
Seq.init 10 (fun index -> index * index)
|> sumSeq
|> printfn "The sum of the elements is %d."
// The sum of the elements is 285.





