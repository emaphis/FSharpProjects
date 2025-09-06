// Chapter 2 - Fibonacci

//  A Fibonacci series is written as follows: 1, 1, 2, 3, 5, 8, 13 ..

/// Recursive fibonacci
let rec fibonacciRec n =
    if n <= 2 then 1
    else fibonacciRec (n - 1) + fibonacciRec (n - 2)

 
open System.Numerics

/// Tail-call fibonacci
let fibonacciTC n =
    let rec loop (n, x, y) =
        if (n = 0I) then x
        else loop ((n - 1I), y, (x + y))

    loop (n, 0I, 1I)



open System.Collections.Generic

let rec fibonacci_Generic n =
   let cache = Dictionary<_, _>()
   let rec fibonacciX = function 
       | n when n = 0I -> 0I
       | n when n = 1I -> 1I
       | n -> fibonacciX (n - 1I) + fibonacciX  (n - 2I)
   if cache.ContainsKey(n) then cache.[n]
   else
      let result = fibonacciX n
      cache.[n] <- result
      result
