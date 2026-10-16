(* F# - Loops *)

(* for...to and for...downto

Syntax

The syntax of a for to loop in F# programming language is −

for var = start-expr to end-expr do

   ... // loop body
The syntax of a for downto loop in F# programming language is −

for var = start-expr downto end-expr do
   ... // loop body

*)


(* Example 1 *)

let main1() =
    for i = 1 to 20 do
        printfn $"i: %i{i}"

main1()


(* Example 2 *)

let main2() =
    for i = 20 downto 1 do
        printfn $"i: {i}"

main2()


(* for...in loop

Syntax

for pattern in enumerable-expression do
   body-expression

*)


(* Example *)

// Looping over a list.

let list1 = [ 10; 25; 34; 45; 78 ]

for i in list1 do
    printfn $"%d{i}"


// Looping over a sequence

let seq1 = seq { for i in 1 .. 10 -> (i, i*i) }

for a, asqr in seq1 do
    printfn $"%d{a} squared is %d{asqr}"


(* while..do Loops

Syntax

while test-expression do
   body-expression

*)

(* Example *)

let mutable a = 10

while a < 20 do
    printfn $"value of a: %d{a}"
    a <- a + 1


(* Nested Loops

Syntax

The syntax for a nested for loop statement could be as follows −

for var1 = start-expr1 to end-expr1 do
   for var2 = start-expr2 to end-expr2 do
      ... // loop body

The syntax for a nested while loop statement could be as follows −

while test-expression1 do
   while test-expression2 do
      body-expression

*)


(* Example *)

let main3() =
    for i = 1 to 5 do
        printf "\n"
        for j = 1 to 3 do
            printf "*"
    printfn ""

main3()
