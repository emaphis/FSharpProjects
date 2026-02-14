module Structs

    // Have many features of classes but are values and
    // are stored on the stack.

module CreatingStructs =

    [<Struct>]
    type StructPoint(x: int, y: int) =
        member this.X = x
        member this.Y = y

    type StructRect(top: int, bottom: int, left: int, right: int) =
        struct
            member this.Top = top
            member this.Bottom = bottom
            member this.Left = left
            member this.Right = right

            override this.ToString() =
                $"[%d{top}, %d{bottom}, %d{left}, %d{right}]"
        end


    // Define two different struct values
    let x = StructPoint(6, 20)
    let y = StructPoint(6, 20)

    let bool1 = x = y
    // val bool1: bool = true

    // Automatic constructor that assigns each field a default value

    /// Struct for describing a book.
    [<Struct>]
    type BookStructure(title: string, pages: int) =
        member this.Title = title
        member this.Pages = pages

        override this.ToString() =
            $"Title: {this.Title}, Pages: {this.Pages}"
