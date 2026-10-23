// Chapter 5 - Stacks

// Pattern matching


let PickProgrammingParadigm (x: int) =
    match x with
    | 1 -> "Imperative"
    | 2 -> "Procedural"
    | 3 -> "Declarative"
    | 4 -> "Functional"
    | 5 -> "Object Oriented"
    | 6 -> "Event Driven"
    | 7 -> "Automata Based"
    | _ -> x.ToString()


PickProgrammingParadigm -1
// val it: string = "-1"

PickProgrammingParadigm 4
// val it: string = "Functional"

// Pattern matching with arrays

let MatchArray arr =
    match arr with
    | [| |] -> "An Empty Array"
    | [| x |] -> $"Single Value: %A{x}"
    | [| x; y |] -> sprintf $"A Pair: %A{x} and %A{y}"
    | _ -> ">2 Array"

MatchArray [| 1; 2; 3|]

// Pattern matching with Lists

let MatchList list =
    match list with
    | [] -> "An Empty List"
    | head::tail -> sprintf $"List %A{head} has %i{tail.Length} more elements"

// Testing

#load "Stack.fs"

open Stack.Stack

let stack = Stack<string>()

//stack.ToString()

stack.Push "Syntactic"
stack.Push "sugar"
stack.Push "causes"
stack.Push "cancer"
stack.Push "of"
stack.Push  "semicolon"

stack.Pop
//val it: string = "semicolon"
stack.Pop
//val it: string = "of"
stack.Pop
//val it: string = "cancer"
stack.Pop
//val it: string = "causes"
stack.Pop
//val it: string = "sugar"
stack.Pop
//val it: string = "Syntactic"
//stack.Pop
//System.Exception: Stack is Empty


let stack2 = Stack<string>()

stack2.Push "hello"
stack2.Push "world"

stack2.Pop
//val it: string = "world"
stack2.Pop
//val it: string = "hello"
//stack2.Pop
//System.Exception: Stack is Empty

stack2.TryPop
//val it: string option = None


// Stack with concurrency support

lock
//val it: ('a -> (unit -> 'b) -> 'b) when 'a: not struct


// Algorithm – parenthesis matching using stacks

//(8 + 3) * (6 + 9) / (1 -  1)

let BalanceExpression expr =
    let rec loop xs stack =
        match xs, stack with
        | [], [] -> true
        | [], _ -> false
        | '(' :: ys, stack -> loop ys ('(' :: stack)
        | ')' :: ys, '(' :: stack -> loop ys stack
        | ')' :: _, _ -> false
        | _ :: ys, stack -> loop ys stack
    loop (Seq.toList expr) []

BalanceExpression "(())"

BalanceExpression "(((( )))"

BalanceExpression "(8 + 3) * (6 + 9) / (1 -  1)"
