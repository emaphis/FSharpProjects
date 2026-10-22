module Stack.Tests

open Stack.Stack
open NUnit.Framework

[<TestFixture>]
type TestStackClass() =
    
    [<Test>]
    member this.validateStack() =
        let stack = Stack<string>()
        stack.Push "Test"
        let result = stack.Pop
        Assert.That("Test", Is.EqualTo(result))
