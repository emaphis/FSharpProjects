// Intro to Modules

module Farm

type Chicken =
    {
        Name: string
        Size: float
    }

let myFunction (a: float) (b: float) = a + b

[<RequireQualifiedAccess>]
module Chicken =

    let grow (c: Chicken) =
        { c with Size = myFunction c.Size 10.0 }

type Cow =
    {
        Name: string
        Age: int
    }

module Cow =

        let grow (c: Cow) =
            { c with Age = c.Age + 1 }

let aChicken = { Name = "Clucky"; Size = 1.0 }
let bChicken = Chicken.grow aChicken
