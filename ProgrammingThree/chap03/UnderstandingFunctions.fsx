// Understanding Functions

// mathematical functions
// f(x) = x^2 + x
// g(x) = x + 1

// translated to F#
open System.Net.NetworkInformation

let f x = x ** 2.0 + x
let g x = x + 1.0


module Immutability =

    // Example 3-1. Summing a list of squares using imperative and functional styles

    let square x = x * x

    let imperativeSum numbers =
        let mutable total = 0
        for i in numbers do
            let x = square i
            total <- total + x
        total

    let functionalSum numbers =
        numbers
        |> Seq.map square
        |> Seq.sum

    let listInt = [ 1; 2; 3; 4; 5 ]

    let sum1 = imperativeSum listInt
    //val sum1: int = 55

    let sum2 = functionalSum listInt
    //al sum2: int = 55


module FunctionValues =
    // Functions are considered data in F#

    // Example 3-2. Example of higher-order functions

    let negate x = -x

    let listInt = [1 .. 10]

    let listNeg = List.map negate listInt
    //val listNeg: int list = [-1; -2; -3; -4; -5; -6; -7; -8; -9; -10

    // lambdas - function values
    let sum1 = (fun x -> x + 3) 5
    //val sum1: int = 8


    // Rewrite example 3-2 using a lambda
    let listNeg2 = List.map (fun x -> -x) listInt
    //val listNeg2: int list = [-1; -2; -3; -4; -5; -6; -7; -8; -9; -10]


    // Partial function application

    // a practical example that appends text to a file using the .NET libraries:
    open System.IO

    let appendFile(fileName: string) (text: string) =
        use file = new StreamWriter(fileName, true)
        file.Write(text)
        file.Close()

    //val appendFile: fileName: string -> text: string -> unit
    
    do appendFile @"C:\temp\Log.txt" "Processing Event X..."

    // Create a new function, with a partially applied call to appendFile.
    let appendLogFile = appendFile @"C:\temp\Log.txt"
    //val appendLogFile: (string -> unit

    // Append text to 'C:\temp\Log.txt'
    appendLogFile "Processing Event Y..."
    
    // Currying.
    do List.iter (fun i -> printfn "%d" i) [1..3]

    // Using printfn applying the "%d" parameter, which returns a new
    // function with type int -> unit opposed to string -> int -> unit
    do List.iter (printfn "%d") [1..3]


    // Functions returning functions
    
    // Function returning functions
    let generatePowerOfFunc baseValue =
        (fun exponent -> baseValue ** exponent)
    //val generatePowerOfFunc: baseValue: float -> exponent: float -> float

    let powerOfTwo = generatePowerOfFunc 2.0

    let num1 = powerOfTwo 8.0
    //val num1: float = 256.0

    let powerOfThree = generatePowerOfFunc 3.0

    let num2 = powerOfThree 2.0
    //val num2: float = 9.0



module RecursiveFunctions =

    // define a recursive function
    let rec factorial x =
        if x <= 1 then
            1
        else
            x * factorial (x - 1)

    let fact5 = factorial 5
    //val fact5: int = 120
    

    /// Functional for loop
    let rec forLoop body times =
        if times <= 0 then ()
        else
            body()
            forLoop body (times - 1)
    
    //val forLoop: body: (unit -> unit) -> times: int -> unit

    /// Functional while loop
    let rec whileLoop predicate body =
        if predicate() then
            body()
            whileLoop predicate body
        else
            ()

    //val whileLoop: predicate: (unit -> bool) -> body: (unit -> unit) -> unit

    do forLoop (fun () -> printfn "Looping ...") 3

    //do whileLoop
    //    (fun () -> DateTime.Now.DayOfWeek <> DayOfWeek.Saturday)
    //    (fun () printfn "I wish it were the weekend ...")


    // Mutual recursion

    // Mutually recursive functions using "rec" and "and"
    let rec isOdd x =
        if x = 0 then   false
        elif x = 1 then true
        else isEven(x - 1)
    and isEven x =
        if x = 0 then   true
        elif x = 1 then false
        else isOdd(x - 1)

    //val isOdd: x: int -> bool
    //val isEven: x: int -> bool

    let odd = isOdd 314
    //val odd: bool = false
    let even = isEven 314
    //val even: bool = true


