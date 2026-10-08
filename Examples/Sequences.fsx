(* F# - Sequences *)

(* Creating Sequences and Sequences Expressions
By specifying the range.
By specifying the range with increment or decrement.
By using the yield keyword to produce values that become part of the sequence.
By using the → operator.
*)

(* Example 1 - Range *)

(* ascending order *)
let seq1 = seq { 1 .. 10 }
printfn $"The Sequence: %A{seq1}"

(* ascending order and increment *)
let seq2 = seq { 1 .. 5 .. 50  }
printfn $"The Sequence: %A{seq2}"

(* descending order and decrement*)
let seq3 = seq { 50 .. -5 .. 0 }
printfn $"The Sequence: %A{seq3}"

(* using yield *)
let seq4 = seq { for a in 1 .. 10 do yield a, a*a, a*a*a }
printfn $"The Sequence: %A{seq4}"


(* Example 2 - Recursive isPrime *)

/// Calculate primes
let isPrime n =
  let rec check i =
    i > n/2 || n % i <> 0 && check (i + 1)
  check 2

let primeIn50 = seq { for n in 1..50 do if isPrime n then yield n }

for x in primeIn50 do
  printfn $"%d{x}"


(* Basic Operations on Sequence *)

(* Example 1 *)

(* Creating sequences *)
printfn"The singleton sequence:"
let emptySeq = Seq.empty
let seq5 = Seq.singleton 20
printfn $"Singleton seq: %A{seq5} "

printfn"The init sequence:"
let seq6 = Seq.init 5 (fun n -> n * 3)
Seq.iter (fun i -> printf $"%d{i} ") seq6
printfn ""

(* converting an array to sequence by using cast *)
printfn"The array sequence 7:"
let seq7 = [| 1 .. 10 |] :> seq<int>
Seq.iter (fun i -> printf $"%d{i} ") seq7
printfn ""

(* converting an array to sequence by using Seq.ofArray *)
printfn"The array sequence 8:"
let seq8 = [| 2 .. 2 .. 20 |] |> Seq.ofArray
Seq.iter (fun i -> printf $"%d{i} ") seq8
printfn ""


(* Example 2 - Seq.unfold *)
let seq9 =
    Seq.unfold (fun state ->
        if state > 20 then None
        else Some(state, state + 1)) 0

printfn "The sequence seq1 contains numbers from 0 to 20."
for x in seq9 do printf $"%d{x} "
printfn ""


(* Example 3 - Truncate, take *)
let mySeq = seq { for i in 1 .. 10 -> 3 * i }
let truncatedSeq = Seq.truncate 5 mySeq
let takeSeq = Seq.take 5 mySeq

printfn"The original sequence"
Seq.iter (fun i -> printf $"%d{i} ") mySeq
printfn ""

printfn"The truncated  sequence"
Seq.iter (fun i -> printf $"%d{i} ") truncatedSeq
printfn ""

printfn"The take sequence"
Seq.iter (fun i -> printf $"%d{i} ") takeSeq
printfn""
