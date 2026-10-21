// Sequence and Enums

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


// Enumerating a CSV file

open System.IO

let dataFile = $"{__SOURCE_DIRECTORY__}\ProgrammingLanguages.txt"

let data =
    seq { use s = new StreamReader(dataFile)
          while not s.EndOfStream do yield s.ReadLine() }

data |> printfn "%A"

let XSVEnumerator fileName =
    seq { use s = File.OpenText(fileName)
          while not s.EndOfStream do
             let line = s.ReadLine()
             let tokens = line.Split [|'\t'|]
             yield tokens }

let xsv = XSVEnumerator(dataFile)

xsv |> Seq.iter (string >> printfn "line %s")
// line System.String[] ...

xsv |> Seq.iter (Array.length >> printfn"line has %d entries")
// line has 3 entries ...

xsv |> Seq.iter (Array.map (fun s ->s.Length) >> printfn "lengths of entries: %A")
// lengths of entries: [|1; 26; 4|]
// lengths of entries: [|3; 17; 4|]
// lengths of entries: [|2; 16; 4|]
// ...

xsv |> Seq.iter (Array.map (fun s -> s.ToString()) >> printfn "Entries: %A")

xsv |> Seq.iter (fun s -> printfn $"%s{s[0]} %s{s[1]} %s{s[2]}")


// Query expressions

// Data definitions

/// Represents a programming language
type ProgrammingLanguage =
    { id : int; name : string; publishYear : int}
    override x.ToString() = $"%s{x.name} (%i{x.publishYear})"

/// Represents a programmer
type Developer =
    { id : int; Name : string  }
    override x.ToString() = $"%s{x.Name}"

type Developer_PL = { developerID : int; pl_ID : int }

// Tables

let ProgrammingLanguages =
    [   { id = 1; name = "C"; publishYear = 1972; }
        { id = 2; name = "C++"; publishYear = 1985; }
        { id = 3; name = "C#"; publishYear = 2000; }
        { id = 4; name = "F#"; publishYear = 2005; }
        { id = 5; name = "Java"; publishYear = 1991; }
        { id = 6; name = "Pascal"; publishYear = 1970; }
        { id = 7; name = "Python"; publishYear = 1997; }
        { id = 8; name = "Basic"; publishYear = 1964; }
        { id = 9; name = "COBOL"; publishYear = 1959; }
        { id = 10; name = "FORTRAN"; publishYear = 1957; }
        { id = 11; name = "LISP"; publishYear = 1956; }
        { id = 12; name = "Perl"; publishYear = 1987; }
        { id = 13; name = "JavaScript"; publishYear = 1995; }
        { id = 14; name = "Scheme"; publishYear = 1975; }
        { id = 15; name = "Clojure"; publishYear = 2007; }
        { id = 16; name = "Haskell"; publishYear = 1990; }
        { id = 17; name = "Ruby"; publishYear = 1995; }
        { id = 18; name = "OCaml"; publishYear = 1996; }
        { id = 19; name = "Scala"; publishYear = 2003; }]

let Developers =
    [   { id = 1; Name = "Dennis Ritchie"; }
        { id = 2; Name = "Bjarne Stroustrup";}
        { id = 3; Name = "Anders Hejlsberg";}
        { id = 4; Name = "Don Syme";}
        { id = 5; Name = "James A. Gosling";}
        { id = 6; Name = "Nicklaus Wirth";}
        { id = 7; Name = "Guido van Rossum";}
        { id = 8; Name = "Kemeny and Kurtz";}
        { id = 9; Name = "Grace Hopper";}
        { id = 10; Name = "John Backus";}
        { id = 11; Name = "John McCarthy";}
        { id = 12; Name = "Larry Wall";}
        { id = 13; Name = "Brendan Eich";}
        { id = 14; Name = "Steele and Sussman";}
        { id = 15; Name = "Rich Hickey";}
        { id = 16; Name = "Jones, Augustsson, et al";}
        { id = 17; Name = "Yukihiro Matsumoto, et al";}
        { id = 18; Name = "Xavier Leroy et al.";}
        { id = 19; Name = "Martin Odersky";}]


let Developers_PLs =
    [ { developerID = 1; pl_ID  = 1; }
      { developerID = 2; pl_ID  = 2; }
      { developerID = 3; pl_ID  = 3; }
      { developerID = 4; pl_ID  = 4; }
      { developerID = 5; pl_ID  = 5; }
      { developerID = 6; pl_ID  = 6; }
      { developerID = 7; pl_ID  = 7; }
      { developerID = 8; pl_ID  = 8; }
      { developerID = 9; pl_ID  = 9; }
      { developerID = 10; pl_ID  = 10; }
      { developerID = 11; pl_ID  = 11; }
      { developerID = 12; pl_ID  = 12; }
      { developerID = 13; pl_ID  = 13; }
      { developerID = 14; pl_ID  = 14; }
      { developerID = 15; pl_ID  = 15; }
      { developerID = 16; pl_ID  = 16; }
      { developerID = 17; pl_ID  = 17; }
      { developerID = 18; pl_ID  = 18; }
      { developerID = 19; pl_ID  = 19 ;}]

// Queries

// simple query operation finding out the books
// published in the year 2005
query { for pl in ProgrammingLanguages do
            where (pl.publishYear = 2005)
            select (pl.ToString()) }
// val it: string seq = seq ["F# (2005)"]

// we use the skip and take operators of the query expression:
let getPLPageBySize pageSize pageNumber =
    query { for pl in ProgrammingLanguages do
                skip (pageSize * (pageNumber - 1))
                take pageSize
                select (pl.ToString()) }

getPLPageBySize 4 2
// val it: string seq =
//  seq ["Java (1991)"; "Pascal (1970)"; "Python (1997)"; "Basic (1964)"]

// sorted by the publishing year
let getPLPageByYear year =
    query { for pl in ProgrammingLanguages do
                sortBy pl.publishYear
                skipWhile (pl.publishYear < year)
                takeWhile (pl.publishYear = year)
                select (pl.ToString()) }

getPLPageByYear 2007
// val it: string seq = seq ["Clojure (2007)"]


// equivalent to
let getPLPageByYear1 year =
    query { for pl in ProgrammingLanguages do
                where (pl.publishYear = year)
                select (pl.ToString()) }

getPLPageByYear1 2007

// Query expressions provide various options including an aggregate such as count:

query { for _ in ProgrammingLanguages do
            count }
// val it: int = 19

// generic SQL-like selections based on parameters such as:

query { for pl in ProgrammingLanguages do
            select pl.publishYear }
// val it: int seq = seq [1972; 1985; 2000; 2005; ...]

// You have the ability to access nth elements:

query { for _dev in Developers do nth 2 }
// val it: Developer = { id = 3 Name = "Anders Hejlsberg" }

// You can also aggregate operations such as group by

query { for pl in ProgrammingLanguages do
            groupBy pl.publishYear into pl
            sortBy pl.Key
            select (pl.Key, pl) }


// Creating sequences from collections

let seqFromArray =  [| 1 .. 10 |] :> int seq

let seqFromArray2 = [| 1 .. 10 |] |> Seq.ofArray

// Sequences are monads
let values = seq { for x in 0 .. 9 -> x = x * x }

values
//val it: bool seq = seq [true; true; false; false; ...]
