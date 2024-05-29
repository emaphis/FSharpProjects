// OO Programming
// <https://marketsplash.com/object-oriented-programming-in-f/>


////////////////////////////////////////////////////////////////
// Understanding The Basics Of Object Oriented Programming In F#

// Class Definition And Instantiation

type Car0(make: string, model: string) =
    member this.Make = make
    member this.Model = model

let myCar0 = Car0("Toyota", "Corolla")


// Methods and Properties

type Car1(make: string, model: string) =
    member this.Make = make
    member this.Model = model
    member this.Display() = printf "Car: %s %s\n" this.Make this.Model

let myCar1 = Car1("Toyota", "Corolla")
myCar1.Display()


// Inheritance

type ElectricCare1(make: string, model: string, batteryLife: int) =
    inherit Car1(make, model)
    member this.BatteryLife = batteryLife

let myElectricCar1 = ElectricCare1("Toyota", "Corolla", 100)


// Encapsulation

type Person0(name: string, age: int) =
    let mutable _age = age
    member this.Name = name
    member this.Age
        with get() = _age
        and set(value) =
            if value > 0 then _age <- value
    member this.Display() = printfn $"Person: name {this.Name} age {this.Age}"

let person0 = Person0("Jerry", 38)         
person0.Display()
person0.Age <- 39
person0.Display()


//////////////////////////////////////////////
// Implementing Classes And Objects In F#

// Defining A Class

type Student0(name: string, age: int) =
    member val Name = name with get, set
    member val Age = age with get, set
    member this.ShowInfo() = printfn "Name: %s, Age: %d" this.Name this.Age

// Creating An Object

let student0 = Student0("Alice", 20)

// Accessing Properties And Methods

student0.ShowInfo()

// Modifying Object Properties

student0.Name <- "Bob"
student0.ShowInfo()


//////////////////////////////////////////////////////////////
// Exploring Inheritance And Polymorphism

// Implementing Inheritance

type Vehicle2() =
    member val Speed = 0 with get, set
    member this.DisplaySpeed() = printfn "Speed: %d" this.Speed

type Car2() =
    inherit Vehicle2()
    member val Wheels = 4 with get, set

// Demonstrating Polymorphism

type Vehicle3() =
    abstract member DisplayInfo: unit -> unit
    member val Speed = 0 with get, set
    default this.DisplayInfo() = printfn "Tjos is a vehicle"

type Car3() =
    inherit Vehicle3()
    override this.DisplayInfo() = printf "This is a car`"

// Utilizing Interface Polymorphism

type IDisplayable =
    abstract member Display: unit -> unit

type Bike() =
    interface IDisplayable with
        member this.Display() = printfn "Displaying Bike"

type Scooter() =
    interface IDisplayable with
        member this.Display() = printfn "Displaying Scooter"


//////////////////////////////////////////////
// Using Interfaces And Abstraction

// Defining An Interface

type IPrintable =
    abstract member Print: unit -> unit

// Implementing An Interface

type Report() =
    interface IPrintable with
        member this.Print() = printfn "Printing Report"

// Using Abstraction

let printDocument (doc: IPrintable) = doc.Print()

// Polymorphic Behavior

let report = Report()
let printable: IPrintable = report
printable.Print()
