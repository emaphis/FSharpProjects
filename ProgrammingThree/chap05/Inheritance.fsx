// Chapter 5 - Inheritance

module Inheritance

    // Provides type based polymorphism - the heart of Object Oriented programming

module Example1 =

    type BLTSandwich() =
        member this.Ingredients = [ "Bacon"; "Lettuce"; "Tomato" ]
        member this.Calories = 450
        override this.ToString() = "BLT"

    type TurkeySwissSandwich() =
        member this.Ingredients = [ "Turkey"; "Swiss" ]
        member this.Calories = 330
        override this.ToString() = "Turkey and Swiss"

    // Because both sandwiches are unrelated types to use sandwiches we need to
    // overload functions that use sandwiches

    type LunchTime() =
        member this.EatLunch(sandwich: BLTSandwich) =
            printfn $"I ate a {sandwich}"
        member this.EatLunch(sandwich: TurkeySwissSandwich) =
            printfn $"I ate a {sandwich}"
        //member this.EatLunch(sandwich: ClubSandwich) = ()

    let lunch = LunchTime()
    let blt = BLTSandwich()
    let tkswiss = TurkeySwissSandwich()

    do lunch.EatLunch(blt)
    do lunch.EatLunch(tkswiss)


module Example2 =

    // Base class
    type BaseClass =
        val m_field1 : int

        new(x) = { m_field1 = x }
        member  this.Field1 = this.m_field1

    // Derived class using implicit class construction
    type ImplicitDerived(field1, field2) =
        inherit BaseClass(field1)

        let m_field2: int = field2

        member this.Field2 = m_field2

    // Derived class using explicit class construction.
    type ExplicitDerived =
        inherit BaseClass

        val m_field2 : int

        new (field1, field2) = {
            inherit BaseClass(field1)
            m_field2 = field2
        }

        member this.Field2 = this.m_field2

    type BLTSandwich() =
        member this.Ingredients = [ "Bacon"; "Lettuce"; "Tomato" ]
        member this.Calories = 450
        override this.ToString() = "BLT"

    type TurkeySwissSandwich() =
        member this.Ingredients = [ "Turkey"; "Swiss" ]
        member this.Calories = 330
        override this.ToString() = "Turkey ans Swiss"

    // Because both sandwiches are unrelated types to use sandwiches we need to
    // overload functions that use sandwiches

    type LunchTime() =
        member this.EatLunch(sandwich: BLTSandwich) =
            printfn $"I ate a {sandwich}"
        member this.EatLunch(sandwich: TurkeySwissSandwich) =
            printfn $"I ate a {sandwich}"
        //member this.EatLunch(sandwich: ClubSandwich) = ()

    let lunch = LunchTime()
    let blt = BLTSandwich()
    let tkswiss = TurkeySwissSandwich()

    let printFields(field1: int, field2: int) =
        printfn $"fld1 = {field1}, fld2 = {field2}"

    let bc1 = BaseClass(1)
    let id1 = ImplicitDerived(3, 4)
    let ed1 = ExplicitDerived(5, 6)

    printFields(bc1.Field1, 999)
    printFields(id1.Field1, id1.Field2)
    printFields(ed1.Field1, ed1.Field2)


module MethodOverriding =

    // Example 5-16. Method overriding in F#

    type Sandwich() =
        abstract Ingredients : string list
        default this.Ingredients = []

        abstract Calories: int
        default this.Calories = 0

    type BLTSandwich() =
        inherit Sandwich()

        override this.Ingredients = [ "Bacon"; "Lettuce"; "Tomato" ]
        override this.Calories = 450
        override this.ToString() = "BLT"

    type TurkeySwissSandwich() =
        inherit Sandwich()

        override this.Ingredients = [ "Turkey"; "Swiss" ]
        override this.Calories = 330
        override this.ToString() = "Turkey and Swiss"

    // Because both sandwiches are unrelated types to use sandwiches we need to
    // overload functions that use sandwiches

    type LunchTime() =
        static member EatLunch(sandwich: Sandwich) =
            printfn $"I ate a {sandwich}"

    let blt = BLTSandwich()
    let tkswiss = TurkeySwissSandwich()

    LunchTime.EatLunch(blt)
    LunchTime.EatLunch(tkswiss)

    // Example 5-17. Accessing the base class

    // BLT with pickles
    type BLTWithPickleSandwich() =
        inherit BLTSandwich()

        override this.Ingredients = "Pickles" :: base.Ingredients
        override this.Calories = 50 + base.Calories
        override this.ToString() = "BLT with pickles"

    let bltWP = BLTWithPickleSandwich()

    LunchTime.EatLunch(bltWP)
    bltWP.Ingredients


module CategoriesOfClasses =

    // Abstract classes  - base class that has no implementation

    // ERROR: Define a class without providing an implementation to its members
    //type Foo1() =
    //   member this.Alpha = true
    //   abstract member Bravo : unit -> bool
    //  error FS0365: No implementation was given for 'abstract Foo.Bravo: unit -> bool'

    // Properly define an abstract class
    [<AbstractClass>]
    type Foo1() =
       member this.Alpha = true
       abstract member Bravo : unit ->  bool


    // Define a sealed class
    //[<Sealed>]
    //type Foo2() =
    //    member this.Alpha() = true

    // ERROR: Inherit from a sealed class
    //type Bar2() =
    //    inherit Foo2()
    //    member this.Bravo() = false
    // error FS0945: Cannot inherit a sealed type


module Casting =

    // Static upcast

    // Example 5-18. Static upcasts

    // Define a class hierarchy
    [<AbstractClass>]
    type Animal() =
        abstract member Legs : int

    [<AbstractClass>]
    type Dog() =
        inherit Animal()
        abstract member Description: string
        override this.Legs = 4

    type Pomeranian() =
        inherit Dog()
        override this.Description = "Furry"


    let steve = Pomeranian()

    // Casting Steve as various types
    let steveAsDog = steve :> Dog
    let steveAsAnimal = steve :> Animal
    let steveAsObj = steve :> obj


    // Dynamic cast  - Back up the hierarchy tree

    //let steveobject = steve :> obj
    let steveAsDog2 = steveAsObj :?> Dog

    let desc = steveAsDog2.Description
    //al desc: string = "Furry"

    //let asString = steveAsObj :?> string
    // Unable to cast object of type 'Pomeranian' to type 'System.String'.


    // Pattern matching against types

    // Example 5-19. Type tests in a pattern match

    // Pattern matching against types
    let whatIs (x : obj) =
        match x with
        | :? string as s -> printfn $"x is a string \"%s{s}\""
        | :? int as i -> printfn $"x is an int %d{i}"
        | :? list<int> as l -> printfn $"x is an int list '%A{l}'"
        | _ -> printfn "x is a '%s'" <| x.GetType().Name

    do  whatIs [1 .. 5]
    // x is an int list '[1; 2; 3; 4; 5]'
    do whatIs "Rosebud"
    // x is a string "Rosebud"
    do  whatIs (System.IO.FileInfo(@"C:\config.sys"))
    // x is a 'FileInfo'
