// Lesson 6 - Reading file data

open System.IO


// Data definitions

type Customer = {
    CustomerId : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}


// Reading Data

type DataReader = string -> Result<string seq,exn>


// string -> Result<seq<string>>,exn>
let readFile : DataReader  =
    fun path ->
        try
            File.ReadLines(path)
            |> Ok
        with
        | ex -> Error ex


// Parsing Data

let parseLine (line:string) : Customer option =
    match line.Split('|') with
    | [| customerId; email; eligible; registered; dateRegistered; discount |] ->
        Some {
            CustomerId = customerId
            Email = email
            IsEligible = eligible
            IsRegistered = registered
            DateRegistered = dateRegistered
            Discount = discount
        }
    | _ -> None


let parse (data: string seq) =
    data
    |> Seq.skip 1
    |> Seq.choose parseLine


let output data =
    data
    |> Seq.iter (fun x -> printfn "%A" x)


let import (dataReader:DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error ex -> printfn "Error: %A" ex.Message


let importWithFileReader = import readFile


[<EntryPoint>]
let main argv =
    Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")
    |> importWithFileReader
    0
