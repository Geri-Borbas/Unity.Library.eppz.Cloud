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

#import "ViewController.h"


extern void UnitySendMessage(const char* gameObjectName, const char* methodName, const char* message)
{ }


@interface ViewController ()
@property (nonatomic, strong) NSDictionary* uiUpdatingOnChangeActionsForKeys;
@property (nonatomic, strong) NSDictionary* conflictResolvingOnChangeActionsForKeys;
@end


@implementation ViewController


-(void)viewDidAppear:(BOOL)animated
{
    [super viewDidAppear:animated];
    [self initializeCloud];
}

#pragma mark - iCloud

-(void)initializeCloud
{
    LOG_METHOD;
    
    // Updating logic.
    [self bindOnChangeActions];
    
    // Initialize plugin.
    [EPPZ_Cloud setDelegate:self];
    _EPPZ_Cloud_InitializeWithGameObjectName(@"eppz! Cloud");
    _EPPZ_Cloud_Synchronize();
    
    // Setup UI.
    [self populateUI];
    [self setControlsEnabled:YES];
}

-(void)populateUI
{
    self.nameTextField.text = _EPPZ_Cloud_StringForKey(NameKey);
    [self.soundSwitch setOn:_EPPZ_Cloud_BoolForKey(SoundKey)];
    self.volumeSlider.value = _EPPZ_Cloud_FloatForKey(VolumeKey);
    self.levelSegmentedControl.selectedSegmentIndex = _EPPZ_Cloud_IntForKey(LevelKey);
    [self.firstTrophySwitch setOn:_EPPZ_Cloud_BoolForKey(FirstTrophyKey)];
    [self.secondTrophySwitch setOn:_EPPZ_Cloud_BoolForKey(FirstTrophyKey)];
    [self.thirdTrophySwitch setOn:_EPPZ_Cloud_BoolForKey(FirstTrophyKey)];
}

-(void)bindOnChangeActions
{
    self.uiUpdatingOnChangeActionsForKeys = @{
        NameKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", NameKey);
            [self.nameTextField setText:_EPPZ_Cloud_StringForKey(NameKey)];
        },
        SoundKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", SoundKey);
            [self.soundSwitch setOn:_EPPZ_Cloud_BoolForKey(SoundKey) animated:YES];
        },
        VolumeKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", VolumeKey);
            [self.volumeSlider setValue:_EPPZ_Cloud_FloatForKey(VolumeKey) animated:YES];
        },
        LevelKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", LevelKey);
            [self.levelSegmentedControl setSelectedSegmentIndex:_EPPZ_Cloud_IntForKey(LevelKey)];
        },
        FirstTrophyKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", FirstTrophyKey);
            [self.firstTrophySwitch setOn:_EPPZ_Cloud_BoolForKey(FirstTrophyKey) animated:YES];
        },
        SecondTrophyKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", SecondTrophyKey);
            [self.secondTrophySwitch setOn:_EPPZ_Cloud_BoolForKey(SecondTrophyKey) animated:YES];
        },
        ThirdTrophyKey :
        ^{
            NSLog(@"ViewController.uiUpdatingOnChangeActionsForKeys[\"%@\"]()", ThirdTrophyKey);
            [self.thirdTrophySwitch setOn:_EPPZ_Cloud_BoolForKey(ThirdTrophyKey) animated:YES];
        }
    };
    
    self.conflictResolvingOnChangeActionsForKeys = @{
        LevelKey :
        ^{
            NSLog(@"ViewController.conflictResolvingOnChangeActionsForKeys[\"%@\"]()", LevelKey);

            int remoteValue = _EPPZ_Cloud_IntForKey(LevelKey);
            int localValue = (int)self.levelSegmentedControl.selectedSegmentIndex;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                int resolvedValue = MAX(remoteValue, localValue); // Resolving strategy
                _EPPZ_Cloud_SetIntForKey(resolvedValue, LevelKey);
                _EPPZ_Cloud_Synchronize();
            }
        },
        FirstTrophyKey :
        ^{
            NSLog(@"ViewController.conflictResolvingOnChangeActionsForKeys[\"%@\"]()", FirstTrophyKey);

            BOOL remoteValue = _EPPZ_Cloud_BoolForKey(FirstTrophyKey);
            BOOL localValue = self.firstTrophySwitch.isOn;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                BOOL resolvedValue = (remoteValue || localValue); // Resolving strategy
                _EPPZ_Cloud_SetBoolForKey(resolvedValue, FirstTrophyKey);
                _EPPZ_Cloud_Synchronize();
            }
        },
        SecondTrophyKey :
        ^{
            NSLog(@"ViewController.conflictResolvingOnChangeActionsForKeys[\"%@\"]()", SecondTrophyKey);

            BOOL remoteValue = _EPPZ_Cloud_BoolForKey(SecondTrophyKey);
            BOOL localValue = self.secondTrophySwitch.isOn;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                BOOL resolvedValue = (remoteValue || localValue); // Resolving strategy
                _EPPZ_Cloud_SetBoolForKey(resolvedValue, SecondTrophyKey);
                _EPPZ_Cloud_Synchronize();
            }
        },
        ThirdTrophyKey :
        ^{
            NSLog(@"ViewController.conflictResolvingOnChangeActionsForKeys[\"%@\"]()", ThirdTrophyKey);

            BOOL remoteValue = _EPPZ_Cloud_BoolForKey(ThirdTrophyKey);
            BOOL localValue = self.thirdTrophySwitch.isOn;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                BOOL resolvedValue = (remoteValue || localValue); // Resolving strategy
                _EPPZ_Cloud_SetBoolForKey(resolvedValue, ThirdTrophyKey);
                _EPPZ_Cloud_Synchronize();
            }
        }
    };
}

