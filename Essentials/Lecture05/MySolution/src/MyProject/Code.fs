// Lesson 5 - Modules and Namespaces

namespace MyProject


type Customer = {
    Id : int
    IsVip : bool
    Credit : decimal
}

type GetPurchasesError =
    | HasOddId of int

type IncreaseCreditIfVIPError =
    | IsNotVIP

type UpgradeCusomeError =
    | GetPurchases of GetPurchasesError
    | IncreaseCredit of IncreaseCreditIfVIPError


module Customer =

    // Customer -> Result<(Customer * decimal), exn>
    let getPurchases customer =
        // Imagine this function is fetching data from a database
        if customer.Id % 2 = 0 then Ok (customer, 120m)
        else Error (HasOddId customer.Id)


    // Customer * decimal -> Customer
    let tryPromoteToVIP purchaes =
        let customer, amount = purchaes
        if amount > 100m then { customer with IsVip = true}
        else customer


    // Customer -> Result<Customer,exn>
    let increaseCreditIfVip customer =
        // Imagine this function could cause an exceptio
        if customer.IsVip then
            Ok { customer with Credit = customer.Credit + 100.0m }
        else Error IsNotVIP

    // original version - updated below
    let upgradeCustomer0 customer :  Result<Customer, UpgradeCusomeError> =
        customer
        |> getPurchases
        |> Result.mapError (fun err -> GetPurchases err)
        |> Result.map tryPromoteToVIP
        |> fun result ->
            match result with
            | Ok cust ->
                match increaseCreditIfVip cust with
                | Ok res -> Ok res
                | Error err -> Error (IncreaseCredit err)
            | Error err -> Error err

    // Result.bind    : (('a -> Result<'b,'c>) -> Result<'a,'c> -> Result<'b,'c>)
    // Result.map     : (('a -> 'b) -> Result<'a,'c> -> Result<'b,'c>)
    // Result.mapError: (('a -> 'b) -> Result<'c,'a> -> Result<'c,'b>)

    let upgradeCustomer1 customer :  Result<Customer, UpgradeCusomeError> =
        customer
        |> getPurchases
        |> Result.mapError (fun err -> GetPurchases err)
        |> Result.map tryPromoteToVIP
        |> Result.bind (fun cust ->
                match increaseCreditIfVip cust with
                | Ok res -> Ok res
                | Error err -> err |> IncreaseCredit |> Error
                )

    // final version
    let upgradeCustomer customer :  Result<Customer, UpgradeCusomeError> =
        customer
        |> getPurchases
        |> Result.mapError (fun err -> GetPurchases err)
        |> Result.map tryPromoteToVIP
        |> Result.bind (fun cust ->
            cust
            |> increaseCreditIfVip
            |> Result.mapError IncreaseCredit
            )

    // def of maperror
    let mapE (fn: 'errA -> 'errB) (result: Result<'cst, 'errA>) : Result<'cst, 'errB> =
        match result with
        | Ok res -> Ok res
        | Error ex -> ex |> fn |> Error
