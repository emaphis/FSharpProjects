(* F# - Operator Overloading *)

(* he following example, shows a &plus; operator on complex numbers −

//overloading &plus; operator
static member (&plus;) (a : Complex, b: Complex) =
Complex(a.x &plus; b.x, a.y &plus; b.y)
*)

(* Implementation of Operator Overloading *)

//implementing a complex class with +, and - operators
//overloaded
type Complex(x: float, y: float) =
    member this.x = x
    member this.y = y

    // overloading the + operator
    static member (+) (a: Complex, b: Complex) =
        Complex(a.x + b.x, a.y + b.y)

    //overloading - operator
    static member (-) (a : Complex, b: Complex) =
      Complex(a.x - b.x, a.y - b.y)

    // overriding the ToString method
    override this.ToString (): string =
            this.x.ToString() + ", " + this.y.ToString()


// Creating two complex numbers
let c1 = Complex(7.0, 5.0)
let c2 = Complex(4.2, 3.1)

// addition and subtraction using the overloaded operators
let c3 = c1 + c2
let c4 = c1 - c1
