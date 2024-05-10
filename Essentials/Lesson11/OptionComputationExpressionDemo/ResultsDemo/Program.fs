open ComputationExpression.ResultDemo

let cust1 = { Id = 1; IsVip = false; Credit = 0m }
let cust2 = { Id = 2; IsVip = false; Credit = 200m }

cust1
|> upgradeCustomerCU
|> printfn "Cust1: %A"

cust2
|> upgradeCustomerCU
|> printfn "Cust2: %A"
