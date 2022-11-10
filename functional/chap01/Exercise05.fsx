// 1.5 The sequence F0, F1, F2,... of Fibonacci numbers is defined by:
//      F0 = 0
//      F1 = 1
//      Fn = Fn−1 + Fn−2
// Thus, the first members of the sequence are 0, 1, 1, 2, 3, 5, 8, 13,....
// Declare an F# function to compute Fn. Use a declaration with three clauses, where the patterns
// correspond to the three cases of the above definition.
// Give an evaluations for F4.

let rec fibo = function
    | 0 -> 0
    | 1 -> 1
    | n -> fibo (n - 1) + fibo (n - 2)

// testing
printf "%b\n" (0 = fibo 0)
printf "%b\n" (1 = fibo 1)
printf "%b\n" (1 = fibo 2)
printf "%b\n" (2 = fibo 3)
printf "%b\n" (3 = fibo 4)
printf "%b\n" (5 = fibo 5)
printf "%b\n" (8 = fibo 6)
printf "%b\n" (13 = fibo 7)
