// Exercise 6.1

 // 1. Using the function add defined in listing 6.2, create
 //    a function addTen, and test that this new function
 //    works as expected.
 
 // 2. Create a function multiply that works in a similar
 //    fashion to add.
 
 // 3. Create a function, addTenAndDouble, that takes in a
 //    single number. It should add 10 to it before and
 //    then double the answer.
 
 // 4. Create a dedicated timesTwo function based on
 //    multiply and use that in the addTenAndDouble
 //    function.

 let add a b = a + b

 // 1.
let addTen = add 10

let fifeteen = addTen 5

// 2.
let multiply a b = a * b

let ten = multiply 2 5

// 4.
let timesTwo a = multiply a 2

let twenty = timesTwo ten

// 3.
let addTenAndDouble a = timesTwo (addTen a)

let thirty = addTenAndDouble 5
