module Classes

(*

// Class definition:
type [access-modifier] type-name [type-params] [access-modifier] ( parameter-list ) [ as identifier ] =
[ class ]
[ inherit base-type-name(base-constructor-args) ]
[ let-bindings ]
[ do-bindings ]
member-list
...
[ end ]
// Mutually recursive class definitions:
type [access-modifier] type-name1 ...
and [access-modifier] type-name2 ...
...

 *)


module DefiningAClass =

    type CustomerName(firstName, middleInitial, lastName) =
        member this.FirstName = firstName
        member this.MiddleInitial = middleInitial
        member this.LastName = lastName


    (*
        // C# equivalent
        public class CustomerName
        {
            public CustomerName(string firstName,
               string middleInitial, string lastName)
            {
                this.FirstName = firstName;
                this.MiddleInitial = middleInitial;
                this.LastName = lastName;
            }

            public string FirstName { get; private set; }
            public string MiddleInitial { get; private set; }
            public string LastName { get; private set; }
        }

    *)

    // Specifying the type in the constructor


    type CustomerName2(firstName: string, middleInitial: string, lastName: string) =
        member this.FirstName = firstName
        member this.MiddleInitial = middleInitial
        member this.LastName = lastName


    // * Tuple parameters *
    // if you ever need to pass a tuple as a parameter to a constructor, you will have to
    // annotate it explicitly

    type NonTupleConstructor(x: int, y: int) =
        do printfn $"x=%i{x} y=%i{y}"

    type TupleConstructor(tuple: int * int)=
        let x, y = tuple
        do printfn $"x=%i{x} y=%i{y}"

    // Calls look the same
    let myNTC = NonTupleConstructor(1,2)
    let myTC = TupleConstructor(1,2)


module UnderstandingClassSignature =

    type MyClass(_intParam: int, _strParam: string) =
        member this.Two = 2
        member this.Square x = x * x

    (*

     // The corresponding class type signature
     type MyClass =
     new: intParam: int * strParam: string -> MyClass
     member Square: x: int -> int
     member Two: int

    *)


    // * Method signatures *
    // member Square : x:int -> int  // Method
    // val Square : int -> int


    // * Constructor signatures *
    // Constructor signatures are always called `new` instead of `val`  and they
    // always take tuple parameters

    // * class constructor signature *
    // new : intParam:int * strParam:string -> MyClass

    // standalone function signature
    // val new : int * string -> MyClass


    // * Property signatures *
    // Similar to `let` value signatures, but they have no defining value

    // member property
    // member Two : int

    // standalone value
    // val Two : int = 2


module PrivateFieldsAndLetBindings =

        // Private fields and functions using “let” bindings

        type PrivateValueExample(seed) =

            // private immutable value
            let _privateValue = seed + 1

            // private mutable value
            let mutable mutableValue = 42

            // private function definition
            let privateAddToSeed input =
                seed * input

            // public wrapper for private function
            member this.AddToSeed x =
                privateAddToSeed x

            // public wrapper for mutable value
            member this.SetMutableValue x =
                mutableValue <- x


        // Example code
        let instance =  PrivateValueExample(42)
        do printf $"%i{instance.AddToSeed 2}\n"

        instance.SetMutableValue 43

        (*
         // Type signature
         type PrivateValueExample =
         new: seed: int -> PrivateValueExample
         member AddToSeed: x: int -> int
         member SetMutableValue: x: int -> unit

        *)

        // let bindings are automatically private and immutable


        // * Mutable constructor parameters *
        // assign the parameter to a mutable let binding

        type MutableConstructorParameter(seed) =
            let mutable mutableSeed = seed

            // public wrapper for mutable value
            member this.SetSeed x =
                mutableSeed <- x


        type MutableConstructorParameter2(seed) =
            let mutable seed = seed  // shadow the parameter

            // public wrapper for mutable value
            member this.SetSeed x =
                seed <- x


module ConstructorBehaviorWithDoBlocs =

    // * Additional constructor behavior with “do” blocks *
    // Sometimes you need to do other processing of the parameters

    type DoExample(seed) =
        let privateValue = seed + 1

        // extra code to be done at constructor time
        do printfn $"the privateValue is now {privateValue}"


    // test example
    let doEx1 = DoExample(42)
    // the privateValue is now 43


    // The “do” code can also call any let-bound functions defined before it

    type DoPrivateFunctionExample(seed) =
        let privateValue = seed + 1

        // extra code to be done at constructor time
        do printfn "Hello world ..."

        // must come BEFORE the do block that calls it.
        let printPrivateValue() =
            do printfn $"the privateValue is now {privateValue}"

        // more code to be done at construction time
        do printPrivateValue()


    // test example
    let DoPFE =  DoPrivateFunctionExample(42)

    // Hello world ...
    // the privateValue is now 43


    // *Accessing the instance via “this” in a do block*
    // “do” bindings can access the instance while “let” bindings cannot.
    // The "let" bindings are created before instance is created.

    // If you need to call members of the instance from a “do” block
    // This is again done using a “self-identifier”, but this time it is attached
    // to the class declaration itself


    type DoPublicFunctionExample(seed) as this =
        // Note the "this" keyword in the declaration

        let privateValue = seed + 1

        // extra code to be done at construction time
        do this.PrintPrivateValue()

        // must be a member
        member this.PrintPrivateValue() =
            do printfn $"the privateValue is now {privateValue}"


    // test example
    let DoPuFE =  DoPublicFunctionExample(42)
    // the privateValue is now 43


