// Object-Oriented Programming
// Inheritance

// Subclasses

type Person(name) =
    member x.Name = name
    member x.Greet() = printfn $"Hi, I'm {x.Name}"

type Student(name, studentID: int) =
    inherit Person(name)

    let mutable _GPA = 0.0

    member x.StudentID = studentID
    member x.GPA
        with get() = _GPA
        and set value = _GPA <- value

type Worker(name, employer: string) =
    inherit Person( name)

    let mutable _salary = 0.0

    member x.Salary
        with get() = _salary
        and set value = _salary <- value

    member x.Employer = employer


let somePerson, someStudent, someWorker =
    Person "Juliet",
    Student("Monique", 123456),
    Worker("Carla", "Awesome Hair Salon")

let id = someStudent.StudentID
let emp = someWorker.Employer
let smw = someWorker.ToString()


// Overriding Methods

type Person2(name) =
    member x.Name = name
    member x.Greet() = printfn $"His, I'm %s{x.Name}"

    // The ToString() method is inherited from System.Object
    override x.ToString (): string = x.Name


type Person3(name) =
    member x.Name = name

    abstract Greet : unit -> unit
    default x.Greet() = printfn $"Hi!, I'm %s{x.Name}"

type Quebecois3(name) =
    inherit Person3(name)

    override x.Greet() = printfn $"Bonjour, je m'appelle %s{x.Name}, eh."

let terrance = Person3 "Terrance"
let phillip = Quebecois3  "Phillip"

terrance.Greet()
phillip.Greet()


// Abstract Classes

open System

type Point =
  { X : int;
    Y : int }

[<AbstractClass>]
type Shape(position: Point) =
    member x.Position = position

    override x.ToString() =
        $"position = {{%i{position.X}, %i{position.Y}}}, area = %f{x.Area()}"

    abstract member Draw : unit -> unit
    abstract member Area : unit -> float


type Circle(position: Point, radius: float) =
    inherit Shape(position)

    member x.Radius = radius
    override x.Draw() = printfn "(Circle)"
    override x.Area (): float = Math.PI * radius * radius


type Rectangle(position : Point, width : float, height : float) =
    inherit Shape(position)

    member x.Width = width
    member x.Height = height
    override x.Draw() = printfn "(Rectangle)"
    override x.Area() = width * height


type Square(position : Point, width : float) =
    inherit Shape(position)

    member x.Width = width
    member x.ToRectangle() = Rectangle(position, width, width)
    override x.Draw() = printfn "(Square)"
    override x.Area() = width * width


type Triangle(position : Point, sideA : float, sideB : float, sideC : float) =
    inherit Shape(position)

    member x.SideA = sideA
    member x.SideB = sideB
    member x.SideC = sideC

    override x.Draw() = printfn "(Triangle)"
    override x.Area() =
        (* Heron's formula *)
        let a, b, c = sideA, sideB, sideC
        let s = (a + b + c) / 2.0
        Math.Sqrt(s * (s - a) * (s - b) * (s - c) )



let position = { X = 0; Y = 0 }

let circle, rectangle, square, triangle =
    Circle(position, 5.0),
    Rectangle(position, 2.0, 7.0),
    Square(position, 10.0),
    Triangle(position, 3.0, 4.0, 5.0)

let cir1 = circle.ToString()
let tri1 = triangle.ToString()
let sqr1 = square.ToString()
let width1 = square.Width
let rec1 = square.ToRectangle().ToString()
let msm1 = rectangle.Height, rectangle.Width



// Working With Subclasses

// Up-casting and Down-casting

let regularString = "Hello world"
let upcastString = "Hello world" :> obj

let str2 = regularString.ToString()
let len2 = regularString.Length

let str3 = upcastString.ToString()

//let len3 = upcastString.Length
// error FS0039: The type 'Object' does not define a field, constructor, or member named 'Length'.


// Up-casting is considered "safe", because a derived class is guaranteed to have
// all the same members as an ancestor class.

// We can, go in the opposite direction: we can down-cast from an ancestor class
// to a derived class using the :?> operator:

let intAsObj = 20 :> obj

let pair1 = intAsObj, intAsObj.ToString()
//val pair1: int * string = (20, "20")

let intDownCast = intAsObj :?> int

let pair2 = intDownCast, intDownCast.ToString()
//val pair2: int * string = (20, "20")

// BOOM!
//let stringDownCast = intAsObj :?> string
// System.InvalidCastException: Unable to cast object of type 'System.Int32' to type 'System.String


// Up-casting example using shapes

//open System

let shapes =
        [ Circle(position, 5.0) :> Shape;
          Circle(position, 12.0) :> Shape;
          Square(position, 10.5) :> Shape;
          Triangle(position, 3.0, 4.0, 5.0) :> Shape;
          Rectangle(position, 5.0, 2.0) :> Shape ]


let main1() =
    shapes
    |> Seq.iter ( fun shp -> printfn $"shp.ToString: %s{shp.ToString()}" )


main1()


// Public, Private, and Protected Members
