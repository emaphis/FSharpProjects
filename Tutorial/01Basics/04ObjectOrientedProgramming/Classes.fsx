// Object Oriented Programming
// Classes

// Classes and Objects

// Defining an Object

(*
type TypeName optional-type-arguments arguments [ as ident ] =
    [ inherit type { as base } ]
    [ let-binding | let-rec bindings ] *
    [ do-statement ] *
    [ abstract-binding | member-binding | interface-implementation ] *
*)

type Account(number: int, holder: string) = class
    let mutable amount = 0m

    member x.Number = number
    member x.Holder = holder
    member x.Amount = amount

    member x.Deposit value = amount <- amount + value
    member x.Withdraw value = amount <- amount - value
end


let bob = Account(123456, "Bob’s Saving")


// access properties using .propertyName notation.

let printAccount (x: Account) =
    printfn $"x.Number: %i{x.Number}, x.Holder: %s{x.Holder}, x.Amount: %M{x.Amount}"

printAccount bob
//x.Number: 123456, x.Holder: Bob’s Saving, x.Amount: 0

bob.Deposit 100M

printAccount bob
//x.Number: 123456, x.Holder: Bob’s Saving, x.Amount: 100

bob.Withdraw 29.95M

printAccount bob
//x.Number: 123456, x.Holder: Bob’s Saving, x.Amount: 70.05


// Example

// using Account class

let homer = Account (12345, "Homer")
let marge = Account (67890, "Marge")

let transfer amount (source: Account) (target: Account) =
    source.Withdraw amount
    target.Deposit amount


let main2() =
    let printAccounts() =
        [homer; marge] |> Seq.iter printAccount

    printfn "\nInitializing account"
    homer.Deposit 50M
    marge.Deposit 100M
    printAccounts()

    printfn "\nTransferring $30 from Homer to Marge"
    transfer 30M marge homer
    printAccounts()

    printfn "\nTransferring $75 from Homer to Marge"
    transfer 75M homer marge
    printAccounts()

main2()


// Example using the do keyword
// The do keyword is used for post-constructor initialization.

open System
open System.Net

type Stock(symbol: string) = class
    let url = "https://download.finance.yahoo.com/d/quotes.csv?s=" + symbol + "&f=sl1d1t1c1ohgv&e=.csv"

    let mutable _symbol = String.Empty
    //let mutable _current
    let mutable _open = 0.0
    let mutable _high = 0.0
    let mutable _low = 0.0
    let mutable _volume = 0

    do
        (* We initialize our object in the do block *)

        let webClient = new WebClient()

        (* Data comes back as a comma-separated list, so we split it
           on each comma *)
        let data = webClient.DownloadString(url).Split [|','|]

        _symbol <- data[0]
      //  _current <- float data[1]
        _open <- float data[5]
        _high <- float data[6]
        _low <- float data[7]
        _volume <- int data[8]

    member x.Symbol = _symbol
    //member x.Current = _current
    member x.Open = _open
    member x.High = _high
    member x.Low = _low
    member x.Volume = _volume
end

(*
let main3() =
    let stocks =
        ["msft"; "noc"; "yhoo"; "gm"]
        |> Seq.map (fun x -> Stock(x))

    stocks |> Seq.iter (fun x -> printfn "Symbol: %s (%F)" x.Symbol x.Current)


main3()
*)

// Explicit Class Definition
// val defines a field in our object.

(*
type TypeName =
    [ inherit type ]
    [ val-definitions ]
    [ new ( optional-type-arguments arguments ) [ as ident ] =
      { field-initialization }
      [ then constructor-statements ]
    ] *
    [ abstract-binding | member-binding | interface-implementation ] *
*)


type Line = class
    val X1: float
    val Y1: float
    val X2: float
    val Y2: float

    new (x1, y1, x2, y2) as this =
        { X1 = x1; Y1 = y1;
          X2 = x2; Y2 = y2;}
        then
            printfn $"Line constructor: {{(%F{this.X1}, %F{this.Y1}), (%F{this.X2}, %F{this.Y2})}}, Line.Length: %F{this.Length}"

    member x.Length =
        let sqr x = x * x
        sqrt(sqr(x.X1 - x.X2) + sqr(x.Y1 - x.Y2))
end


let line = Line(1.0, 1.0, 4.0, 2.5)
// Line constructor: {(1.000000, 1.000000), (4.000000, 2.500000)}, Line.Length: 3.354102


// Example Using Two Constructors

//open System
//open System.Net


type Car = class
    val used : bool
    val owner : string
    val mutable mileage : int

    (* first constructor *)
    new (owner) =
        { used = false;
          owner = owner;
          mileage = 0 }

    (* another constructor *)
    new (owner, mileage) =
        { used = true;
          owner = owner;
          mileage = mileage }
end


let main4() =
    let printCar(c: Car) =
        printfn $"c.used: {c.used}, c.owner: {c.owner}, c.mileage: {c.mileage}"

    let stevesNewCar = Car "Steve"
    let bobsUsedCar = Car("Bob", 83000)

    let printCars() =
        [stevesNewCar; bobsUsedCar] |> Seq.iter printCar

    printfn "\nCars created"
    printCars()

    printfn "\nsteve drives all over the state"
    stevesNewCar.mileage <- stevesNewCar.mileage + 780
    printCars()

    printfn "\nBob commits odometer fraud"
    bobsUsedCar.mileage <- 0
    printCars()


main4()


