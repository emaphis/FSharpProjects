// The active pattern

let (|Even|Odd|) x =
    if x % 2 <> 0 then Odd
    else Even

let choiceA = (|Even|Odd|) 5
// val choiceA: Choice<unit,unit> = Choice2Of2 ()

let describe num =
    match num with
    | Even -> "Even"
    | Odd -> "Odd"

let out1 = describe 5
let out2 = describe 6
