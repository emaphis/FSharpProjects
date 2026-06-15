// Immutable Data Structures
// Discriminated Unions

// Creating Discriminated Unions

(*
type unionName =
    | Case1
    | Case2 of datatype
    | ...
*)

// Union Basics: an On/Off switch

type switchstate =
    | On
    | Off

let toggle = function
    | On  -> Off
    | Off -> On

let main1() =
    let x = On
    let y = Off
    let z = toggle y

    printfn $"x: %A{x}"
    printfn $"y: %A{y}"
    printfn $"z: %A{z}"

main1()


// Holding Data In Unions: a dimmer switch

//open System

type switchstate' =
    | On
    | Off
    | Adjustable of float


let toggle' = function
    | On  -> Off
    | Off -> On
    | Adjustable brightness ->
        // Matches any swtichstate' of type Adjustable.  Binds
        // the value passed into the constructor to the variable
        // 'brightness'. Toggles dimness around the halfway point.
        let pivot = 0.5
        if brightness <= pivot then
            Adjustable(brightness + pivot)
        else
            Adjustable(brightness - pivot)

let main2() =
    let x = On
    let y = Off
    let z = Adjustable 0.25

    printfn $"x: %A{x}"
    printfn $"y: %A{y}"
    printfn $"z: %A{z}"
    printfn $"toggle z: %A{toggle' z}"

main2 ()


// Creating Trees

//open System

type tree =
    | Leaf of int
    | Node of tree * tree

let simpleTree =
    Node(
        Leaf 1,
        Node(
            Leaf 2,
            Node(
                Node(
                    Leaf 4,
                    Leaf 5
                ),
                Leaf 3
            )
        )
    )

let rec countLeaves = function
    | Leaf _  -> 1
    | Node(tree1, tree2) ->
        countLeaves tree1 + countLeaves tree2


let main3() =
    printfn $"%A{simpleTree}"
    printfn $"countLeaves simpleTree: %i{countLeaves simpleTree}"

main3()


// Generalizing Unions For All Datatypes

type 'a tree' =
    | Leaf of 'a
    | Node of 'a tree' * 'a tree'


let firstTree =
    Node(
        Leaf 1,
        Node(
            Leaf 2,
            Node(
                Node(
                    Leaf 4,
                    Leaf 5
                ),
                Leaf 3
            )
        )
    )

let secondTree =
    Node(
        Node(
            Node(
                Leaf "Red",
                Leaf "Orange"
            ),
            Node(
                Leaf "Yellow",
                Leaf "Green"
            )
        ),
        Node(
            Leaf "Blue",
            Leaf "Violet"
        )
    )

let prettyPrint tree =
    let rec loop depth tree =
        let spacer = new string(' ', depth)
        match tree with
        | Leaf value ->
            printfn $"%s{spacer} |- %A{value}"
        | Node(tree1, tree2) ->
            printfn $"%s{spacer} |"
            loop (depth + 1) tree1
            loop (depth + 1) tree2
    loop 0 tree

let main4() =
    printfn "firstTree:"
    prettyPrint firstTree

    printfn "secondTree:"
    prettyPrint secondTree
   // Console.ReadKey(true) |> ignore

main4()


// Examples

// Built-in Union Types

type 'a list' =
    | Cons of 'a * 'a list
    | Nil

type 'a option' =
    | Some of 'a
    | None


// Propositional Logic

type proposition =
    | True
    | Not of proposition
    | And of proposition * proposition
    | Or of proposition * proposition

let rec eval = function
    | True -> true
    | Not prop -> not (eval prop)
    | And (prop1, prop2) -> eval prop1 && eval prop2
    | Or (prop1, prop2) -> eval prop1 || eval prop2


let prop1 =
    (* ~t || ~~t *)
    Or(
        Not True,
        Not (Not True)
    )

let prop2 =
    (* ~(t && ~t) || ( (t || t) || ~t) *)
    Or(
        Not(
            And(
                True,
                Not True
            )
        ),
        Or(
            Or(
                True,
                True
            ),
            Not True
        )
    )

let prop3 =
    (* ~~~~~~~t *)
    Not(Not(Not(Not(Not(Not(Not True))))))


let main5() =
    let testProp name prop = printfn $"%s{name}: %b{eval prop}"

    testProp "prop1" prop1
    testProp "prop2" prop2
    testProp "prop3" prop3

main5()