module SymbolicOperators =

    //symbolic operator can be made up of any sequence of !%&*+-./<=>@^|? 
    
    /// '!' as Factorial
    let rec (!) x =
        if x <= 1 then 1
        else x * !(x - 1)

    //val (!) : x: int -> int

    let fact1 = !5
    //val fact1: int = 120
    

    // Define (===) to compare strings based on regular expressions

    open System.Text.RegularExpressions

    let (===) str (regex : string) =
        Regex.Match(str, regex).Success
    //val (===) : str: string -> regex: string -> bool

    let bool1 =  "The quick brown fox" === "The (.*) fox"
    //val bool1: bool = true


    // Symbolic operators can be passed around to higher order functions

    // Sum a list using the symbolic (+) function
    let num1 = List.fold (+) 0 [1..10]
    //val num1: int = 55

    // Multiply all the elements using the (*) symbolic function
    let num2 = List.fold (*) 1 [1..10]
    //val num2: int = 3628800
    
    let minus = (-)
    //val minus: (int -> int -> int)

    let num3 = List.fold minus 10 [3; 3; 3]
    //val num3: int = 1
    


module FunctionComposition =

    // Functionality in one massive function.
    open System.IO

    let sizeOfFolder1 folder =
    
        // Get all the files under the path
        let fileInFolder : string [] =
            Directory.GetFiles(folder, "*.*",
                                SearchOption.AllDirectories)

        // Map those files to their corresponding FileInfo Object
        let fileInfos : FileInfo [] =
            Array.map (fun (file : string) -> new FileInfo(file))
                        fileInFolder

        // Map those FileInfo object to the file's size
        let fileSizes : int64 [] =
            Array.map (fun (info : FileInfo) -> info.Length)
                                fileInfos

        // Total the file sizes
        let totalSize = Array.sum fileSizes

        // Return the total size of the files
        totalSize
    
    sizeOfFolder1 @"C:\temp"
    //val it: int64 = 8013936L


    // Avoid the let bindings by nesting the lambdas.

    let uglySizeOfFolder folder =
         Array.sum
            (Array.map
            (fun (info : FileInfo) -> info.Length)
                 (Array.map
                 (fun file -> new FileInfo(file))
                    (Directory.GetFiles(
                         folder, "*.*",
                         SearchOption.AllDirectories))))

    uglySizeOfFolder @"C:\temp"
    //val it: int64 = 8013936L

    // Pipe-forward operator
    //let (|>) x f = f x

    // Rearrange the parameters of a function so that you present the last parameter
    // of the function first.

    do List.iter (printfn "%d") [1 .. 3]

    // with the pipe forward operator
    do [1 .. 3] |> List.iter (printfn "%d")


    let sizeOfFolderPiped folder =
        
        let getFiles path =
                Directory.GetFiles(path, "*.*", SearchOption.AllDirectories)

        let totalSize =
            folder
            |> getFiles
            |> Array.map (fun file -> new FileInfo(file))
            |> Array.map (fun info -> info.Length)
            |> Array.sum

        totalSize

    
    sizeOfFolderPiped @"C:\temp"
    //val it: int64 = 8_013_936L


    // Function Composition operator

    //let (>>) f g x= g(f x)
    //val (>>) : f: ('a -> 'b) -> g: ('b -> 'c) -> x: 'a -> 'c

    let sizeOfFolderComposed =
        
        let getFiles folder =
            Directory.GetFiles(folder, "*.*", SearchOption.AllDirectories)
        
        // The result of this expression is a function that takes
        // one parameter, which will be passed to getFiles and piped
        // through the following functions.
        getFiles
        >> Array.map (fun file -> FileInfo(file))
        >> Array.map (fun info -> info.Length)
        >> Array.sum
    
    //val sizeOfFolderComposed: (string -> int64)    
    
    sizeOfFolderComposed @"C:\temp"    
    //val it: int64 = 8013936L

    
    // Pipe-backward operator
    
    //let (<|) f x = f x
    //val (<|) : f: ('a -> 'b) -> x: 'a -> 'b
    
    do List.iter (printfn "%d") <| [1 .. 3]
    
    
    // Backward composition operator
    
    //let (<<) f g x = f(g x)
    //val (<<) : f: ('a -> 'b) -> g: ('c -> 'a) -> x: 'c -> 'b
    
    // Backward Composition
    let square x = x * x
    let negate x = -x
    
    // Using (>>) negates the square
    let sq1 = (square >> negate) 10
    //val sq1: int = -100
    
    // ** But what we really want is the square of the negation
    //    So we use (<<)
    let sq2 = (square << negate) 10
    //val sq2: int = 100
    
    // Filtering lists
    [ [1]; []; [4;5;6]; [3;4]; []; []; []; [9] ]
    |> List.filter(not << List.isEmpty)
    
    //val it: int list list = [[1]; [4; 5; 6]; [3; 4]; [9]]
 