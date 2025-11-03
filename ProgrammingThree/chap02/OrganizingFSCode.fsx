

// Organizing FSharp Code.

// modules

(*
module Alpha

// Refer to this as Alpha.x
let x = 1
*)


// Nested modules
// See Utilities.fs

#load "Utilities.fs"

open Utilities

let ten = ConversionUtils.intToString 10
//val ten: string = "10"

let oct = ConvertBase.convertToOct 10
//val oct: string = "12"

let hex = ConvertBase.convertToHex 10
//val hex: string = "a"



// Namespaces

// PlayingCars.fs

#load "PlayingCards.fs"

open PlayingCards

let ace = Ace Spade
let Queen = Queen Diamond
let three = ValueCard(3, Heart)


open PlayingCards.Poker

let playerGeorge = { Name = "George"; Money = 100; Position = 3 }
