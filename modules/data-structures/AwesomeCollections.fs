namespace AwesomeCollections

type 'a stack =
    | EmptyStack
    | StackNode of 'a * 'a stack


module Stack =

    let hd = function
        | EmptyStack -> failwith "Empty stack"
        | StackNode(hd, _) -> hd

    let tl = function
        | EmptyStack -> failwith "Empty stack"
        | StackNode(_, tl) -> tl

    let cons hd tl = StackNode(hd, tl)

    let empty = EmptyStack

    let rec update index value stk =
        match index, stk with
        | index, EmptyStack -> failwith "Index out of range"
        | 0, StackNode(hd, tl) -> StackNode(value, tl)
        | n, StackNode(hd, tl) -> StackNode(hd, update (index - 1) value tl)

    let rec append x y =
        match x with
        | EmptyStack -> y
        | StackNode(hd, tl) -> StackNode(hd, append tl y)

    let rec map fn = function
        | EmptyStack -> EmptyStack
        | StackNode(hd, tl) -> StackNode(fn hd, map fn tl)

    let rev stk =
        let rec loop acc = function
            | EmptyStack -> acc
            | StackNode(hd, tl) -> loop (StackNode(hd, acc)) tl
        loop EmptyStack stk

    let rec contains x = function
        | EmptyStack -> false
        | StackNode(hd, tl) -> hd = x || contains x tl

    let rec fold fn seed = function
        | EmptyStack -> seed
        | StackNode(hd, tl) -> fold fn (fn seed hd) tl


type Queue<'a>(f: stack<'a>, r: stack<'a>) =
    let check = function
        | EmptyStack, r -> Queue(Stack.rev r, EmptyStack)
        | f, r -> Queue(f, r)

    member this.hd =
        match f with
        | EmptyStack -> failwith "empty"
        | StackNode(hd, tl) -> hd

    member this.tl =
         match f, r with
         | EmptyStack, _ -> failwith "empty"
         | StackNode(x, f), r -> check(f, r)

    member this.enqueue x = check(f, StackNode(x, r))

    override this.ToString() = sprintf $"%A{f}"

    static member empty = Queue<'a>(Stack.empty, Stack.empty)



type 'a tree =
    | EmptyTree
    | TreeNode of 'a * 'a tree * 'a tree

module Tree =
    let hd = function
        | EmptyTree -> failwith "empty"
        | TreeNode(hd, l, r) -> hd

    let rec exists item = function
        | EmptyTree -> false
        | TreeNode(hd, l, r) ->
            if hd = item then true
            elif item < hd then exists item l
            else exists item r


    let rec insert item = function
        | EmptyTree -> TreeNode(item, EmptyTree, EmptyTree)
        | TreeNode(hd, l, r) as node ->
            if hd = item then node
            elif item < hd then TreeNode(hd, insert item l, r)
            else TreeNode(hd, l, insert item r)



type BinaryTree(inner: tree<'a>) =
    member this.hd = Tree.hd inner

    member this.exists item = Tree.exists item inner

    member this.insert item = BinaryTree(Tree.insert item inner)

    static member empty = BinaryTree(EmptyTree)



type color = R | B
type 'a clTree =
    | E
    | T of color * 'a clTree * 'a * 'a clTree


module RedBlackTree =
    let hd = function
        | E  -> failwith "empty"
        | T(c, l, x, r) -> x

    let left = function
        | E -> failwith "empty"
        | T(c, l, x, r) -> l

    let right = function
        | E -> failwith "empty"
        | T(c, l, x, r) -> r 

    let rec exists item = function
        | E -> false
        | T(c, l, x, r) ->
            if item = x then true
            elif item < x then exists item l
            else exists item r

    let balance = function              // Red nodes in relation to black root
        | B, T(R, T(R, a, x, b), y, c), z, d    // Left, left
        | B, T(R, a, x, T(R, b, y, c)), z, d    // Left, right
        | B, a, x, T(R, T(R, b, y, c), z, d)    // Right, left
        | B, a, x, T(R, b, y, T(R, c, z, d)) -> // Right, right
            T(R, T(B, a, x, b), y, T(B, c, z, d))
        | c, l, x, r -> T(c, l, x, r)


