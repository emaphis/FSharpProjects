module FunctionalPatterns

module Memoization =

    // Example 7-19. Memoizing pure functions
    open System.Collections.Generic

    let memoize (f: 'a -> 'b) =
        let dict = Dictionary<'a, 'b>()

        let memoizedFunc (input: 'a) =
            match dict.TryGetValue(input) with
            | true, x  -> x
            | false, _ ->
                // Evaluate and add to lookup dictionary
                let answer = f input
                dict.Add(input, answer)
                answer

        // Return our memoized version of f dict in closure
        memoizedFunc


    // Memoizing a simple function
    let rec fn1 x =
        match x with
        | 1 -> 1
        | n -> n * fn1 (n - 1)

    let fn2 x = memoize fn1 x


    let num1 = fn2 6


    // Memoizing a recursive function
    #nowarn 40
    let rec memoFib =
        let body x =
            match x with
            | 0 | 1 -> 1
            | 2 -> 2
            | n -> memoFib (n - 1) + memoFib (n - 2)
        memoize body


    let num2 = memoFib 45
    //val num2: int = 1836311903



module MutableFunctionValues =

    // Example 7-21. Using mutable function values
    // shows how you can change out the implementation of the generateWidget
    // function on the fly


    // Code to produce Widgets, the backbone of all .NET applications
    type Widget =
        | Sprocket of string * int
        | Cog of string * float

    let mutable generateWidget =
        let count = ref 0
        (fun () -> count.Value <- count.Value + 1
                   Sprocket("Model A1", count.Value))


    // Produce a Widget
    generateWidget()
    //val it: Widget = Sprocket ("Model A1", 1)

    // ... and another
    generateWidget()
    //val it: Widget = Sprocket ("Model A1", 2)

    // Now  update the function ...
    generateWidget <-
        let count = ref 0
        (fun () -> count.Value <- count.Value + 1
                   Cog( (sprintf $"Version 0x%x{count.Value}"), 0.0))


    // Produce another Widget - with the updated function
    generateWidget()
    //val it: Widget = Cog ("Version 0x1", 0.0)

    // ... and another
    generateWidget()
    //val it: Widget = Cog ("Version 0x2", 0.0)



module LazyProgramming =

    // * Reduced memory usage *

    // Example 7-22. Lazy type
    // defines a binary tree where the nodes are evaluated lazily
    type LazyBinTree<'a> =
        | Node of 'a * LazyBinTree<'a> Lazy * LazyBinTree<'a> Lazy
        | Empty

    let rec map fn tree =
        match tree with
        | Empty  -> Empty
        | Node(x, l, r) ->
            Node(fn x,
                 Lazy(let lfNode = l.Value
                      map fn lfNode),
                 Lazy(let rtNode = r.Value
                      map fn rtNode)
            )

        // * Abstracting data access *
