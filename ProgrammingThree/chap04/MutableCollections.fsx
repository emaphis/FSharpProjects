module MutableCollections



module ListT =

    // Create a List<_> of planetset
    open System.Collections.Generic

    let planets = List<string>()

    // Add individual planets
    planets.Add("Mercury")
    planets.Add("Venus")
    planets.Add("Earth")
    planets.Add("Mars")

    let cnt = planets.Count
    //val cnt: int = 4

    // Add a collection of values at once
    planets.AddRange(  [| "Jupiter"; "Saturn"; "Uranus"; "Neptune"; "Pluto" |] )

    let cnt2 = planets.Count
    //val cnt2: int = 9


    // Sorry Pluto!
    planets.Remove("Pluto")
    //val it: bool = true

    let cnt3 = planets.Count
    //val cnt3: int = 8


module Dictionary =

    // Using a dictionary.

    // Atomic Mass Units.
    [<Measure>]
    type amu

    type Atom = { Name : string; Weight : float<amu> }

    open System.Collections.Generic
    let periodicTable = Dictionary<string, Atom>()

    periodicTable.Add( "H", { Name = "Hydrogen";  Weight = 1.0079<amu> })
    periodicTable.Add("He", { Name = "Helium";    Weight = 4.0026<amu> })
    periodicTable.Add("Li", { Name = "Lithium";   Weight = 6.9410<amu> })
    periodicTable.Add("Be", { Name = "Beryllium"; Weight = 9.0122<amu> })
    periodicTable.Add( "B", { Name = "Boron ";    Weight = 10.811<amu> })


    /// Lookup an element
    let printElement name =
        if periodicTable.ContainsKey(name) then
            let atom = periodicTable[name]
            printfn"Atom with symbol with '%s' has weight %A" atom.Name atom.Weight
        else
            printfn "Error. No atom with name '%s' found." name

    /// Alternate syntax to get a value. Return a tuple of 'success * result'
    let printElement2 name =
        let (found, atom) = periodicTable.TryGetValue(name)
        if found then
            printfn "Atom with symbol with '%s' has weight %A." atom.Name atom.Weight
        else
            printfn "Error. No atom with name '%s' found." name


    do printElement "He"
    // Atom with symbol with 'Helium' has weight 4.0026

    do printElement2 "Be"
    //Atom with symbol with 'Beryllium' has weight 9.0122.


module Hashset =

    // Unordered collection with fast lookup

    open System.Collections.Generic

    let bestPicture = new HashSet<string>()

    bestPicture.Add("The Artist")
    bestPicture.Add("The King's Speech")
    bestPicture.Add("The Hurt Locker")
    bestPicture.Add("Slumdog Milionaire")
    bestPicture.Add("No Country for Old Men")
    bestPicture.Add("The Departed")

    // Check if it was best picture
    if bestPicture.Contains("Manos: The Hands of Fate") then
        printfn "Sweet."
    else
        printfn "Oh no."


    if bestPicture.Contains("No Country for Old Men") then
        printfn "Sweet."
    else
        printfn "Oh no."
