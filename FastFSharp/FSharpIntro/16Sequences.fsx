// Intor to Sequences

let a = seq {
    1
    2
    3
}

let b = seq { 1; 2; 3 }

let c = seq {
    for i in 1 ..10 do
        i
}
c

for i in c do
    printfn $"{i}"

let d = seq {
    for i in 1..2 do
    for j in 1..3 do
        i, j
}

let foo =
    [ 1 .. 5 ]
    |> List.map (fun x -> x * x)

    