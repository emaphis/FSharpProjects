namespace AwesomeCollections

module Main =
    [<EntryPoint>]
    let main args =
        let stack = StackNode(1, StackNode(2, StackNode(3, StackNode(4, StackNode(5, EmptyStack)))))
        printfn "Arguments passed to function : %A" args
        // Return 0. This indicates success.
        0

