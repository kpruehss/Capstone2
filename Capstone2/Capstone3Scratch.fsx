#load "Domain.fs"

let openingAccount =
    { Owner = { Name = "Karsten" }
      Balance = 0M
      AccountId = Guid.Empty }

let account =
    let commands = [ 'd'; 'w'; 'z'; 'f'; 'd'; 'x'; 'w' ]

    commands
    |> Seq.filter isValidCommand
    |> Seq.takeWhile (not << isStopCommand)
    |> Seq.map getAmount
    |> Seq.fold processCommand openingAccount
