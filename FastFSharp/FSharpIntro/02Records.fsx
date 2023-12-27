// Records

type Turkey =
    {
        Name : string
        Size : float
    }

let t1 = { Name = "Tom"; Size = 50.0 }


type Chicken =
    {
        Name : string
        Size : float
    }

    static member Create (name, size) =
        if size <= 0.0 then
            invalidArg (nameof size) "Cannot have a zero sized chicken!"
        {
            Name = name
            Size = size
        }

    static member Create name =
        {
            Name = name
            Size = 10.0;
        }

module Chicken =

    let create name size =
        {
            Name = name
            Size = size
        }



let c1 = Chicken.create "Clucky" 20.0

let c2 = Chicken.Create("Bucky", 30.0)

let c3 = Chicken.Create "Plucky"

//let c4 = Chicken.Create("Nully", -10.0)

