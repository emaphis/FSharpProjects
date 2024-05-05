// Lesson 6 - CSV
//

#r "nuget: FSharp.Data"

open System
open System.IO
open FSharp.Data


type CustomerImport = {
    Id : string
    Email : string option
    IsEligible : bool
    IsRegistered : bool
    DateRegistered : bool
    Discount : decimal option
}

[<Literal>]
let ResolutionFolder = __SOURCE_DIRECTORY__


type Customers = FSharp.Data.CsvProvider<"customers.csv", "|", ResolutionFolder=ResolutionFolder, HasHeaders=true,
            Schema="string,string option,bool,bool,string option,decimal option">


let getCustomers file =
    Path.Combine(__SOURCE_DIRECTORY__, file)
    |> Customers.Load
    |> fun customers -> customers.Rows
    |> Seq.map (fun row  ->
            { 
                Id = row.CustomerId
                Email = row.Email
                IsEligible = row.Eligible
                IsRegistered = row.Eligible
                DateRegistered = row.Registered
                Discount = row.Discount
            }
        )

let data = getCustomers "customers.csv"

printfn "%A" data
