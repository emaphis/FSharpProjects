module Sequences

module LazyEvaluation =

    // Lazy types
    // A thunk of place holder for a future evaluation
    // `lazy` of `Lazy<_>.Create

    // Example 3-10. Using lazy evaluation

    open System

    // Define two lazy values
    let x = Lazy<int>.Create(fun () -> printfn "Evaluating x ..."; 10)
    let y = lazy (printfn "Evaluating y..."; x.Value + x.Value)

    //val x: Lazy<int> = Value is not created.
    //val y: Lazy<int> = Value is not created.

    // Directly requesting y's value will forc its evaluation
    let num1 = y.Value
    //Evaluating y...
    //Evaluating x ...
    //val num1: int = 20

    // Accessing y's value agains will used a cahce value.
    let num2 = y.Value
    //val num1: int = 20


module Sequences =

    // The most common use of lazy evaluation is through sequences

    let seqOfNumbers = seq { 1 .. 5 }
    //val seqOfNumbers: int seq

    do seqOfNumbers |> Seq.iter (printfn "%d")

    // Sequences are lzay

    //  Example 3-11. A sequence of all integers

    // Sequence of all positive integers.
    let allPositiveIntsSeq =
        seq { for i in 1 .. System.Int32.MaxValue do
                    yield i }
    //val allPositiveIntsSeq: int seq

    allPositiveIntsSeq
    //val it: int seq = seq [1; 2; 3; 4; ...]

    // List of all possitive interegers
    //let allPositiveInts = [ for i in 1 .. System.Int32.MaxValue -> i ]
    // fails


module SequenceExpressions =

    // Use similar syntax as list comprehensions as list comprehenstions

    let alphabet = seq { for ch in 'A' .. 'Z' -> ch }

    let someChars = Seq.take 4 alphabet
    someChars
    //val it: char seq = seq ['A'; 'B'; 'C'; 'D']

    // Sequence with a side effect.
    let noisyAlphabet =
        seq {
            for ch in 'A' .. 'Z' do
                printfn $"Yielding {ch}..."
                yield ch
        }
    //val noisyAlphabet: char seq

    let fifthLetter = Seq.item 4 noisyAlphabet
    //Yielding A...
    //Yielding B...
    //Yielding C...
    //Yielding D...
    //Yielding E...
    //val fifthLetter: char = 'E'
 

    // Example 3-12. Sequence for listing all files under a folder

    open System.IO

    let rec allFilesUnder basePath =
        seq {
            // yield all files in the base folder
            yield! Directory.GetFiles(basePath)

            // yield all files in its fub folder
            for subdir in Directory.GetDirectories(basePath) do
                yield! allFilesUnder subdir
        }
    //val allFilesUnder: basePath: string -> string seq

    //let stuff = allFilesUnder @"C:\temp\accounts"
    //printfn "%A" stuff


module SeqModuleFunctions =

    //Seq.take
    //val it: (int -> 'a seq -> 'a seq)
    // Returns the first n items from a sequence

    open System

    let randomSequence =
        seq {
            let rng = new Random()
            while true do
                yield rng.Next()

        }
    //val randomSequence: int seq

    let rands =  randomSequence |> Seq.take 3
    printf "%A" rands
    //seq [1661191131; 530481539; 1601451948]val rands: int seq


    //Seq.unfold
    //val it: (('a -> ('b * 'a) option) -> 'a -> 'b seq)
    // Generates a sequence from a provided function

    // Example 3-13. Using Seq.unfold

    // Generates the next element of the Fibonacci sequence give the previous
    // tow elements. To be used with Seq.unfold

    let nextFibUnder100 (a, b) =
        if a + b > 100 then None
        else
            let nextValue = a + b
            Some(nextValue, (nextValue, a))
    //val nextFibUnder100: a: int * b: int -> (int * (int * int)) option

    let fibsUnder100 = Seq.unfold nextFibUnder100 (0, 1)
    //val fibsUnder100: int seq

    let fibList = Seq.toList fibsUnder100
    do printfn "%A" fibList
    //[1; 1; 2; 3; 5; 8; 13; 21; 34; 55; 89]


module AggregateOperators =

    //Seq.iter
    //val it: (('a -> unit) -> 'a seq -> unit)
    // Iterates through each item in teh sequence producing sied effecting opperations

    // Print odd numbers under 10
    let oddsUnderN n = seq { for i in 1 .. 2 .. n -> i }
    Seq.iter (printfn "%d") (oddsUnderN 10)


    //Seq.map
    //val it: (('a -> 'b) -> 'a seq -> 'b seq)

    // Sequence of words (Arrays are compatible with sequences)
    let words = "The wuick brown fox jumped over the lazy dog".Split( [| ' ' |])
    //val words: string array =
    //      [|"The"; "wuick"; "brown"; "fox"; "jumped"; "over"; "the"; "lazy"; "dog"|]

    // Map string to string, length tuples
    let tuples = words |> Seq.map (fun word -> word, word.Length)
    do printfn "%A" tuples
    //seq [("The", 3); ("wuick", 5); ("brown", 5); ("fox", 3); ...]


    // Seq.fold
    //val it: (('a -> 'b -> 'a) -> 'a -> 'b seq -> 'a)
    // Reduce the sequence into a single value

    let sum1 = Seq.fold (+) 0 <| seq { 1 .. 100 }
    //val sum1: int = 5050


module Queries =

    // query expressions.

    type Customer = { Name : string; Address : string; State : string; ZipCode : string }

    let allCustomers : Customer seq = seq { }

    /// Return the zip codes of all known customers in a given state.
    let customerZipCodesByState' stateName =
        allCustomers
        |> Seq.filter (fun customer -> customer.State = stateName)
        |> Seq.map (fun customer -> customer.ZipCode)
        |> Seq.distinct

    let customerZipCodeByState stateName =
        query {
            for customer in allCustomers do
            where (customer.State = stateName)
            select customer.ZipCode
            distinct
        }

