# Invoice Module
## Development Instruction
1. To clone source code from github: git clone https://github.com/SkightTeam/BW.Invoice.git
2. To set Inovice.UI as start up project if it is not yet.
3.

### Naming Convention
This project used a different naming convention than "standard" C#
* Removed `I` prefix in interface naming to make clean and meaningful codes
* Added `Impl` to the class of the interface if only a single implementation 
or specific provider name for multiple implementations. 

### Dependencies
#### Main Project's Dependencies
* Xero.Api.Core
* Automapper 8.0.0

#### Test Project's Dependencies
* Machine.Specifications 0.12.0
* Machine.Specifications.Should 0.11.0
* Machine.Specifications.Runner.VisualStudio 2.8.0
* Machine.Specifications.Fakes 2.10.0
* Machine.Specifications.Fakes.NSubstitute 2.10.0

Also need install Resharper extension if want to run test from Resharper: machine.specifications.runner.resharper## Technology Debt### Duplicated Code in Public/Parnter AuthenticatorThe two classes have lots duplicated code, which came from Xero.Api two base authenticators design. It can be improved by rewrite Xero.Api authenticators's code.### UserId improper passed in XeroApiThe UserID has been passed through XeroCoreApi down to XeroHttpClient, and finally used to look up token in Token Store.The whole process is complicated and hidden behavior. Need improve by rework on XeroApi itself.