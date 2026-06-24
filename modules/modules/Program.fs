// Object-Oriented Programming
// Modules and Namespaces

// Defining Modules
module Main

//open DataStructures

open Princess.Collections


let x = Stack.getRange 1 10

let y =
    let rnd = System.Random()
    [ for _ in 1 .. 10 -> rnd.Next(0, 100) ]
    |> Seq.fold (fun acc x -> Tree.insert x acc) EmptyTree

printfn $"%A{Stack.hd x}"
printfn $"%A{Stack.tl x}"
printfn $"%A{Stack.fold ( * ) 1 x}"
printfn $"%A{Stack.reduce ( + ) x}"
printfn $"%A{y}"
