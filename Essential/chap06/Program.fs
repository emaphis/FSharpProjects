
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

/// Load data from cvs file
let readFile : DataReader =
    fun path ->
        try 
            File.ReadLines(path)
            |> Ok
        with
        | ex -> Error ex

/// Parse a line of delimited text representing a Customer then return a Customer
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

// parses the input data, and returns a seq<Customer>
let parse (data: string seq) =
    data
    |> Seq.skip 1
    |> Seq.choose parseLine

/// parses the input data, and returns a seq<Customer>
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
