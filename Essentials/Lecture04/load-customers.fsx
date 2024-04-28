// Lesson 4 -  Load `customers.csv` file

open System.IO
open System.Collections.Generic

type Customer = {
    CustomerId : string
    Email : string
    IsElegible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}


let getData path =
    path
    |> File.ReadLines
    |> Seq.skip 1
    |> Seq.map (fun line ->
        match line.Split('|') with
        | [| customerid; email; eligible; registered; dateregistered; discount |] ->
            { CustomerId = customerid
              Email = email
              IsElegible = eligible 
              IsRegistered = registered
              DateRegistered = dateregistered
              Discount = discount }
        | _ -> failwith $"invalid customer: {line}"
       )
    |> Seq.toList

let data =
    Path.Combine(__SOURCE_DIRECTORY__, "customers.csv")
    |> getData

data
