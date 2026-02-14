// Interfaces

module Interfaces

    // Models a "can do" relationship.

module CanDo =

    // Example 6-1. Interfaces in F
    type Tastiness =
        | Delicious
        | SoSo
        | TrySomethingElse

    type IConsumable =
        abstract Eat : unit -> unit
        abstract Tastiness : Tastiness

    // Protip: Eat one of these a day
    type Apple() =
        interface IConsumable with
            member this.Eat (): unit =
                printfn "Tasty"
            member this.Tastiness = Delicious

    // Not that tasty, but if you are really hungary will do in a bind
    type CardboardBos() =
        interface IConsumable with
            member this.Eat(): unit =
                printfn "Yuck!"
            member this.Tastiness: Tastiness =
                TrySomethingElse



module UsingInterfaces =

    open CanDo

    let apple = Apple()

    //let tst1 = apple.Tastiness
    //he type 'Apple' does not define the field, constructor or member 'Tastiness'.

    // Need to create an interface to access the object
    let iconsumable = apple :> IConsumable
    let tst2 = iconsumable.Tastiness
    // val tst2: Tastiness = Delicious



module DefiningInterfaces =

    // Define a type inferred to be an interface
    type IDoStuff' =
        abstract DoThings : unit -> unit

    // Define an interface explicitly
    type IDoStuffToo' =
        interface
            abstract member DoThings : unit -> unit
        end



    // Example 6-2. Implementing a derived interface

    // Inherited interfaces
    type IDoStuff =
        abstract DoStuff : unit -> unit

    type IDoMoreStuff =
        inherit IDoStuff

        abstract DoMoreStuff : unit -> unit

        // ...


    // This works
    type Bar() =
        interface IDoStuff with
            override this.DoStuff() = printfn "Stuff getting done..."

        interface IDoMoreStuff with
            override this.DoMoreStuff() = printfn "More stuff getting done...";;
