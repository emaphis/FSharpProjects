// Lesson 7 - parsing

open System
open System.IO

type Customer = {
    Name : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}

let createCustomer name email eligible registered dateRegistered discount = 
    { 
        Name = name
        Email = email
        IsEligible = eligible
        IsRegistered = registered
        DateRegistered = dateRegistered
        Discount = discount
    }


type ValidatedCustomer = {
    Name : string
    Email : string option
    IsEligible : bool
    IsRegistered : bool
    DateRegistered : DateTime option 
    Discount : decimal option
}


type DataReader = string -> Result<string seq,exn>

let readFile : DataReader =
    fun path ->
        try
            seq { 
                use reader = new StreamReader(File.OpenRead(path))
                while not reader.EndOfStream do
                    reader.ReadLine() 
            }
            |> Ok
        with
        | ex -> Error ex


let parseLine (line:string)  =
    match line.Split('|') with
    | [| name; email;  eligible;  registered;  dateRegistered;  discount |] ->
        createCustomer name  email  eligible  registered  dateRegistered  discount |> Ok
    | _ -> Error $"Invalid line {line}"

;;

let parse (data:string seq) =
    data
    |> Seq.skip 1
    |> Seq.map parseLine
    |> Seq.choose id

let output data =
    data 
    |> Seq.iter (fun x -> printfn "%A" x)

let import (dataReader:DataReader) path =
    match path |> dataReader with
    | Ok data -> data |> parse |> output
    | Error ex -> printfn "Error: %A" ex.Message

Path.Combine(__SOURCE_DIRECTORY__, "resources", "customers.csv")    
|> import readFile

