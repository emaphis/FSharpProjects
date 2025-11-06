module DiscriminatedUnions

// Discriminate union for a card suit

/// Card suits
type Suit =
    | Heart
    | Diamond
    | Spade
    | Club

let suits = [ Heart; Diamond; Spade; Club ]

/// Discriminated union for playing cards
type PlayingCard =
    | Ace   of Suit
    | King  of Suit
    | Queen of Suit
    | Jack  of Suit
    | ValueCard of int * Suit

// Use a list comprehension to generate a deck of cards

let deckOfCards =
    [
        for suit in suits do
            yield Ace(suit)
            yield King(suit)
            yield Queen(suit)
            yield Jack(suit)
            for value in 2 .. 10 do
                yield ValueCard(value, suit)
    ]


// Recursive discriminated unions

/// Program statements
type Statement =
    | Print     of string
    | Sequence  of Statement * Statement
    | IfStmnt   of Expression * Statement * Statement

/// Program expressions
and Expression =
    | Integer     of int
    | LessThan    of Expression * Expression
    | GreaterThan of Expression * Expression


// if (3 > 1)
//        print "3 is greater than 1"
//    else
//        print "3 is not"
//        print "greater than 1
let program =
    IfStmnt(
        GreaterThan(Integer(3), Integer(1)),
        Print("3 is greater than 1"),
        Sequence(Print("3 is not"), Print("greater than 1"))
    )


module TreeStructures =

    //  Example 3-7 defines a binary tree and a function for traversing it in only 11 lines of 
    //  code.

    /// Example 3-7. Binary tree using discriminated unions
    type BinaryTree =
        | Node of int * BinaryTree * BinaryTree
        | Empty

    let rec printInOrder tree =
        match tree with
        | Node (data, left, right) ->
            printInOrder left
            printfn $"Node {data}"
            printInOrder right
        | Empty ->
            ()

    (*
          2
        /   \
       1     4
           /   \
          3     5
    *)

    let binTree =
        Node(2,
             Node(1, Empty, Empty),
             Node(4,
                  Node(3, Empty, Empty),
                  Node(5, Empty, Empty)
            )
        )

    do printInOrder binTree


module PatternMatching =

    /// Describe a pair of cards in a game of poker.
    let describeHoleCards cards =
        match cards with
        | []
        | [_] ->
            failwith "Too few cards"
        | cards when List.length cards >= 2 ->
            failwith "Too many cards"
        | [ Ace _; Ace _ ] -> "Pocket Rockets"
        | [ King _; King _ ] -> "Cowboys"
        | [ ValueCard(2, _); ValueCard(2, _) ] -> "Ducks"
        | [ Queen _; Queen _ ]
        | [ Jack _; Jack _ ] -> "Pair of face cards"
        | [ ValueCard(x, _); ValueCard(y, _) ] when x = y ->
            "A Pair"
        | [ first; second ] -> $"Two cards: {first} and {second}"
        | _ -> "A hand"



    // Recursive discriminated functions in pattern matches.

    type Employee =
        | Manager of string * Employee list
        | Worker  of string

    let rec printOrganization worker =
        match worker with
        | Worker name -> printfn $"Employee {name}"

        // Manager with a worker list with one element
        | Manager (managerName, [ Worker employeeName ]) ->
            printfn $"Manager {managerName} with Worker {employeeName}"

        // Manager with a worker list of two elements
        | Manager (managerName, [ Worker employee1; Worker employee2 ]) ->
            printfn $"Manager {managerName} with two workers {employee1} and {employee2}"

        // Manager with a list of workers
        | Manager (managerName, workers) ->
            printfn $"Manager {managerName} with workers..."
            workers |> List.iter printOrganization

    let company0 = Manager ("Tom", [ Worker "Pam"  ])
    printOrganization company0

    let company1 = Manager ("Tom", [ Worker "Pam"; Worker "Stuart" ])
    printOrganization company1
    
    let company2 = Manager ("Tom", [ Worker "Pam"; Worker "Stuart"; Worker "Charley" ])
    printOrganization company2


module MethodsProperties =

    /// Discriminated union for playing cards
    type PlayingCard =
        | Ace   of Suit
        | King  of Suit
        | Queen of Suit
        | Jack  of Suit
        | ValueCard of int * Suit

        member this.Value =
            match this with
            | Ace _ -> 11
            | King _ | Queen _ | Jack _ -> 10
            | ValueCard (x, _) when x <= 10 && x >= 2 -> x
            | ValueCard _ -> failwith "Card has an invalid value!"

    let highCard = Ace Spade
    let value = highCard.Value
    //val value: int = 11
