module Capstone2.Operations

open System
open Capstone2.Domain

let deposit amount account =
    { account with
          Balance = account.Balance + amount }

let withdraw amount account =
    if amount < account.Balance then
        { account with
              Balance = account.Balance - amount }
    else
        account

let auditAs operationName audit operation amount account =
    audit account (sprintf $"{DateTime.UtcNow}: Performing a {operationName} for ${amount}...")

    let updatedAccount = operation amount account
    let accountIsUnchanged = (updatedAccount = account)

    if accountIsUnchanged then
        audit account (sprintf $"{DateTime.UtcNow}: Insufficient Funds...Transaction rejected!")
    else
        audit account (sprintf $"{DateTime.UtcNow}: Transaction accepted! Balance is now ${account.Balance}")

    updatedAccount

let getAmount () =
    Console.Write "Amount: "
    Console.ReadLine() |> Decimal.Parse
