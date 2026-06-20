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
        then
            printfn $"Line constructor: {{(%F{this.X1}, %F{this.Y1}), (%F{this.X2}, %F{this.Y2})}}, Line.Length: %F{this.Length}"

    member x.Length =
        let sqr x = x * x
        sqrt(sqr(x.X1 - x.X2) + sqr(x.Y1 - x.Y2))
end


let line = Line(1.0, 1.0, 4.0, 2.5)


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
