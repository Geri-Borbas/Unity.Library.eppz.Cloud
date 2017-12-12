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


@interface ViewController ()
@property (nonatomic, strong) NSDictionary *blocksForChangedAlwaysUpdatingKeys;
@property (nonatomic, strong) NSDictionary *blocksForChangedConflictingKeys;
@property (nonatomic, strong) NSMutableDictionary *blocksForEveryChangedKeys;
@end


@implementation ViewController


#pragma mark - iCloud

-(void)initializeCloud
{
    LOG_METHOD;
    
    // Bind blocks.
    [self bindBlocksForEveryChangedKeysWithConflictResolution:YES];
    
    // Initialize plugin.
    [EPPZ_Cloud setDelegate:self];
    EPPZ_Cloud_InitializeWithGameObjectName("eppz! Cloud");
    EPPZ_Cloud_Synchronize();
 
    // Invoke every block (sync UI with local key-value store).
    for (NSString* eachKey in self.blocksForEveryChangedKeys.allKeys)
    {
        void (^eachBlock)(void) = self.blocksForEveryChangedKeys[eachKey];
        eachBlock();
    }

    [self setControlsEnabled:YES];
}

-(void)bindBlocksForEveryChangedKeysWithConflictResolution:(BOOL) useConflictResolution
{
    [self bindBlocksForChangedAlwaysUpdatingKeys];

    if (useConflictResolution)
    { [self bindConflictResolvingBlocksForChangedConflictingKeys]; }
    else
    { [self bindAlwaysUpdatingBlocksForChangedConflictingKeys]; }
    
    self.blocksForEveryChangedKeys = [NSMutableDictionary new];
    [self.blocksForEveryChangedKeys addEntriesFromDictionary: self.blocksForChangedAlwaysUpdatingKeys];
    [self.blocksForEveryChangedKeys addEntriesFromDictionary: self.blocksForChangedConflictingKeys];
}

-(void)bindBlocksForChangedAlwaysUpdatingKeys
{
    LOG_METHOD;
    self.blocksForChangedAlwaysUpdatingKeys = @{
        NameKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", NameKey);
            [self.nameTextField setText:[NSString stringWithUTF8String:EPPZ_Cloud_StringForKey(NameKey.UTF8String)]];
        },
        SoundKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", SoundKey);
            [self.soundSwitch setOn:EPPZ_Cloud_BoolForKey(SoundKey.UTF8String) animated:YES];
        },
        VolumeKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", VolumeKey);
            [self.volumeSlider setValue:EPPZ_Cloud_FloatForKey(VolumeKey.UTF8String) animated:YES];
        }
    };
}

-(void)bindAlwaysUpdatingBlocksForChangedConflictingKeys
{
    LOG_METHOD;
    self.blocksForChangedConflictingKeys = @{
        LevelKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", LevelKey);
            [self.levelSegmentedControl setSelectedSegmentIndex:EPPZ_Cloud_IntForKey(LevelKey.UTF8String)];
        },
        FirstTrophyKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", FirstTrophyKey);
            [self.firstTrophySwitch setOn:EPPZ_Cloud_BoolForKey(FirstTrophyKey.UTF8String) animated:YES];
        },
        SecondTrophyKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", SecondTrophyKey);
            [self.secondTrophySwitch setOn:EPPZ_Cloud_BoolForKey(SecondTrophyKey.UTF8String) animated:YES];
        },
        ThirdTrophyKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", ThirdTrophyKey);
            [self.thirdTrophySwitch setOn:EPPZ_Cloud_BoolForKey(ThirdTrophyKey.UTF8String) animated:YES];
        }
    };
}

