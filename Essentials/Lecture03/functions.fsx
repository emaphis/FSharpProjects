// Lesson 3 - Functions

// Tupled

let add1 (x:int, y:int) : int = x + y

let add2 (x, y) = x + y

let add3 = fun (x, y) -> x + y


// Curried

let add4 (x: int) (y: int) : int = x + y

let add5 x y = x + y

let add6 = fun x y -> x + y


// Partial application

let addFive = add6 5
let assert1 = addFive 4  = 9


// Higher-Order

let calculate f a b = f a b

let assert2 = calculate add5 4 5  = 9

let assert3 = 12 = calculate (fun x y -> x * y) 3 4


let swap (x, y) = (y, x)

swap (1,2)

// Unit (void)

let write (msg: string) = System.Console.WriteLine(msg)

write ("message for you")

let now () = System.DateTime.UtcNow

now()


// Composition

let calc1 = add4 5 4 |> add4 3
