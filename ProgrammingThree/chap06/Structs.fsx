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

    /// Struct for describing a book. (automantically defined constructors
    [<Struct>]
    type BookStruct(title: string, pages: int) =
        member this.Title = title
        member this.Pages = pages

        override this.ToString() =
            $"Title: {this.Title}, Pages: {this.Pages}"

    // Create an instance of the struct using the constructor
    let book1 = BookStruct("Philosopher's stone", 309)

    // Create an instance using the defualt constructor
    let namelessBook = BookStruct()
    // val namelessBook: BookStruct = Title: , Pages: 0


    // To create a mutable struct you must explicitly declare each mutable field as a `val` aand add the 4
    // `[<DefaultValue>] attribute, then the instance of the strcuk must be declare mutable

    // Define a struct with mutable fields
    [<Struct>]
    type MPoint =
        val mutable X : int
        val mutable Y : int

        override this.ToString() =
            $"{{%d{this.X}, %d{this.Y}}}"


    let mutable pt1 = MPoint()

    // Update the fields
    pt1.X <- 10
    pt1.Y <- 7

    pt1
