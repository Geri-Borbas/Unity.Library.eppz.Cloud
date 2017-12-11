//
// Copyright (c) 2017 eppz! mobile, Gergely Borbás (SP)
//
// http://www.twitter.com/_eppz
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED,
// INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A
// PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT
// HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE
// OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
//

#import <UIKit/UIKit.h>
#import "EPPZ_Cloud_C++.h"


@interface ViewController : UIViewController <EPPZ_Cloud_Delegate>


@property (nonatomic, strong) IBOutlet UISwitch *unlockSwitch;
@property (nonatomic, strong) IBOutlet UISwitch *hintSwitch;
@property (nonatomic, strong) IBOutlet UISwitch *solveSwitch;

-(IBAction)initializeButtonTouchedUp:(id)sender;
-(IBAction)unlockSwitchDidChange:(id) sender;
-(IBAction)hintSwitchDidChange:(id) sender;
-(IBAction)solveSwitchDidChange:(id) sender;


@end
