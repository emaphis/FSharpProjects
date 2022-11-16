// 2.9 Consider the declaration:
//
//         let rec f = function
//             | (0,y) -> y
//             | (x,y) -> f(x-1, x*y);;
//
//     1. Determine the type of f.
//     2. For which arguments does the evaluation of f terminate?
//     3. Write the evaluation steps for f(2,3).
//     4. What is the mathematical meaning of f(x, y)?


// 1. type
// f int * int -> int

// 2. For which arguments does the evaluation of f terminate?
// x >= 0

// 3. Write the evaluation steps for f(2,3).

let rec f = function
    | (0,y) -> y
    | (x,y) -> f(x-1, x*y)

6 = f(2,3)
f(2-1, 2*3)
f(1,6)
f(1-1, 1*6)
f(0, 6)
6

// 4. What is the mathematical meaning of f(x, y)?
// factorial?
