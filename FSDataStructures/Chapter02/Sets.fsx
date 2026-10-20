// Sets and Maps

let androidVersions =
    Set.empty.Add("Cupcake").Add("Donut").Add("Eclair").Add("Froyo").Add("Gingerbread").Add("Honeycomb")
        .Add("Ice Cream Sandwich").Add("Jelly Bean").Add("KitKat")

// More additions
androidVersions.Add("Lollipop")


let bibTexBiblio =
    Map.empty
        .Add("agrawal1996fast", "Fast Discovery of Association Rules.")
        .Add("bell2009beyond", "Beyond the data deluge")
        .Add("Wooldridge2003", "Bayesian Belief Networks")
        .Add("Witten2005", "Data Mining: Practical machine learning tools and techniques")

let title1 = bibTexBiblio["Wooldridge2003"]
// val title1: string = "Bayesian Belief Networks"
