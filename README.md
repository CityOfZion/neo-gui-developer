# Custom development Neo-gui
!!! EXPERIMENTAL DEVELOPMENT GUI

!!! ONLY USE ON TESTNET

This custom development Neo GUI is built on top of the official Neo GUI. It includes some additional features to make it easier to deploy Neo Smart contracts and get return/runtime events from invoking a smart contract.

Features:
1. Event log
  - Additional tab on the GUI which shows a list of events when any Neo smart contracts calls a Runtime.Log() function
  - Shows the local time when the event is received, block height, script info, and log message
  - Event log picks up events in both test invoke and blockchain invoke
  

2. Smart contract Return
  - Implemented an experimental feature to allow Neo Smart contract CheckWitness() authentication function to verify during test invoke
  - Custom GUI catches Neo smart contract byte[] (parameter 05) Return during test invoke (this is useful for functions returning information stored in Storage and true/false bool)
  - This function is activated as a messagebox popup during Test invoke
  - The return format is currently set as hexadecimal to show byte[] return, but also includes a UTF8 conversion of byte[]
  
  
3. Smart contract monitor
  - Script hash is now displayed in an Information box when a Smart contract is deployed during on DeployDialog
  - Optional smart contract monitor feature added to menu Advanced -> SC watchlist...
  - Smart contract watchlist shows the script info, Script hash, and status of the Neo smart contract on the blockchain
  - The watchlist automatically refreshes with the timer1 to check if the Neo Smart contract script hash can be found on the blockchain. Green "Found!" indicates the Neo Smart contract has been deployed successfully, red "Unavailable" indicates that it has not been deployed successfully.
  - Neo Smart contract Script hash is automatically added to the watchlist when it is deployed on DeployDialog or looked up on InvokeDialog
  - The script hash on the watchlist are saved/loaded on a smartcontract.txt in the Neo main folder. Script hash can be removed/added on this txt file.
