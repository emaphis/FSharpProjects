
open ComputationExpressions.OptionDemoCU

calculate 8 0 |> printfn "caculate 8 0 = %A"  // None
calculate 8 2 |> printfn "caculate 8 4 = %A"  // Some 16
