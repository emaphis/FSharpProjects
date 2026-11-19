module Tree

type Tree<'a> =
    | Tree of 'a * Tree<'a> * Tree<'a>
    | Leaf of 'a

module Algorithms =

    /// The in-order traversal algorithm
    let rec inOrder tree =
        seq {
          match tree with
              | Tree(x, left, right) ->
                   yield! inOrder left
                   yield x
                   yield! inOrder right
              | Leaf x -> yield x
        }


     /// pre-order
    let rec preOrder tree =
        seq {
          match tree with
              | Tree(x, left, right) ->
                   yield x
                   yield! preOrder left
                   yield! preOrder right
              | empty -> ()
        }

    /// post-order traversal
    let rec postOrder tree =
        seq {
          match tree with
              | Tree(x, left, right) ->
                   yield! postOrder left
                   yield! postOrder right
                   yield x
              | empty -> ()
        }


/// 
