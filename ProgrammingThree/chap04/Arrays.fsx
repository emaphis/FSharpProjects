module Arrays

// Arrays in .NET are a contiguous block of memory containing zero or more elements,
// each of which can be modified individually.

// Using the array comprehension syntax
let perfectSquares = [| for i in 1 .. 7 -> i * i |]
//val perfectSquares: int array = [|1; 4; 9; 16; 25; 36; 49|]

// Manually declared
let perfectSquares2 =  [| 1; 4; 9; 16; 25; 36; 49; 64; 81 |]


module IndexingAnArray =

    printfn
        "The first three perfect squares are %d, %d, and %d "
        perfectSquares[0]
        perfectSquares[1]
        perfectSquares[2]
    //The first three perfect squares are 1, 4, and 9


    // Example 4-9. ROT13 encryption in F#

    open System

    /// Encrypt a letter using ROT13
    let rec rot13Encrypt (letter : char) =
        // Move the letter forward 13 places in the alphabet (looping around)
        // Otherwise ignore
        if Char.IsLetter(letter) then
            let newLetter =
                (int letter)
                |> (fun letterIdx -> letterIdx - (int 'A'))
                |> (fun letterIdx -> (letterIdx + 13) % 26)
                |> (fun letterIdx -> letterIdx + (int 'A'))
                |> char
            newLetter
        else
            letter
        //val rot13Encrypt: letter: char -> char

    /// Loop through each array element, encrypting each letter.
    let encryptText (text : char[]) =
        for idx = 0 to text.Length - 1 do
            let letter = text[idx]
            text[idx] <- rot13Encrypt letter  // mutable


    do
        let text = Array.ofSeq  "THE QUICK BROWN FOX JUMPED OVER THE LAZY DOG"
        printfn $"Original = {String text}"
        encryptText text
        printfn $"Encrypted = {String text}"

        // A unique trait of ROT13 is that to decrypt, simply encrypt again
        encryptText text
        printfn $"Decrypted = {String text}"

    //Original = THE QUICK BROWN FOX JUMPED OVER THE LAZY DOG
    //Encrypted = GUR DHVPX OEBJA SBK WHZCRQ BIRE GUR YNML QBT
    //Decrypted = THE QUICK BROWN FOX JUMPED OVER THE LAZY DOG


    // Out of bounds
    let alphabet = [| 'a' .. 'z' |]

    // First char
    let chr1 = alphabet[0]

    // Nonexistent element
    let chr2 = alphabet[10000]
    //m.IndexOutOfRangeException: Index was outside the bounds of the array.


module ArraySlices =

    // array[ lowerbound .. upperbound ]

    // Example 4-10. Using array slices

    open System

    let daysOfWeek = Enum.GetNames (typedefof<DayOfWeek>)

    // Standard array slice, elements 2 through 4
    let wkDays = daysOfWeek[ 2 .. 4 ]
    //val wkDays: string array = [|"Tuesday"; "Wednesday"; "Thursday"|]

    // Just specify lower bound, elements 4 to end
    let endDays = daysOfWeek[ 4 .. ]
    //val endDays: string array = [|"Thursday"; "Friday"; "Saturday"|]

    // Just specify an upper bound, elements 0 to 2
    let begDays = daysOfWeek.[..2]
    //val it: string array = [|"Sunday"; "Monday"; "Tuesday"|]

    // Specify no bounds, get all of the elements, just copyy yhr attay
    let allDays = daysOfWeek[*]
    //val allDays: string array =
    //  [|"Sunday"; "Monday"; "Tuesday"; "Wednesday"; "Thursday"; "Friday";
    //    "Saturday"|]


module CreatingArrays =

    //  Example 4-11. Initializing arrays using Array.init

    open System

    // Intialize an aray of win-wav elements
    let divisions = 4.0
    let twoPi = 2.0 * Math.PI

    let sins = Array.init (int divisions) (fun i -> float i * twoPi / divisions)
    //val sins: float array = [|0.0; 1.570796327; 3.141592654; 4.71238898|]

    // Construct empty arrays
    let emptyIntArray: int []= Array.zeroCreate 3
    let emptyStringArray: string [] = Array.zeroCreate 3

    //val emptyIntArray: int array = [|0; 0; 0|]
    //val emptyStringArray: string array = [|null; null; null|]


module PatternMatching =

    // F# can match of values and structure

    // Example 4-12. Pattern matching against arrays
    let describeArray arr =
        match arr with
        | null              -> "The array is null"
        | [| |]             -> "The array is empty"
        | [| x |]           -> sprintf "The array has one element, %A" x
        | [| x; y |]    -> sprintf "The array has two elements, %A and %A" x y
        | a     -> sprintf "Thge array had %d elements, %A" a.Length a
    //val describeArray: arr: 'a array -> string

    let desc0 = describeArray null
    //val desc1: string = "Thge array had 4 elements, [|1; 2; 3; 4|]"

    let desc1 = describeArray [| |]
    //val desc1: string = "The array is empty"

    let desc2 = describeArray [| "one" |]
    //array has one element, "one"

    let desc3 = describeArray [| 1 .. 4 |]
    //val desc3: string = "Thge array had 4 elements, [|1; 2; 3; 4|]"


module ArrayEquality =

    // Arrays in F# are compared using structural equality.

    let bool1 = [| 1 .. 5 |] = [| 1; 2; 3; 4; 5 |]
    //val bool1: bool = true

    let bool2 = [| 1 ..3 |] = [| |]
    //val bool2: bool = false


module ArrayModuleFunctions =

    // partition

    // Simple Boolean function
    let isGreaterThanTen x = (x > 10)

    // Partioninf arrays
    let partitions1 =
        [| 5; 5; 6; 20; 1; 3; 7; 11 |]
        |> Array.partition isGreaterThanTen
    //tions1: int array * int array = ([|20; 11|], [|5; 5; 6; 1; 3; 7|])


    // tryFind and tryFindIndex

    // Simple Boolean function
    let rec isPowerOfTwo x =
        if x = 2 then true
        elif x % 2 = 1 then false
        else isPowerOfTwo (x / 2)

    let pows1 =
        [| 1; 7; 13; 64; 32 |]
        |> Array.tryFind isPowerOfTwo
    //val pows1: int option = Some 64

    let indx1 =
        [| 1; 7; 13; 64; 32 |]
        |> Array.tryFindIndex isPowerOfTwo
    //val indx1: int option = Some 3


    // Aggregate operators

    let vowels = [| 'a'; 'e'; 'i'; 'o'; 'u' |]

    let shat = Array.iteri (fun idx chr -> printfn $"vowel[{idx} = {chr}") vowels


module MultidimensionalArrays =

    let identityMatrix : float[,] = Array2D.zeroCreate 3 3
    identityMatrix.[0,0] <- 1.0
    identityMatrix.[1,1] <- 1.0
    identityMatrix.[2,2] <- 1.0

    //identityMatrix
    //val it: float array2d = [[1.0; 0.0; 0.0]
    //                         [0.0; 1.0; 0.0]
    //                         [0.0; 0.0; 1.0]]

    let mtx1 =
        identityMatrix[*, 1..2]

    //val mtx1: float array2d = [[0.0; 0.0]
    //                           [1.0; 0.0]
    //                           [0.0; 1.0]]
