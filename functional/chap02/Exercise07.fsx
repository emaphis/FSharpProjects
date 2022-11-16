// 2.7  1. Declare the F# function test: int * int * int -> bool. The value of test(a, b, c),
//         for a ≤ b, is the truth value of:
//
//                 notDivisible(a, c)
//             and notDivisible(a + 1, c)
//             .
//             .
//             .
//             and notDivisible(b, c)
//
//     2. Declare an F# function prime: int -> bool, where prime(n) = true, if and only if n
//        is a prime number.
//     3. Declare an F# function nextPrime: int -> int, where nextPrime(n) is the smallest
//        prime number > n.

// TODO finish this.


let notDivisible (d, n) = n % d <> 0

let rec test (a, b, c) =
    if a > b then false
    else  if a < b then notDivisible(a, c) && test(a+1, b, c)
    else notDivisible(a, c) 

