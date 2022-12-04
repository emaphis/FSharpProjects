
open System.IO


type Customer = {
    CustomerId : string
    Email : string
    IsElegible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}

type DataReader = string -> Result<string seq,exn>


let readFile : DataReader =
    fun path ->
        try 
            File.ReadLines(path)
            |> Ok
        with
        | ex -> Error ex


let parseLine (line: string) : Customer option =
    match line.Split('|') with
    | [| customerId; email; eligible; registered; dateRegistered; discount |] ->
        Some {
            CustomerId = customerId
            Email = email
            IsElegible = eligible
            IsRegistered = registered
            DateRegistered = dateRegistered
            Discount = discount
        }
    | _ -> None


let parse (data: string seq) =
    data
    |> Seq.skip 1  // ignore the header row
    |> Seq.map parseLine
    |> Seq.choose id   // ignore Nane and unwratpe Some


let output data =
    data
    |> Seq.iter (fun x -> printfn "%A" x)


let import (dataReader: DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error ex -> printfn "Error: %A" ex.Message


let importWithFileReader = import readFile


[<EntryPoint>]
let main argv =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> importWithFileReader
    0
