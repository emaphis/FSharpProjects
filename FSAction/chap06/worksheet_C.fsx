// 6.2.2 Modules

// Listing 6.8 Working with modules in F#

module FsInAction.MyMaths

let add x y = x + y
let subract x y = x - y

module Complicated =
    open System

    let ten = 10
    let addTogetherThenSubtractTen y x = add x y |> subract 10


//  6.2.3 Moving from script to
//   application: A step-by-step
//   exercise

// Most of the source code is in the "drivingapp" project
// The original code is in the `exercise_6_2.fsx` file


