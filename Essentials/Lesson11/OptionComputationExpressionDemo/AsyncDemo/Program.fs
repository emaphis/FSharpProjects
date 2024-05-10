
open System.IO
open ComputationExpression.AsyncDemo


Path.Combine(__SOURCE_DIRECTORY__, "customers.csv")
|> getFileInformation
|> Async.RunSynchronously
|> printfn "%A"
