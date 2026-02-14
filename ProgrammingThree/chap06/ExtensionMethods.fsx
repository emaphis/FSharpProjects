// Extension methods

module ExtensionMethods

//let fmt1 = (1094).ToHexString();;
//  error FS0039: The type 'Int32' does not define the field, constructor or member 'ToHexString'.

// Extend the System.Int32 AKA type
type System.Int32 with
    member this.ToHexString() = $"0x%x{this}"

// Now...
let fmt2 = (1094).ToHexString()
// val fmt2: string = "0x446"

