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
    let audit =
        audit account.AccountId account.Owner.Name

    audit (sprintf $"{DateTime.UtcNow}: Performing a {operationName} for ${amount}...")
    let updatedAccount = operation amount account

    let accountIsUnchanged = (updatedAccount = account)

    if accountIsUnchanged then
        audit (sprintf $"{DateTime.UtcNow}: Transaction rejected!")
    else
        audit (sprintf $"{DateTime.UtcNow}: Transaction accepted! Balance is now ${updatedAccount.Balance}")

    updatedAccount
