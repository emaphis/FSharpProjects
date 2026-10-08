// Arrays - pg 112

// A basic array can be declared as follows:
let Philosophers = [| "Aquinas" ; "Alfarabi"; "Avicenna"; "Averroes"; "Maimonides"|]

// The individual elements of an array can be accessed with a
// x[index] suffix given as follows:
let phi2 = Philosophers[2]
//val phi2: string = "Avicenna"


let phil0 = Philosophers[0]
//val phil0: string = "Aquinas"

// The type of an array can be explicitly defined as follows:
let PhilosophersTyped : string [] =
   [| "Aquinas" ; "Alfarabi"; "Avicenna"; "Averroes"; "Maimonides"|]

// Generate an array
let CountTo100 = [| 1 .. 100 |]

let countTo1000by100 = [| 1 .. 100 .. 1000|]
//  [|1; 101; 201; 301; 401; 501; 601; 701; 801; 901|]

let reversedCount100 = [| 100 .. -1 .. 1|]


// Array.zeroCreate allows you to create pre-populated arrays with zeroes or nulls
Array.zeroCreate
// val it: (int -> 'a array)

let count = 10
let array1 : int array = Array.zeroCreate count
// val array1: int array = [|0; 0; 0; 0; 0; 0; 0; 0; 0; 0|]

Array.create
// val it: (int -> 'a -> 'a array)

let sixSixers =  Array.create 6 6
// val sixSixers: int array = [|6; 6; 6; 6; 6; 6|]

let zeroToSixty = [| 0.0 .. 4.5 .. 60.0 |]
//val zeroToSixty: float array =
//  [|0.0; 4.5; 9.0; 13.5; 18.0; 22.5; 27.0; 31.5; 36.0; 40.5; 45.0; 49.5; 54.0; 58.5|]

Array.init
// val it: (int -> (int -> 'a) -> 'a array)

// Array of cubes
let arrayOfCubes = Array.init 10 (fun idx -> idx * idx * idx)
// val arrayOfCubes: int array = [|0; 1; 8; 27; 64; 125; 216; 343; 512; 729|]

// An array can be declared in various ways
let senators : string[] = Array.zeroCreate 100
let houseReps : string[] = Array.zeroCreate 435
let originalColonies = Array.zeroCreate<string> 13


//  you can also access the array elements using user-defined sequences,
// also known as slice notation.
let imdbtop10 = [|
    "The Shawshank Redemption (1994)";
    "The Godfather (1972)";
    "The Godfather: Part II (1974)";
    "Il buono, il brutto, il cattivo. (1966)";
    "Pulp Fiction (1994)";
    "Inception (2010)";
    "Schindler's List (1993)";
    "12 Angry Men (1957)";
    "One Flew Over the Cuckoo's Nest (1975)";
    "The Dark Knight (2008)"
|]

let topThree = imdbtop10[1..3]
let topFive = imdbtop10[..5]
let bottomFive = imdbtop10[5..]
let list1 = imdbtop10[0..]

let wolfenstein3D = Array3D.zeroCreate<float> 11 11 11

wolfenstein3D[0, 0, 0] <- 1.1
