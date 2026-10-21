// Seqencce and Enums

// A data structure that contains elements that are evaluated demand.
// Sequence seq<'a> or 'a seq is just IEnumerable<'a> for a generic type a,
// which is now a commonly used construct since the introduction of LINQ to the .NET Framework.
// IEnumerable<'a> is defined as an interface that exposes an enumerator to 
// iterate over a collection.

let countToTen = seq { 1 .. 10 }

countToTen
// val it: int seq = seq [1; 2; 3; 4; ...]

// Following are some examples for creating sequences using seq:

seq { 0..10 }
// val it: int seq = seq [0; 1; 2; 3; ...]

seq { 0.0 .. 10.0 }
// val it: float seq = seq [0.0; 1.0; 2.0; 3.0; ...]

seq { 'a' .. 'z' }
// val it: float seq = seq [0.0; 1.0; 2.0; 3.0; ...]

// A simple stepping instruction in the sequences

seq { 0 .. 10 .. 100 }
// val it: int seq = seq [0; 10; 20; 30; ...]

seq { 99 .. -1 .. 0 }
// val it: int seq = seq [99; 98; 97; 96; ...]


// for yield expression

let intExp =
    seq {
        for i in 0 .. 999 do
            yield i
    }

intExp
// val it: int seq = seq [0; 1; 2; 3; ...]


let intExp1 =
    seq {
        for i in 0 .. 999 -> i
    }

intExp1
// val it: int seq = seq [0; 1; 2; 3; ...]

let intExp2 = 
    seq { 
        for i in 0 .. 10 .. 100 do
            yield! seq { i .. 1 .. i+9 }
    }

Seq.iter ( fun num -> printf $"%d{num} " ) intExp2


// Seq.init

let integers = Seq.init 1000 ( fun i -> i + 1 )

printfn $"%A{integers}"
// seq [1; 2; 3; 4; ...]


// `Seq.initInfinite` lazy 'infinite' sequences

let integersInf = Seq.initInfinite ( fun i -> i + 1 )

integersInf
|> Seq.skip 1
|> Seq.take 4
|> Seq.iter (printf "%d ")


let  intsInf = Seq.initInfinite (fun i -> 
    let n = float (i + 1)
    1.0 / (n * n * n))

intsInf
|> Seq.take 4
|> Seq.iter (printf "%g ")
// 1 0.125 0.037037 0.015625 val it: unit = ()


// `Seq.iter` provides support for iteration over a sequence.

seq { 0 .. 9 } |> Seq.iter (printfn "%i")


// A sequence can be filtered using the `Seq.filter`

let arr = [| 1 .. 1000 |]

let odds =
    arr
    |> Seq.filter (fun i -> i%2 <> 0)

odds
// val it: int seq = seq [1; 3; 5; 7; ...]

// `Map` operations allow you to apply a function to an entire sequence.

seq { 0 .. 999 } |> Seq.map (fun i -> i * i)
// val it: int seq = seq [0; 1; 4; 9; ...]


// The Seq module also offers support for sorting a sequence via `Seq.sortBy`.

let seq1 = seq { 10 .. -1 .. 1} |> Seq.sort

seq1 
// al it: int seq = seq [1; 2; 3; 4; ...]


// Seq.fold, Seq,reduce

Seq.fold
// val it: (('a -> 'b -> 'a) -> 'a -> 'b seq -> 'a)

seq { 1 .. 100 } |> Seq.fold (fun x y -> x + y) 0
// val it: int = 5050

seq { 1 .. 100 } |> Seq.fold (+) 0
// val it: int = 5050

Seq.fold (+) 0.0 [ 1.0; 2.0; 3.0 ]
// val it: float = 6.0

seq { 1 .. 100 } |> Seq.reduce (+)
// val it: int = 5050''

seq { 1 .. 100 } |> Seq.sum
// val it: int = 5050


seq { 1 .. 100 } |> Seq.map float |> Seq.average
// val it: float = 50.5