module Methods =

    // * Example *
    type MethodExample() =

        // standalone method
        member this.AddOne x =
            x + 1

        // call another method
        member this.AddTwo x =
            this.AddOne x |> this.AddOne

        // parameterless method
        member this.Pi() =
            3.14159


    // test example
    let me =  MethodExample()
    printfn "%i" <| me.AddOne 42  // 43
    printfn "%i" <| me.AddTwo 42  // 44
    printfn "%f" <| me.Pi()  // 3.14159


    // * Tuple form vs. curried form *
    // methods can be tuple.

    type TupleAndCurriedMethodExample() =

        // curried form
        member this.CurriedAdd x y =
            x + y

        // tuple form
        member this.TupleAdd(x, y) =
            x + y


    (*
    type TupleAndCurriedMethodExample =
      new: unit -> TupleAndCurriedMethodExample
      member CurriedAdd: x: int -> y: int -> int
      member TupleAdd: x: int * y: int -> int

    *)

    // test example
    let tc = TupleAndCurriedMethodExample()

    let curAdd = tc.CurriedAdd 1 2
    let tupAdd = tc.TupleAdd(1,2)

    printfn $"%i{curAdd}"
    printfn $"%i{tupAdd}"

    // use partial application
    let addOne = tc.CurriedAdd 1
    let patAdd = addOne 2

    printfn $"%i{patAdd}"


    // * Let-bound functions in conjunction with class methods *
    // A common pattern is to create let-bound functions that do all
    // the heavy lifting, and then have the public methods call these
    // internal functions directly.

    type LetBoundFunctions() =

        let listReduce reducer list =
            list |> List.reduce reducer

        let reduceWithSum sum elem =
            sum + elem

        let sum list =
            list |> listReduce reduceWithSum

        // finally a public member
        member this.Sum = sum


    // example test
    let lbf = LetBoundFunctions()
    let sum = lbf.Sum [ 1 .. 10 ]
    printfn $"Sum is {sum}"


    // * Recursive methods *
    // Do not need the `rec` keyword

    type MethodExampleFib() =

        // recursive method without the "rec" keyword
        member this.Fib x =
            match x with
            | 0 | 1 -> 1
            | _ -> this.Fib (x-1) + this.Fib (x-2)


    // test example
    let mof = MethodExampleFib()
    let fib10 = mof.Fib(10)
    printfn $"Fib(10) = {fib10}"
    //Fib(10) = 89


    // * Type annotation for methods *

    type MethodExampleAnn() =
         // explicit type annotation
         member this.AddThree (x:int) :int =
             x + 3

    // Whatever
    let MEA = MethodExampleAnn()
    printfn $"3 + 4 = {MEA.AddThree(4)}"



