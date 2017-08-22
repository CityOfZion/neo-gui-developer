# Custom development Neo-gui
!!! EXPERIMENTAL DEVELOPMENT GUI

!!! ONLY USE ON TESTNET. __neo-gui-developer runs on testnet in default__

This custom development Neo GUI is built on top of the official Neo GUI. It includes some additional features to make it easier to deploy Neo Smart contracts and get return/runtime events from invoking a smart contract

## __18/08/2017 Update__
- Added libleveldb.dll, which is automatically copied to the appropriate directory during build

### Features:
__4. Add params function__
  - Implemented experimental Add params function to allow adding an optional params object[] during Invoke contract to pass an object[] parameter/argument. Add params button opens a new dialog with a listview to allow users to "build" an object[] array stack 
  - The listview index corresponds to the required array index (i.e. first item in list is arg[0] in object[] array). This function currently supports three types: *byte[]*, *int*, and *string*. Type is selected when adding an item in Add params with a dropdown list.
  - A Smart contract requires the correct number of arguments to be passed, otherwise it will fail. For example, a __FunctionCode Main(byte[] operation, object[])__ must have exactly 2 arguments, byte[] and object[], passed during Invoke. [Supported parameter types](https://github.com/CityOfZion/docs/blob/master/en-us/sc/tutorial/Parameter.md) can be set during Deploy and added with the usual Parameter Editor, while object[] can be not declared during Deploy but added with Add params functions __after__ declared parameters are set. In the above-mentioned example, Parameter List is set as "05" during Deploy Smart contract. Thereafter during Invoke, the byte[] value is first declared using the Parameter Editor and the object[] array is then declared using the Add params function. __An empty array has to be passed even if you do not require the object[] to be declated__, this can be done by clicking Add params -> Accept without adding anything to the stack.

__5. Crude support for Array type__
  - Support for Array type in Parameter Editor
  - Currently only supports byte[] type in the array i.e. byte\[][]
  - This option to pass an object[] type is to set "10" in Parameter List during Deploy Smart contract. That will add the parameter as an Array type in the Parameter Editor during Invoke, which can then be declared in the Parameter Editor as with other types
  - The stack can be built by writing each byte[] in a new line in the textbox, the second byte[] is written after a linebreak in a new line etc.
  
## Previous Features:
__1. Event log__
  - Additional tab on the GUI which shows a list of events when any Neo smart contracts calls a Runtime.Log() function
  - Shows the local time when the event is received, block height, script info, and log message
  - Event log picks up events in both test invoke and blockchain invoke
  

__2. Smart contract Return__
  - Implemented an experimental feature to allow Neo Smart contract CheckWitness() authentication function to verify during test invoke
  - Custom GUI catches Neo smart contract byte[] (parameter 05) Return during test invoke (this is useful for functions returning information stored in Storage and true/false bool)
  - This function is activated as a messagebox popup during Test invoke
  - The return format is currently set as hexadecimal to show byte[] return, but also includes a UTF8 conversion of byte[]
  
  
__3. Smart contract monitor__
  - Script hash is now displayed in an Information box when a Smart contract is deployed during on DeployDialog
  - Optional smart contract monitor feature added to menu Advanced -> SC watchlist...
  - Smart contract watchlist shows the script info, Script hash, and status of the Neo smart contract on the blockchain
  - The watchlist automatically refreshes with the timer1 to check if the Neo Smart contract script hash can be found on the blockchain. Green "Found!" indicates the Neo Smart contract has been deployed successfully, red "Unavailable" indicates that it has not been deployed successfully
  - Neo Smart contract Script hash is automatically added to the watchlist when it is deployed on DeployDialog or looked up on InvokeDialog
  - The script hash on the watchlist are saved/loaded on a smartcontract.txt in the Neo main folder. Script hash can be removed/added on this txt file
