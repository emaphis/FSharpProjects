module Tests

open Modeling
open Xunit
open FsUnit.Xunit

module ``Given an Eligible Customer `` =

    [<Fact>]
    let ``Who spends a suitable amount then they get a discount`` () =
        let john = Registered ("John", true)
        let assertJohn = Customer.calculateTotal(john, 100.0m) = 90.0m
        assertJohn |> should equal true

    [<Fact>]
    let ``Who doesn't spend a sutiable amount so doesn't get a discount`` () =
        let mary = Registered ("Mary",  true)
        let assertMary = Customer.calculateTotal(mary, 99.0M) = 99.0M
        assertMary |> should equal true


module ``Given an Ineligible Customer`` =
    [<Fact>]
    let ``Doesn't get the discount even if they spent enough and are Registered`` () =
        let richard = Registered ("Richard", false)
        let assertRichard = Customer.calculateTotal(richard, 100.0M) = 100.0M
        assertRichard |> should equal true

    [<Fact>]
    let ``Doesn't get the discount if Unregistered`` () =
        let sarah= Unregistered "Sarah"
        let assertSarah = Customer.calculateTotal(sarah, 100.0M) = 100.0M
        assertSarah |> should equal true