-(void)bindConflictResolvingBlocksForChangedConflictingKeys
{
    self.blocksForChangedConflictingKeys = @{
        LevelKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", LevelKey);

            int remoteValue = EPPZ_Cloud_IntForKey(LevelKey.UTF8String);
            int localValue = (int)self.levelSegmentedControl.selectedSegmentIndex;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                int resolvedValue = MAX(remoteValue, localValue); // Resolving strategy
                EPPZ_Cloud_SetIntForKey(resolvedValue, LevelKey.UTF8String);
                EPPZ_Cloud_Synchronize();
            }
            
            [self.levelSegmentedControl setSelectedSegmentIndex:EPPZ_Cloud_IntForKey(LevelKey.UTF8String)];
        },
        FirstTrophyKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", FirstTrophyKey);

            BOOL remoteValue = EPPZ_Cloud_BoolForKey(FirstTrophyKey.UTF8String);
            BOOL localValue = self.firstTrophySwitch.isOn;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                BOOL resolvedValue = (remoteValue || localValue); // Resolving strategy
                EPPZ_Cloud_SetBoolForKey(resolvedValue, FirstTrophyKey.UTF8String);
                EPPZ_Cloud_Synchronize();
            }
            
            [self.firstTrophySwitch setOn:EPPZ_Cloud_BoolForKey(FirstTrophyKey.UTF8String) animated:YES];
        },
        SecondTrophyKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", SecondTrophyKey);
            
            BOOL remoteValue = EPPZ_Cloud_BoolForKey(SecondTrophyKey.UTF8String);
            BOOL localValue = self.secondTrophySwitch.isOn;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                BOOL resolvedValue = (remoteValue || localValue); // Resolving strategy
                EPPZ_Cloud_SetBoolForKey(resolvedValue, SecondTrophyKey.UTF8String);
                EPPZ_Cloud_Synchronize();
            }
            
            [self.secondTrophySwitch setOn:EPPZ_Cloud_BoolForKey(SecondTrophyKey.UTF8String) animated:YES];
        },
        ThirdTrophyKey :
        ^{
            NSLog(@"ViewController.blocksForChangedKeys[\"%@\"]()", ThirdTrophyKey);
            
            BOOL remoteValue = EPPZ_Cloud_BoolForKey(ThirdTrophyKey.UTF8String);
            BOOL localValue = self.thirdTrophySwitch.isOn;
            BOOL isConflict = (remoteValue != localValue);
            if (isConflict)
            {
                BOOL resolvedValue = (remoteValue || localValue); // Resolving strategy
                EPPZ_Cloud_SetBoolForKey(resolvedValue, ThirdTrophyKey.UTF8String);
                EPPZ_Cloud_Synchronize();
            }
            
            [self.thirdTrophySwitch setOn:EPPZ_Cloud_BoolForKey(ThirdTrophyKey.UTF8String) animated:YES];
        }
    };
}

-(void)cloudDidChange:(NSString*) userInfoJSON
{
    LOG_METHOD;
    
    NSError *error;
    NSData *JSONData = [userInfoJSON dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *userInfo = [NSJSONSerialization JSONObjectWithData:JSONData
                                                             options:NSJSONReadingMutableContainers
                                                               error:&error];
    
    NSLog(@"userInfo[NSUbiquitousKeyValueStoreChangedKeysKey]: %@", userInfo[NSUbiquitousKeyValueStoreChangedKeysKey]);
    
    // Invoke appropriate action for each value changed.
    for (NSString* eachChangedKey in userInfo[NSUbiquitousKeyValueStoreChangedKeysKey])
    {
        if ([self.blocksForEveryChangedKeys.allKeys containsObject:eachChangedKey])
        {
            void (^eachBlock)(void) = self.blocksForEveryChangedKeys[eachChangedKey];
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
    EPPZ_Cloud_SetStringForKey(sender.text.UTF8String, NameKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)soundSwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    EPPZ_Cloud_SetBoolForKey(sender.isOn, SoundKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)volumeSliderTouchedUp:(UISlider*) sender
{
    LOG_METHOD;
    EPPZ_Cloud_SetFloatForKey(sender.value, VolumeKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)levelSegmentedControlValueChanged:(UISegmentedControl*) sender
{
    LOG_METHOD;
    EPPZ_Cloud_SetIntForKey((int)sender.selectedSegmentIndex, LevelKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)firstTrophySwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    EPPZ_Cloud_SetBoolForKey(sender.isOn, FirstTrophyKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)secondTrophySwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    EPPZ_Cloud_SetBoolForKey(sender.isOn, SecondTrophyKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)thirdTrophySwitchValueChanged:(UISwitch*) sender
{
    LOG_METHOD;
    EPPZ_Cloud_SetBoolForKey(sender.isOn, ThirdTrophyKey.UTF8String);
    EPPZ_Cloud_Synchronize();
}

-(IBAction)initializeButtonTouchedUp:(UIButton*) sender
{
    LOG_METHOD;
    [self initializeCloud];
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
