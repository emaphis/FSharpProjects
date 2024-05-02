// Lesson 5 - Modules and Namespaces

open MyProject

let customer1 = { Id = 2000; IsVip = false; Credit = 100m }

let result1 = Customer.getPurchases customer1

printfn "%A" result1
