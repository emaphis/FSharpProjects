module InheritanceAndAbstractClasses

module Inheritance =

    // To declare that a class inherits from another class, use the syntax:

    (*
    public class MyBaseClass
    {
        public MyBaseClass(int param1)
        {
            this.Param1 = param1;
        }
        public int Param1 { get; private set; }
    }

    public class MyDerivedClass: MyBaseClass
    {
        public MyDerivedClass(int param1,int param2): base(param1)
        {
            this.Param2 = param2;
        }
        public int Param2 { get; private set; }
    }
    *)

    type BaseClass(param1) =
        member this.Param1 = param1

    type DerivedClass(param1, param2) =
        inherit BaseClass(param1)
        member this.Param2 = param2

    // Text example
    let derived = DerivedClass(1, 2)
    printfn $"param1={derived.Param1}"
    printfn $"param2={derived.Param2}"


module AbstractAndVirtualMethods =

    // * Defining abstract methods in the base class *

    // n C#, an abstract method is indicated by the abstract keyword plus the method signature.
    // In F#, it is the same concept, except that the way that function signatures are written

    // Concrete function definition
    let Add x y = x + y
    // Signature: val Add: x: int -> y: int -> int

    // So to define an abstract method, we use the signature syntax, along with the
    // `abstract member` keywords:

    [<AbstractClass>]
    type BaseClass() =
        abstract member Add: int -> int -> int
        // Signature: abstract Add: int -> int -> int


module DefiningAbstractProperties =

        [<AbstractClass>]
        type BaseClass() =
            abstract member Pi : float

        // f the abstract property is read/write, you add the get/set keywords.

        [<AbstractClass>]
        type BaseClass2() =
            abstract Area : float with get, set


module DefaultImplementations  =

        // To provide a default implementation of an abstract method
        // in the base class, use the default keyword

        // with default implementation
        type BaseClass() =
            // abstract method
            abstract member Add: int -> int -> int
            // abstract property
            abstract member Pi: float

            // defaults
            default this.Add x y = x + y
            default this.Pi = 3.15


        let bs = BaseClass()
        let add1 = bs.Add 3 4
        let pi = bs.Pi
        printfn $" {add1}, {pi}"


module AbstractClasses =

    [<AbstractClass>]
    type AbstractBaseClass() =
        // abstract method
        abstract member Add: int -> int -> int

        // abstract immutable property
        abstract member Pi : float

        // abstract read/write property
        abstract member Area : float with get, set


module OverridingMethods =

    [<AbstractClass>]
    type Animal() =
        abstract member MakeNoise: unit -> unit

    type Dog() =
        inherit Animal()
        override this.MakeNoise() = printfn "woof"


    // test example
    //let animal = Animal()
    // error FS0759: Instances of this type cannot be created since it has been marked abstract or not all methods have
    // been given implementations. Consider using an object expression '{ new ... with ... }' instead.

    let dog = Dog()
    do dog.MakeNoise()

    // And to call a base method, use the base keyword, just as in C#.
    type Vehicle() =
        abstract member TopSpeed: unit -> int
        default this.TopSpeed() = 60

    type Rocket() =
        inherit Vehicle()
        override this.TopSpeed() =
            base.TopSpeed() * 10

    // test example
    let vehicle = Vehicle()
    let vt = vehicle.TopSpeed()
    printfn $"vehicle top speed = {vt}"

    let rocket = Rocket()
    let rt = rocket.TopSpeed()
    printfn $"rocket top speed = {rt}"
