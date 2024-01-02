// izntro to Arrays

let a = [| 1; 2; 3 |]
let e = a[0]
let b =
    [|
        1
        2
        3
    |]
let c = [| 1 .. 3 |]
let d = [| 1 .. 2 .. 110 |]
let f =
    [| for i in 1 .. 10 do
            i * i |]

let g : array<int>  = Array.zeroCreate 5
g[2] <- 10
printfn "%A" g

let myValues = [| 1 .. 10 |]

let mySolution =
    myValues
    |> Array.filter (fun v -> v % 2 = 0)
    |> Array.map (fun v -> v * 2)
    |> Array.sortDescending

Array.sortInPlace mySolution    
printfn "%A" mySolution
