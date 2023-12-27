
// Intro to Discriminated Unions
type Chicken =
    {
        Name : string
        Size : float
    }

type Turkey =
    {
        Name : string
        Size : float
    }

type Bird =
    | Chicken of Chicken
    | Turkey of Turkey


let c1 : Chicken = { Name = "Clucky"; Size = 10.0 }
let b1 = Bird.Chicken c1
let t1 : Turkey = { Name = "Gobble"; Size = 20.0 }
let b2 = Bird.Turkey t1



