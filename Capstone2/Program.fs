module Capstone2.Program

open System
open Capstone2.Domain
open Capstone2.Operations
// Define a function to construct a message to print

[<EntryPoint>]
let main argv =
    let mutable account =
        let customer =
            Console.Write "Please enter your name: "
            let customerName = Console.ReadLine()
            { Name = customerName }

        Console.Write "Enter opening balance: "
        let balance = Console.ReadLine() |> Decimal.Parse

        { AccountId = Guid.NewGuid()
          Owner = customer
          Balance = balance }

    let withdrawWithAudit =
        withdraw
        |> auditAs "withdraw" Auditing.fileSystemAudit

    let depositWithAudit =
        deposit
        |> auditAs "deposit" Auditing.fileSystemAudit

    while true do
        let action =
            Console.WriteLine()
            printfn $"Current balance is ${account.Balance}"
            Console.Write "(d)eposit, (w)ithdraw or e(x)it: "
            Console.ReadLine()

        if action = "x" then Environment.Exit 0

        let processAction =
            function
            | "d" -> account |> depositWithAudit (getAmount ())
            | "w" -> account |> withdrawWithAudit (getAmount ())
            | _ -> account

        account <- processAction action

    0 // return an integer exit code
