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
#import "EPPZ_Cloud_C.h"


// Settings keys.
NSString* NameKey = @"name"; // string
NSString* SoundKey = @"sound"; // bool
NSString* VolumeKey = @"volume"; // float

// Progress keys.
NSString* LevelKey = @"level"; // int
NSString* FirstTrophyKey = @"firstTrophy"; // bool
NSString* SecondTrophyKey = @"secondTrophy"; // bool
NSString* ThirdTrophyKey = @"thirdTrophy"; // bool


@interface ViewController : UIViewController <EPPZ_Cloud_Delegate>


@property (nonatomic, weak) IBOutlet UITextField* nameTextField;
@property (nonatomic, weak) IBOutlet UISwitch* soundSwitch;
@property (nonatomic, weak) IBOutlet UISlider* volumeSlider;

@property (nonatomic, weak) IBOutlet UISegmentedControl* levelSegmentedControl;
@property (nonatomic, weak) IBOutlet UISwitch* firstTrophySwitch;
@property (nonatomic, weak) IBOutlet UISwitch* secondTrophySwitch;
@property (nonatomic, weak) IBOutlet UISwitch* thirdTrophySwitch;

@property (nonatomic, weak) IBOutlet UISwitch* resolveConflictsSwitch;


-(IBAction)nameTextFieldEditingDidEndOnExit:(UITextField*) sender;
-(IBAction)soundSwitchValueChanged:(UISwitch*) sender;
-(IBAction)volumeSliderTouchedUp:(UISlider*) sender;
-(IBAction)levelSegmentedControlValueChanged:(UISegmentedControl*) sender;
-(IBAction)firstTrophySwitchValueChanged:(UISwitch*) sender;
-(IBAction)secondTrophySwitchValueChanged:(UISwitch*) sender;
-(IBAction)thirdTrophySwitchValueChanged:(UISwitch*) sender;


@end


// Helper.
#define LOG_METHOD NSLog(@"[%@ %@]", NSStringFromClass([self class]), NSStringFromSelector(_cmd))
