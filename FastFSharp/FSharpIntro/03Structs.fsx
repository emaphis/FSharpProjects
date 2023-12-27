// Intro to Structs
// Intro to Structs
// Allocated on the Stack instead of the heap

[<Struct>]
type Chicken =
    {
        Name : string
        Size : float
    }


let c1 = { Name = "Clucky"; Size = 10.0 }
let c2 = { c1 with Size = 20.0 }


[<Struct>]
type Turkey =
    val Name : string
    val Size : float
    new (name, size) =
        { Name = name; Size = size }
    new name =
        { Name = name; Size = 10.0 }

let t0 = Turkey()
let t1 = Turkey("Gobbler", 30.0 ) 
let t2 = Turkey "Giblette"


[<Struct>]
type Goose (name: string, size: float) =
    member _.Name = name
    member _.Size = size
    new (name: string) =
        Goose (name, 10.0)
 
let g0 = Goose () 


type Duck =
    struct
        val Name : string
        val Size : float

        new (name, size) =
            { Name = name; Size = size }
    end

let d0 = Duck ()
let d1 = Duck ("Donald", 10.0)
