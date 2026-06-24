// Object-Oriented Programming
// Modules and Namespaces

// Extending Types and Modules.

// F# supports extension methods, which allow programmers to add new static
// and instance methods to classes and modules without inheriting from them.

module Seq =
    let foralli f s =
        s
        |> Seq.mapi (fun i x -> i, x)
        |> Seq.forall (fun (i, x) -> f i x)



let isPalindrome (input : string) =
    input
    |> Seq.take (input.Length / 2)
    |> Seq.foralli (fun i x -> x = input.[input.Length - i - 1]);;


let bool1 = isPalindrome "hello"
let bool2 = isPalindrome "racecar"


// Extending a Type
module StringExtensions =
    type System.String with
        member this.IsPalindrome =
            this
            |> Seq.take (this.Length / 2)
            |> Seq.foralli (fun i x -> this.[this.Length - i - 1] = x)

        static member Reverse(s : string) =
            let chars : char array =
                let temp = Array.zeroCreate s.Length
                let charsToTake = if temp.Length % 2 <> 0 then (temp.Length + 1) / 2 else temp.Length / 2
                s
                |> Seq.take charsToTake
                |> Seq.iteri (fun i x ->
                    temp.[i] <- s.[temp.Length - i - 1]
                    temp.[temp.Length - i - 1] <- x)
                temp
            new System.String(chars)

open StringExtensions

let bool3 = "hello world".IsPalindrome
let str1 = System.String.Reverse("hello world")

// Module Signatures
// See modules/modules/DataStructures.fsi
