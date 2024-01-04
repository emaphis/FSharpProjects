// Intro to Loops

let values =
    [| 1..10 |]

for v  in values do
    printfn $"a = {v}"

for i in 0 .. values.Length - 1 do
    printfn $"b = {values[i]}"

for i in 0 .. 2 .. values.Length - 1 do
    printfn $"c = {values[i]}"

for i in values.Length - 1 .. -2 .. 0 do
    printfn $"d = {values[i]}"

for i in 0 .. 2 .. values.Length - 1 do
    printfn $"e = {values[i]}"

for i in values.Length - 1 .. 2 .. 0 do
    printfn $"f = {values[i]}"

for i = 0 to values.Length - 1 do
    printfn $"g = {values[i]}"

for i = values.Length - 1 downto 0 do
    printfn $"h = {values[i]}"

let mutable i = 0

while i <= values.Length - 1 do
    printfn $"i = {values[i]}"
    i <- i + 1

values
|> Array.iter (fun v ->
    printfn $"j = {v}")
