// Lesson 10 - Object Expressions
// Sorta like Anonymous Classes


type ILogger =
    abstract Info : string -> unit
    abstract Error : string -> unit


let logger = {
    new ILogger with
    member _.Info(msg) = printfn $"Info: {msg}"
    member _.Error(msg) = printfn $"Error: {msg}"
}


logger.Info "This is a warning"
logger.Error "This is a critical error"
