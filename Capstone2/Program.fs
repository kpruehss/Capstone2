module Capstone2.Program

open System
open Capstone2.Domain
open Capstone2.Operations
// Define a function to construct a message to print

[<EntryPoint>]
let main argv =
    let name =
        Console.Write "Please enter your name: "
        Console.ReadLine()

    let withdrawWithAudit =
        auditAs "withdraw" Auditing.composedLogger withdraw

    let depositWithAudit =
        auditAs "deposit" Auditing.composedLogger deposit

    let openingAccount =
        { Owner = { Name = name }
          Balance = 0M
          AccountId = Guid.Empty }

    let closingAccount =
        // Fill in the main loop here...
        openingAccount

    Console.Clear()
    printfn $"Closing Balance:\r\n {closingAccount}"
    Console.ReadKey() |> ignore

    0 // return an integer exit code
