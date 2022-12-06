// 10 - Object Programming

// Object Expressions

open System

type ILogger =
    abstract member Info : string -> unit
    abstract member Error : string -> unit


type Logger() =
    interface ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
        
// object expression simplifies usage
let logger = {
    new ILogger with
        member _.Info(msg) = printfn "Info: %s" msg
        member _.Error(msg) = printfn "Error: %s" msg
}

// create a class type and pass the ILogger in as a constructor argument

type MyClass(logger: ILogger) =
    let mutable count = 0

    member _.DoSomething input =
        logger.Info $"Processing {input} at {DateTime.UtcNow.ToString()}"
        count <- count + 1
        ()

    member _.Count = count


let myClass = MyClass(logger)
[1..10] |> List.iter myClass.DoSomething
printfn "%i" myClass.Count

// function that takes in an ILogger as a parameter
let doSomethingElse (logger: ILogger) input =
    logger.Info $"Processing {input} at {DateTime.UtcNow.ToString()}"
    ()


doSomethingElse logger "MyData"
