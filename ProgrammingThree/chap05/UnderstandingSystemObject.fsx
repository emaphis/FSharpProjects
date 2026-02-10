module UnderstandingSystemObject

    // System.Object is the base of the CLR object syste4m.

module CommonMethods =

    // These are all overridable for special cases
    // just like the Object classes in Java of C#

    // Override toString
    type PunctuationMark =
        | Period
        | Comma
        | QuestionMark
        | ExclamationPoint
        override this.ToString() =
            match this with
            | Period  -> "Period (.)"
            | Comma   -> "Comma (,)"
            | QuestionMark     -> "QuestionMark (?)"
            | ExclamationPoint -> "ExclamationPoint (@)"

    let x = Comma
    let cm = x.ToString()
    // val cm: string = "Comma (,)"


    // GetHashCode

    let code1 = "alpha".GetHashCode()
    // val code1: int = 734667780

    let code2 = "bravo".GetHashCode()
    //val code2: int = 1232909855


    // Equals

    let bl1 = "alpha".Equals 4
    // val bl1: bool = false

    let bl2 = "alpha".Equals "alpha"
    // val bl2: bool = true


    // GetType

    let stringType = "A String".GetType()
    // val stringType: System.Type = System.String

    let wut = stringType.AssemblyQualifiedName
    // val wut: string =
    //  "System.String, System.Private.CoreLib, Version=10.0.0.0, Cult"+[44 chars]


module ObjectEquality =

    // Value type equality
    let x = 42
    let y = 42

    let bool1 = x = y    // equlity opperator in F#

    // Referential equality
    // Example 5-1

    // default equals
    type ClassType1(x: int) =
        member this.Value = x

    let ct1 = ClassType1(42)
    let ct2 = ClassType1(42)

    let bool2 = ct1 = ct2
    // val bool2: bool = false

    // override equals
    type ClassType2(x: int) =
        member this.Value = x
        override this.Equals(o: obj) =
            match o with
            | :? ClassType2 as other -> (other.Value = this.Value)
            | _  -> false
        override this.GetHashCode() = x

    let ct3 = ClassType2 42
    let ct4 = ClassType2 42
    let ct5 = ClassType2 10000

    let bool3 = ct3 = ct4
    // val bool3: bool = true

    let bool4 = ct3 = ct5
    // val bool4: bool = false


module  GeneratedEquality =

    // tuples, discriminated unions and records act like values even though
    // they are reference types.  The compiler overrides `Equals`

    // tuple equality

    let x = 1, 'a', "str"
    // val x: int * char * string = (1, 'a', "str")

    let bool1 = x = x
    // true

    let bool2 = x = (1, 'a', "different str")
    // false


    // record equality
    type RecType = { Field1 : string; Field2 : float }

    let x1 = { Field1 = "abc"; Field2 = 3.5 }
    let y1 = { Field1 = "abc"; Field2 = 3.5 }
    let z1 = { Field1 = "xxx"; Field2 = 0.0 }

    let bool3 = x1 = y1  // true
    let bool4 = x1 = z1  // false


    // Disciminated unions

    type DUType =
        | A of int * char
        | B

    let x2 = A(1, 'k')
    let y2 = A(1, 'k')
    let z2 = B

    let bool5 = x2 = y2  // true
    let bool6 = x2 = z2  // false


    // Example 5-2. Customizing generated equality

    // Referential Equality on Functional Types
    [<ReferenceEquality>]
    type RefDUType =
        | A of int * char
        | B


    // Declare two conceputally equal values
    let x3 = A(4, 'A')
    let y3 = A(4, 'A')

    let bool7 = x3 = y3  // false
    let bool8 = x3 = x3  // ture
