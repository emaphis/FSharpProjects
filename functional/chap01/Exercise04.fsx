// 1.4 Declare a recursive function f: int -> int, where
//
//      f(n)=1+2+ ··· + (n − 1) + n
//
// for n ≥ 0. (Hint: use two clauses with 0 and n as patterns.)
// State the recursion formula corresponding to the declaration.
// Give an evaluation for f(4).

let rec f = function
    | 0 -> 0
    | n -> n + f (n-1)

f 4
4 + f (4-1)
4 + 3 + f (3-1)
4 + 3 + 2 + f(2-1)
4 + 3 + 2 + 1 + f(1-1)
4 + 3 + 2 + 1 + 0
10

// testing
printf "%b\n" (1 = f 1)
printf "%b\n" (3 = f 2)
printf "%b\n" (6 = f 3)
printf "%b\n" (10 = f 4)
printf "%b\n" (55 = f 10)
