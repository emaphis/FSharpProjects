// Exercise 6.2
// update the drive function from the exercise we
// worked on in chapter 4 to use pipelines.

let drive distance  gas=
    if distance > 50 then  gas / 2.0
    elif distance > 25 then gas - 10.0
    elif distance > 0 then gas - 1.0
    else gas

let gas = 100.0

// Using intermediary variables
let gas1 = drive 60 gas
let gas2 = drive 30 gas1
let gas3 = drive 5 gas2

// Using the pipe operator
let endGas =
    gas
    |> drive 60
    |> drive 30
    |> drive 5
