// Arrays

let Philosophers = [| "Aquinas" ; "Alfarabi"; "Avicenna"; "Averroes"; "Maimonides"|]

let phi2 = Philosophers[2]
//val phi2: string = "Avicenna"


let phil0 = Philosophers[0]
//val phil0: string = "Aquinas"

// Generate an array
let CountTo100 = [| 1..100 |]

let countTo1000by100 = [| 1 .. 100 .. 1000|]
//  [|1; 101; 201; 301; 401; 501; 601; 701; 801; 901|]

// Array.zeroCreate allows you to create pre-populated arrays with zeroes or nulls
Array.zeroCreate
// val it: (int -> 'a array)

let array1 : int array = Array.zeroCreate 10
// val array1: int array = [|0; 0; 0; 0; 0; 0; 0; 0; 0; 0|]

Array.create
// val it: (int -> 'a -> 'a array)

let array2 = Array.create 10 5
// val array2: int array = [|5; 5; 5; 5; 5; 5; 5; 5; 5; 5|]

Array.init
// val it: (int -> (int -> 'a) -> 'a array)

// Array of cubes
let arrayOfCubes = (Array.init 10 (fun idx -> idx * idx))
// val arrayOfCubes: int array = [|0; 1; 4; 9; 16; 25; 36; 49; 64; 81|]

let senators : string[] = Array.zeroCreate 100
let houseReps : string[] = Array.zeroCreate 435
let originalColonies = Array.zeroCreate<string> 13

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



