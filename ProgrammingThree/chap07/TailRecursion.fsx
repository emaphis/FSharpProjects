
module TailRecursion

// Creating a List<_> of 100,000 integers
open System.Collections.Generic

let CreateMutableList1() =
    let l = List<int>()
    for i = 0 to 100_000 do
        l.Add(i)
    l


// Creating a list of 100,000 integer
let createImmutableList() =
    let rec createList i max =
        if i = max then
            []
        else
            i :: createList (i + 1) max

    createList 0 100_000


let list1 = CreateMutableList1()
printfn $"lenght = {list1.Count}"
// lenght = 100001

// Don't run - stack overflow.
//let list2 = createImmutableList()


module UnderStandingTheStack =

    let rec factorial x =
        if x <= 1
        then 1   // base case
        else x * factorial (x - 1)


    let num1 = factorial 10

module IntroducingTailRecursion =

    // Example 7-13. Tail-recursive version of factorial
    [<TailCall>]
    let factorial x =
        // Keep track of both and  an accumulator value (acc)
        let rec loop x acc =
            if x <= 1 then
                acc
            else
                loop (x - 1) (acc * x)

        loop x 1

    let num = factorial 30
    // val num: int = 1409286144


module TailRecursivePatterns  =

    // * Accumulator pattern *

    //  a naïve implementation of List.map
    let rec map' f list =
        match list with
        | []   -> []
        | hd :: tl -> (f hd) :: (map' f tl)


    // Example 7-14. The accumulator pattern

    let map fn list =
        let rec loop fn list acc =
            match list with
            | []       -> acc
            | hd :: tl -> loop fn tl (fn hd :: acc)
        loop fn (List.rev list) []

    let list1 = map (fun x -> x + 1)  [1; 2; 3; 4]
    //val list1: int list = [2; 3; 4; 5]


    // Binary recursion


    type BinTree<'a> =
        | Node of 'a * BinTree<'a> * BinTree<'a>
        | Empty


    // Example 7-15. Non-tail-recursive iter implementation
    let rec iter' fn binTree =
        match binTree with
        | Empty -> ()
        | Node(x, l, r) ->
            fn x
            iter' fn l   // NOT in tail position
            iter' fn r   // In tail position

    //val iter': fn: ('a -> unit) -> binTree: BinTree<'a> -> unit


    // Continuations
    // continuation passing style

    // Example 7-16. Building up a function value
    // Print a list in revere using a continuation
    let printListRev list =
        let rec loop list cont =
            match list with
            // For an empty list, execute the continuation
            | [] -> cont()
            // For other lists, add printing the current
            // node as part of the continuation.
            | hd :: tl ->
                loop tl (fun () -> printf $" %d{hd}"
                                   cont())

        loop list (fun () -> printfn "Done!")


    do printListRev [1 .. 10]
    // 10 9 8 7 6 5 4 3 2 1 Done!


    // Example 7-17 defines a custom iterator type ContinuationStep, which has two data
    // tags Finished and Step. Step is a tuple of a value and a function to produce the next
    // ContinuationStep.

    // Example 7-17. Building a custom iterator

    type ContinuationStep<'a> =
        | Finished
        | Step of 'a * (unit -> ContinuationStep<'a>)

    let iter fn binTree =

        let rec linearize binTree cont =
            match binTree with
            | Empty -> cont()
            | Node(x, l, r) ->
                Step(x (fun () -> linearize l (fun () -> linearize r cont)))

        let steps = linearize binTree (fun () -> Finished )

        let rec processSteps step =
            match step with
            | Finished -> ()
            | Step(x, getNext) ->
                fn x
                processSteps (getNext())

        processSteps steps

        // val iter':
        //  fn: ('a -> unit) -> binTree: BinTree<'a> -> unit

        // val iter:
        //  fn: ('a -> unit) ->
        //    binTree: BinTree<((unit -> ContinuationStep<'a>) ->
        //                        'a * (unit -> ContinuationStep<'a>))> -> unit