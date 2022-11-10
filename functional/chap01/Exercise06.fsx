// 1.6 Declare a recursive function sum: int * int -> int, where
//      sum(m, n) = m + (m + 1) + (m + 2) + ··· + (m + (n − 1)) + (m + n)
// for m ≥ 0 and n ≥ 0. (Hint: use two clauses with (m,0) and (m,n) as patterns.)
// Give the recursion formula corresponding to the declaration.

let rec sum = function
    | (m, 0) -> m
    | (m, n) -> (m + n) + sum (m, n-1)

printf "%b\n" (0 = sum (0,0))
printf "%b\n" (3 = sum (1,1))
printf "%b\n" (9 = sum (2,2))
printf "%b\n" (12 = sum (3,2))
printf "%b\n" (27 = sum (2,5))
