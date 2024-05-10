module ComputationExpressions.OptionDemo

let multiply x y =
    x * y

let divide x y =
    if y = 0 then None
    else Some (x / y)

let calculate x y =
    divide x y
    |> Option.map (fun v-> multiply v x)
    |> Option.bind (fun t -> divide t y)
