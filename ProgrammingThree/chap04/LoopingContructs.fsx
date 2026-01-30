module LoopingConstructs

    // Traditional looping constructs using mutable variables

module WhileLoops =

    // Loop until a Boolean expression evaluates to `false`

    // While loops
    let mutable i = 0
    while i < 5 do
        i <- i + 1
        printfn "i = %d" i ;;

    // i = 1
    // i = 2
    // i = 3
    // i = 4
    // i = 5
    // val mutable i: int = 5


module ForLoops =

    // Simple for loops

    // Loop a fixed number of times

    // For loops
    for i = 1 to 5 do
        printfn "i = %d" i

    // i = 1
    // i = 2
    // i = 3
    // i = 4
    // i = 5
    // val it: unit = ()


    // Counting down
    for i = 5 downto 1 do
        printfn "i = %d" i

    // i = 5
    // i = 4
    // i = 3
    // i = 2
    // i = 1
    // val it: unit = ()

    // Enumerable for loop

    // Loop through sequences
    for i in [ 1 .. 5 ] do
        printfn "i = %d" i

    // i = 1
    // i = 2
    // i = 3
    // i = 4
    // i = 5
    // val it: unit = ()


    // Example 4-17. For loops with pattern matching

    // Pet type
    type Pet =
        | Cat of string * int // Name, Lives
        | Dog of string       // Name

    let famousPets = [ Dog("Lassie"); Cat("Felix", 9); Dog("Rin Tin Tin") ]

    // Print famous dogs, (waring due to incomplete match )
    for Dog(name) in famousPets do
        printfn "%s was a famous dog." name

    // Lassie was a famous dog.
    // Rin Tin Tin was a famous dog.
    // val it: unit = ()