-(void)cloudDidChange:(NSString*) userInfoJSON
{
    LOG_METHOD;
    
    NSError* error;
    NSData* JSONData = [userInfoJSON dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary* userInfo = [NSJSONSerialization JSONObjectWithData:JSONData
                                                             options:NSJSONReadingMutableContainers
                                                               error:&error];
    
    NSLog(@"userInfo[NSUbiquitousKeyValueStoreChangedKeysKey]: %@", userInfo[NSUbiquitousKeyValueStoreChangedKeysKey]);
    
    // Invoke appropriate action for each value changed.
    for (NSString* eachChangedKey in userInfo[NSUbiquitousKeyValueStoreChangedKeysKey])
    {
        // Invoke conflict resolving action if any (and if requested).
        if (self.resolveConflictsSwitch.isOn &&
            [self.conflictResolvingOnChangeActionsForKeys.allKeys containsObject:eachChangedKey])
        {
            void (^eachBlock)(void) = self.conflictResolvingOnChangeActionsForKeys[eachChangedKey];
            eachBlock();
        }

        // Invoke UI updating action if any.
        if ([self.uiUpdatingOnChangeActionsForKeys.allKeys containsObject:eachChangedKey])
        {
            void (^eachBlock)(void) = self.uiUpdatingOnChangeActionsForKeys[eachChangedKey];
            eachBlock();
        }
    }
}


#pragma mark - UI

-(void)viewDidLoad
{
    [super viewDidLoad];
    [self setControlsEnabled:NO];
}

-(IBAction)nameTextFieldEditingDidEndOnExit:(UITextField*) sender
{
    LOG_METHOD;
    [sender resignFirstResponder]; // Hide keyboard
    _EPPZ_Cloud_SetStringForKey(sender.text, NameKey);
    _EPPZ_Cloud_Synchronize();
}

-(IBAction)soundSwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    _EPPZ_Cloud_SetBoolForKey(sender.isOn, SoundKey);
    _EPPZ_Cloud_Synchronize();
}

-(IBAction)volumeSliderTouchedUp:(UISlider*) sender
{
    LOG_METHOD;
    _EPPZ_Cloud_SetFloatForKey(sender.value, VolumeKey);
    _EPPZ_Cloud_Synchronize();
}

-(IBAction)levelSegmentedControlValueChanged:(UISegmentedControl*) sender
{
    LOG_METHOD;
    _EPPZ_Cloud_SetIntForKey((int)sender.selectedSegmentIndex, LevelKey);
    _EPPZ_Cloud_Synchronize();
}

-(IBAction)firstTrophySwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    _EPPZ_Cloud_SetBoolForKey(sender.isOn, FirstTrophyKey);
    _EPPZ_Cloud_Synchronize();
}

-(IBAction)secondTrophySwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    _EPPZ_Cloud_SetBoolForKey(sender.isOn, SecondTrophyKey);
    _EPPZ_Cloud_Synchronize();
}

-(IBAction)thirdTrophySwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    _EPPZ_Cloud_SetBoolForKey(sender.isOn, ThirdTrophyKey);
    _EPPZ_Cloud_Synchronize();
}

-(void)setControlsEnabled:(BOOL) enabled
{
    self.nameTextField.enabled =
    self.soundSwitch.enabled =
    self.volumeSlider.enabled =
    self.levelSegmentedControl.enabled =
    self.firstTrophySwitch.enabled =
    self.secondTrophySwitch.enabled =
    self.thirdTrophySwitch.enabled =
    enabled;
}


@end
