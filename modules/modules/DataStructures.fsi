// Object-Oriented Programming
// Modules and Namespaces

// Module Signatures

//module DataStructures

namespace Princess.Collections

type 'a stack =
    | EmptyStack
    | StackNode of 'a * 'a stack

module Stack =
    val getRange : int -> int -> int stack
    val hd : 'a stack -> 'a
    val tl : 'a stack -> 'a stack
    val fold : ('a -> 'b -> 'a) -> 'a -> 'b stack -> 'a
    val reduce : ('a -> 'a -> 'a) -> 'a stack -> 'a
