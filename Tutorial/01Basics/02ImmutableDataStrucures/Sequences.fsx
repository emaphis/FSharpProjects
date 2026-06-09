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
//open System.Collections

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


// val initInfinite : (int -> 'T) -> seq<'T>
// Generates a sequence consisting of an infinite number of elements.

let infinite = Seq.initInfinite (fun x -> x * x)
let fiveSquares = Seq.take 5 infinite
do printfn $"five squares = %A{fiveSquares}"
//five squares = seq [0; 1; 4; 9; ...]


// val map : ('T -> 'U) -> seq<'T> -> seq<'U>
// Maps a sequence of type 'a to type 'b.

let nums = Seq.map (fun x -> x * x + 2) (seq [3;5;4;3])
do printfn $"nums = %A{nums}"
//nums = seq [11; 27; 18; 11]


// val item : int -> seq<'T> -> 'T
// Returns the nth value of a sequence.

let num1 = Seq.item 3 (seq {for n in 2..9 do yield n})
// val num1: int = 5


// val take : int -> seq<'T> -> seq<'T>
// Returns a new sequence consisting of the first n elements of the input sequence.

let nums3 = Seq.take 3 (seq {1..6})
do printfn $"nums3 = %A{nums3}"
//nums3 = seq [1; 2; 3]


// val takeWhile : ('T -> bool) -> seq<'T> -> seq<'T>
// Return a sequence that, when iterated, yields elements of the underlying sequence while the given predicate returns true, and returns no further elements.

let sequence3 = Seq.takeWhile (fun elem -> elem < 10) (seq {for i in 0..20 do yield i+1})
do printfn $"sequence3 = %A{sequence3}"
//sequence3 = seq [1; 2; 3; 4; ...]


// val unfold : ('State -> ('T * 'State) option) -> 'State seed -> seq<'T>
// The opposite of fold: this function generates a sequence as long as the generator function returns Some.

open System.Numerics

let fibs = (0I, 1I) |> Seq.unfold (fun (a, b) -> Some(a, (b, a+b)))
Seq.iter (fun x -> printf $"{x} ") (Seq.take 20 fibs)
// 0 1 1 2 3 5 8 13 21 34 55 89 144 233 377 610 987 1597 2584 4181 val it: unit = ()
