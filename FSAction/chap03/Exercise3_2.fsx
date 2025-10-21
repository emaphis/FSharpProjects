// Exercise 3.2

// Complete the following steps to see some basic type inference
//  in action:
 
//  1. Enter the code from listing 3.4 into an empty F#
//     script file

//  2. Observe that the CodeLens correctly identifies the
//     type signature of the add function.

//  3. Remove the return type annotation (: int) so that
//     the function declaration is
//     let add (a:int) (b:int) =.

//   4. Observe that the CodeLens still correctly indicates
//      that the function returns an integer.

//   5. Remove the type annotation from answer.

//   6. Observe that the CodeLens still correctly
//      understands that the function returns an integer

//   7. Remove the type annotation from b (you can also
//      remove the parenthesis around it).

//   8. Observe that the CodeLens still correctly
//      understands that b is an integer.

//    9. Remove the type annotation from a (you can also
//       remove the parenthesis around it).

//    10. Observe that the CodeLens still correctly
//        understands that a is an integer.

// Original
let add0 (a: int) (b: int) : int =
    let answer: int  = a + b
    answer

let aa0 = add0 3 4
do printfn "%d" aa0

// Modified
let add a b =
    let answer = a + b
    answer

let aa = add 3 4
do printfn "%d" aa
