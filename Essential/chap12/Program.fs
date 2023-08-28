// 12 - Computation Expressions

open System.IO
open ComputationExpression.OptionDemo
open ComputationExpression.AsyncDemo
open ComputationExpression.AsyncResultDemoTests

let proc1() =
    calculate 8 0 |> printfn "calculate 8 0 = %A"
    calculate 8 2 |> printfn "calculate 8 2 = %A"

let proc2() =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> getFileInformation
    |> Async.RunSynchronously
    |> printfn "%A"

let proc3() =
    printfn "Success: %b" success
    printfn "BadPassword: %b" badPassword
    printfn "InvalidUser: %b" invalidUser
    printfn "IsSuspended: %b" isSuspended
    printfn "IsBanned: %b" isBanned
    printfn "HasBadLuck: %b" hasBadLuck


[<EntryPoint>]
let main argv =
    proc1()
    printfn "------------"
    proc2()
    printfn "------------"
    proc3()
    0
