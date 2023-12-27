// Intro to Tuples

type Chicken =
    {
        Name : string
        Size : float
    }

let myChicken = { Name = "Clucky"; Size = 10.0 }
let myOtherChicken = { Size = 2.0; Name = "Bucky" }

let myTuple = "Clukcy", 10.0
let myTripleTuple = "Clucky", 10.0, 1
let myQuadTuple = 1, 1.0, "Marcus", 1M

let x = fst myTuple
let y = snd myTuple

// deconstruction
let name, size = myTuple
let a, b, c = myTripleTuple

// Struct Tuple
let myStructTuple = struct ("Clucky", 10.0)
let struct (n, f) = myStructTuple