module Properties =

    // Three types
    // - Immutable properties, where there is a “get” but no “set”.
    // - Mutable properties, where there is a “get” and also a (possibly private) “set”.
    // - Write-only properties, where there is a “set” but no “get”.  ???

    // * Example *

    type PropertyExample(seed) =
        // immutable by default
        // using a constructor parameter
        member this.Seed = seed


    // test example
    let pe1 = PropertyExample(42)
    do printfn $"Property seed = {pe1.Seed}"
    // Property seed = 42


    // * Mutable example *
    // with get() = ...
    // and set(value) = ...

    type PropertyExampleMut(seed) =
        // private mutable value
        let mutable myProp = seed

        // mutable property
        // changing a private mutable value
        member this.MyProp
            with get() = myProp
            and set value = myProp <- value


    // test example
    let pem1 = PropertyExampleMut(42)
    let val1 = pem1.MyProp
    do printfn $"MyProp = {val1}"
    do pem1.MyProp <- 33
    let val2 = pem1.MyProp
    do printfn $"MyProp = {val1}"


    // * Automatic properties *
    // member val MyProp = initialValue
    // member val MyProp = initialValue with get,set


    type PropertyExampleAuto(seed) =
        // mutable property
        // changing a private mutable value
        member val MyProp = seed with get, set


    // test example
    let pea1 = PropertyExampleAuto(42)
    let val3 = pea1.MyProp
    do printfn $"MyProp = {val3}"
    do pea1.MyProp <- 33
    let val4 = pea1.MyProp
    do printfn $"MyProp = {val4}"



    // * Complete property example *

    type PropertyExampleComp(seed) =
        // private mutable value
        let mutable myProp = seed

        // private function
        let square x = x * x

        // immutable property
        // using contractor parameter
        member this.Seed = seed

        // immutable property
        // using a private function
        member this.SeedSquared = square seed

        // mutable property
        // changing a private mutable value
        member this.MyProp
            with get() = myProp
            and set value = myProp <- value

        // mutable property with private set
        member this.MyProp2
            with get() = myProp
            and private set value =  myProp <- value

        // automatic immutable property
        member val ReadOnlyAuto = 1

        // automatic mutable property
        member val ReadWriteAuto = 1 with get, set

    // test example
    let pec = PropertyExampleComp(42)
    printfn "%i" <| pec.Seed
    printfn "%i" <| pec.SeedSquared
    printfn "%i" <| pec.MyProp
    printfn "%i" <| pec.MyProp2

    // try calling set
    do pec.MyProp <- 43  // OK
    let val6 = pec.MyProp
    do printfn $"myProp = {val6}"

    // try calling a private set
    //do pec.MyProp2 <- 43  // error


    // * Properties vs. parameterless methods *
    // not the same, methods take at least a `unit` parameter.
    // Methods can have side effects

    type ParameterlessMethodExample() =
        member this.MyProp = 1    // No parens!
        member this.MyFunc() = 1  // Note the () - takes a unit and returns an int


    // test examples
    let x = ParameterlessMethodExample()
    printfn "%i" <| x.MyProp      // No parens!
    printfn "%i" <| x.MyFunc()    // Note the ()


    // * Secondary constructors *
    // use the `new` construct
    type MultipleConstructors(param1, param2) =
        do printfn $"Param1=%i{param1} Param12=%i{param2}"

        // secondary constructor
        new(param1) =
            MultipleConstructors(param1,-1)


        new() =
            printfn "Constructing..."
            MultipleConstructors(13,17)

    // test
    let mc1 = MultipleConstructors(1,2)
    let mc2 = MultipleConstructors(42)
    let mc3 = MultipleConstructors()


module StaticMembers =

    type StaticExample() =
        member this.InstanceValue = 1
        static member StaticValue = 2   // no "this"


    // text example
    let instance = StaticExample()
    do printfn $"%i{instance.InstanceValue}"
    do printfn $"%i{StaticExample.StaticValue}"


module StaticConstructors =

    // There is no direct equivalent of a static constructor in F#, but you can create static
    // let-bound values and static do-blocks that are executed when the class is first used.

    type StaticConstructor() =

        // static field
        static let rand = System.Random()

        // static do
        static do printfn "class initialized ..."

        // instance member accessing static field
        member this.GetRand() = rand.Next()

    // test examples
    let sc = StaticConstructor()
    let nr = sc.GetRand()


module AccessibilityOfMembers =

    // public private internal
    // All class members are public by default not private

    // Example
    type AccessibilityExample() =
        member this.PublicValue = 1
        member private this.PrivateValue = 2
        member internal this.InternalValue = 3


    // test example
    let ae = AccessibilityExample()
    let pv = ae.PublicValue
    let iv = ae.InternalValue
    //let pr = ae.PrivateValue
    //error FS0491: The member or object constructor 'PrivateValue' is not accessible. Private members may only be


    // For properties, if the set and get have different accessibility,
    // you can tag each part with a separate accessibility modifier.

    type AccessibilityExample2() =
        let mutable privateValue = 2
        member this.PrivateSetProperty
            with get() =
                privateValue
            and private set value =
                privateValue <- value


    // test example
    let a2 = AccessibilityExample2();
    printf $"%i{a2.PrivateSetProperty}"  // ok to read
    // a2.PrivateSetProperty <- 43       // not ok to write


//module DefiningClassesForNETCode =

    // Tip: defining classes for use by other .NET code

    // Define F# inside of namespaces instead of modules.  Modules compile to static classes, which
    // .NET code may not like


module ConstructingAndUsingAClass =

    type MyClass(_intParam: int, _stParam: string) =
        member this.Two = 2
        member this.Square x = x * x

    // using `new` like C#
    let myInstance1 = MyClass(1, "hello")

    // But a constructor is just a regular function returning object so `new` can be skipped
    let myInstance2 = MyClass(1, "hello")

    // However:
    // In the case when you are creating a class that implements `IDisposible`, you will
    // get a compiler warning if you do not use new.

    let sr1 = System.IO.StringReader("")
    // warning FS0760: It is recommended that objects supporting the IDisposable interface
    // are created using the syntax 'new Type(args)',

    let sr2 = new System.IO.StringReader("")  // Ok


    // * Calling methods and properties

    let num2 = myInstance1.Two
    let num4 = myInstance1.Square 2


    // tuple-style methods and curried-style methods can be called in distinct ways:

    type TupleAndCurriedMethodExample() =
        member this.TupleAdd(x, y) = x + y
        member this.CurriedAdd x y = x + y


    let tc = TupleAndCurriedMethodExample()
    let num6 = tc.TupleAdd(1, 2)        // called with parens
    let num7 = tc.CurriedAdd 1 2        // called without parens
    let num8 = 2 |> tc.CurriedAdd 1     // partial application
