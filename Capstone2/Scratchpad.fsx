#load "Domain.fs"

open Capstone2.Domain
open System
open System.IO

let customer = { Name = "Karsten Pruehss" }

let account =
    { AccountId = Guid.Empty
      Owner = customer
      Balance = 90M }

let deposit amount account =
    { account with
          Balance = account.Balance + amount }

let withdraw amount account =
    if amount < account.Balance then
        { account with
              Balance = account.Balance - amount }
    else
        account

let consoleAudit account message =
    printfn $"Account {account.AccountId}: {message}"

let fileSystemAudit account message =
    Directory.CreateDirectory(sprintf @"./Logs/%s" account.Owner.Name)
    |> ignore

    let filePath =
        sprintf @"./Logs/%s/%O.txt" account.Owner.Name account.AccountId

    File.AppendAllLines(filePath, [ message ])

let auditAs operationName audit operation amount account =
    audit account (sprintf $"{DateTime.UtcNow}: Performing a {operationName} for ${amount}...")

    let updatedAccount = operation amount account
    let accountIsUnchanged = (updatedAccount = account)

    if accountIsUnchanged then
        audit account (sprintf $"{DateTime.UtcNow}: Insufficient Funds...Transaction rejected!")
    else
        audit account (sprintf $"{DateTime.UtcNow}: Transaction accepted! Balance is now ${account.Balance}")

    updatedAccount
