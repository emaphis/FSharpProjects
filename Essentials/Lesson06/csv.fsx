// Lesson 6 - CSV
// was load-customers.fsx from Lesson 4.

open System
open System.IO

type Customer = {
    Id : string
    Email : string
    IsEligible : string
    IsRegistered : string
    DateRegistered : string
    Discount : string
}

let customers =
    Path.Combine(__SOURCE_DIRECTORY__, "customers.csv")    
    |> File.ReadAllLines
    |> Seq.skip 1
    |> Seq.map (fun row  ->
        match row.Split('|') with
        | [| id; email; eligible; registered; dateRegistered; discount |] -> 
            Some { 
                Id = id
                Email = email
                IsEligible = eligible
                IsRegistered = registered
                DateRegistered = dateRegistered
                Discount = discount
            }
        | _ -> None
        )

