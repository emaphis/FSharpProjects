module ComputationExpressions.OptionDemoCU


let multiply x y =
    x * y

let divide x y =
    if y = 0 then None
    else Some (x / y)


let calculate x y =

    option {
        let! r1 = divide x y
        let r2 = multiply r1 x
        return! divide r2 y
    }
