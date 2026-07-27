namespace AwesomeCollections

type 'a stack =
    | EmptyStack
    | StackNode of 'a * 'a stack

module Stack =
  val hd : 'a stack -> 'a
  val tl : 'a stack -> 'a stack
  val cons : 'a -> 'a stack -> 'a stack
  val empty : 'a stack
  val rev : 'a stack -> 'a stack


[<Class>]
type 'a Queue =
    member hd : 'a
    member tl : 'a Queue
    member enqueue : 'a -> 'a Queue
    static member empty : 'a Queue

[<Class>]
type BinaryTree<'a when 'a: comparison> =
    member hd : 'a
    member exists : 'a -> bool
    member insert : 'a -> 'a BinaryTree
    static member empty : 'a BinaryTree


[<Class>]
type 'a RBTree =
    member hd : 'a
    member left : 'a RBTree
    member right : 'a RBTree
    member exists : 'a -> bool
    member insert : 'a -> 'a RBTree
    member print : unit -> unit
    static member empty : 'a RBTree