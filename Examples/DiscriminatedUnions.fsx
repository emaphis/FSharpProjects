(* F# - Discriminated Unions *)

type choice =
    | Yes
    | No

let x = Yes (* creates an instance of choice *)
let y = No (* creates another instance of choice *)

let main() =
    printfn $"x: %A{x}"
    printfn $"y: %A{y}"

main()


(* Example 1*)

type VoltageState =
    | High
    | Low

let toggleSwitch = function (* pattern matching input *)
    | High -> Low
    | Low -> High


let main1() =
    let on = High
    let off = Low
    let change = toggleSwitch off

    printfn $"Switch on state: %A{on}"
    printfn $"Switch off state: %A{off}"
    printfn $"Toggle off: %A{change}"
    printfn $"Toggle the Changed state: %A{toggleSwitch change}"

main1()


(* Example 2 *)

type Shape =
   // here we store the radius of a circle
   | Circle of float

   // here we store the side length.
   | Square of float

   // here we store the height and width.
   | Rectangle of float * float


let pi = 3.141592654

let area myShape =
   match myShape with
   | Circle radius -> pi * radius * radius
   | Square s -> s * s
   | Rectangle (h, w) -> h * w

let radius = 12.0
let myCircle = Circle(radius)
let area1 = area myCircle 
printfn $"Area of circle with radius %g{radius}: %g{area1}"

let side = 15.0
let mySquare = Square(side)
let area2 = area mySquare
printfn $"Area of square that has side %g{side}: %g{area2}"

let height, width = 5.0, 8.0
let myRectangle = Rectangle(height, width)
let area3 = area myRectangle
printfn $"Area of rectangle with height %g{height} and width %g{width} is %g{area3}"