// Differences Between Implicit and Explicit Syntaxes

// The class body acts as a constructor
type Car1(make : string, model : string) = class
    // x.Make and x.Model are property getters
    // (explained later in this chapter)
    // Notice how they can access the
    // constructor parameters directly
    member x.Make = make
    member x.Model = model

    // This is an extra constructor.
    // It calls the primary constructor
    new () = Car1("default make", "default model")
end

type Car2 = class
    // In this case, we need to declare
    // all fields and their types explicitly
    val private make : string
    val private model : string

    // Notice how field access differs
    // from parameter access
    member x.Make = x.make
    member x.Model = x.model

    // Two constructors
    new (make : string, model : string) = {
        make = make
        model = model
    }
    new () = {
        make = "default make"
        model = "default model"
    }
end


// Class Inference

// Class Inference
type Car3(make: string, model: string) =
    member x.Make = make
    member x.Model = model


// Class Explicit
type Car4(make: string, model: string) = class
    member x.Make = make
    member x.Model = model
end


// Class Members

// Instance and Static Members

type SomeClass1(prop: int) =
    member x.Prop = prop
    static member SomeStaticMethod = "This is a static method"

// Called from class
let str1 = SomeClass1.SomeStaticMethod

let instance1 = SomeClass1 5

// Called from instansnce
let int1 = instance1.Prop


// We can invoke instance methods from objects passed into static methods,

type SomeClass2(prop: int) =
    member x.Prop = prop
    static member SomeStaticMethod = "This is a static method"
    static member Copy (source: SomeClass2) = SomeClass2 source.Prop

let instance2 = SomeClass2 10

let shallowCopy = instance2
let deepCopy = SomeClass2.Copy instance2

//open System

Object.ReferenceEquals(instance2, shallowCopy)
Object.ReferenceEquals(instance2, deepCopy)


// Getters and Setters

(*
member alias.PropertyName
        with get() = some-value
        and set(value) = some-assignment
*)


type IntWrapper1() =
    let mutable num = 0

    member x.Num
        with get() = num
        and set value = num <- value

let wrapper1 = IntWrapper1()

let num1 = wrapper1.Num
wrapper1.Num <- 20

let num2 = wrapper1.Num

// Since getters and setters are methods, they can be used for processing data

type IntWrapper2() =
    let mutable num = 0

    member x.Num
        with get() = num
        and set value =
            if value > 10 || value < 0 then
                raise (Exception "Values must be between 0 and 10")
            else
                num <- value


let wrapper2 = IntWrapper2()

wrapper2.Num <- 5

let num3 = wrapper2.Num

//wrapper2.Num <- 20
//System.Exception: Values must be between 0 and 10


// Adding Members to Records and Unions

// Record example:

type Line2 =
   { X1: float; Y1: float;
     X2: float; Y2: float }
   with
       member x.Length =
           let sqr x = x * x
           sqrt(sqr(x.X1 - x.X2) + sqr(x.Y1 - x.Y2))

        member x.ShiftH amount =
            { x with X1 = x.X1 + amount; X2 = x.X2 + amount }

        member x.ShiftV amount =
            { x with Y1 = x.Y1 + amount; Y2 = x.Y2 + amount }


let line2 = { X1 = 1.0; Y1 = 2.0; X2 = 5.0; Y2 = 4.5 }

let len1 =  line2.Length
let line3 = line2.ShiftH 10.0
let line4 = line2.ShiftV -5.0


// Discriminated Union example

type shape =
    | Circle of float
    | Rectangle of float * float
    | Triangle of float * float
    with
        member x.Area =
            match x with
            | Circle r -> Math.PI * r * r
            | Rectangle(b, h) -> b * h
            | Triangle(b, h) -> b * h / 2.0

        member x.Scale value =
            match x with
            | Circle r -> Circle(r + value)
            | Rectangle(b, h) -> Rectangle(b + value, h + value)
            | Triangle(b, h) -> Triangle(b + value, h + value)


let mycircle = Circle 5.0

let ar1 = mycircle.Area
let cr1 = mycircle.Scale 7.0


// Generic classes

type 'a GenericWrapper1(initialValue: 'a) =
    let mutable internalVal = initialValue

    member x.Value
        with get() = internalVal
        and set value = internalVal <- value


let intWrapper = GenericWrapper1<_> 5

let int2 = intWrapper.Value

intWrapper.Value <- 20

let int3 = intWrapper.Value

//intWrapper.Value <- 2.0


// Pattern Matching Objects

(*
match arg with
| :? type1 -> expr
| :? type2 -> expr
*)

type Cat() =
    member x.Meow() = printfn "Meow"

type Person(name: string) =
    member x.Name = name
    member x.SayHello() = printfn $"Hi, I'm %s{x.Name}"

type Monkey() =
    member x.SwingFromTrees() = printfn "swinging from trees"

let handlesAnything(o: obj) =
    match o with
    | null  -> printfn "<null>"
    | :? Cat as cat -> cat.Meow()
    | :? Person as person -> person.SayHello()
    | _ -> printfn $"I don't recognize type'{o.GetType().Name}'"


let main5() =
    let cat = Cat()
    let bob = Person "Bob"
    let bill = Person "Bill"
    let phrase = "Hello world!"
    let monkey = Monkey()

    handlesAnything cat
    handlesAnything bob
    handlesAnything bill
    handlesAnything phrase
    handlesAnything monkey
    handlesAnything null

main5()
