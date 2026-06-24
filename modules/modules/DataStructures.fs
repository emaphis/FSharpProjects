// Object-Oriented Programming
// Modules and Namespaces

// Defining Modules

//module DataStructures

namespace Princess.Collections

type 'a stack =
    | EmptyStack
    | StackNode of 'a * 'a stack


module Stack =
    // helper functions

    let internal_head_tail = function
        | EmptyStack -> failwith "Empty stack"
        | StackNode(hd, tail) -> hd, tail

    let rec internal_fold_left f acc = function
        | EmptyStack -> acc
        | StackNode(hd, tail) -> internal_fold_left f (f acc hd) tail

    // public functions

    let rec getRange startNum endNum =
        if startNum > endNum then EmptyStack
        else StackNode(startNum, getRange (startNum + 1) endNum)

    let hd s = internal_head_tail s |> fst
    let tl s = internal_head_tail s |> snd
    let fold f seed stack = internal_fold_left f seed stack
    let reduce f stack = internal_fold_left f (hd stack) (tl stack)
