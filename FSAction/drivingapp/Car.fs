//  Exercise 6.3

module Car
open System

let drive distance  gas=
    if distance > 50 then  gas / 2.0
    elif distance > 25 then gas - 10.0
    elif distance > 0 then gas - 1.0
    else gas


// Exercise 6.4

// 6.
type DriveResult = {
    GasRemaining : float
    OutOfGas : bool
}

// 6. 
let driveRec distance gas =
    let endGas = drive distance gas
    let out = endGas <= 0.0
    {  GasRemaining = endGas
       OutOfGas = out }
