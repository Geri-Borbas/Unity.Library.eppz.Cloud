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


const char* gameObjectName = "eppz! Cloud";


@interface ViewController ()

@property (nonatomic, strong) NSDictionary *blocksForChangedKeys;
@end


@implementation ViewController


#pragma mark - iCloud

-(void)initializeCloud
{
    NSLog(@"[ViewController initializeCloud]");

    [EPPZ_Cloud setDelegate:self];
    EPPZ_Cloud_InitializeWithGameObjectName(gameObjectName);
    EPPZ_Cloud_Synchronize();
}

-(void)cloudDidChange:(NSString*) userInfoJSON
{
    NSLog(@"[ViewController cloudDidChange:]");
    
    NSError *error;
    NSData *JSONData = [userInfoJSON dataUsingEncoding:NSUTF8StringEncoding];
    NSDictionary *userInfo = [NSJSONSerialization JSONObjectWithData:JSONData
                                                             options:NSJSONReadingMutableContainers
                                                               error:&error];
    
    NSLog(@"userInfo[NSUbiquitousKeyValueStoreChangedKeysKey]: %@", userInfo[NSUbiquitousKeyValueStoreChangedKeysKey]);
    
    // Invoke appropriate action for each value changed.
    for (NSString* eachChangedKey in userInfo[NSUbiquitousKeyValueStoreChangedKeysKey])
    {
        if ([self.blocksForChangedKeys.allKeys containsObject:eachChangedKey])
        {
            void (^eachBlock)(void) = self.blocksForChangedKeys[eachChangedKey];
            eachBlock();
        }
    }
}


#pragma mark - UI

-(IBAction)initializeButtonTouchedUp:(id)sender
{
    // May resolve any conflicts here and sync data again.
    self.blocksForChangedKeys = @{
                                  @"unlock" : ^{
                                      NSLog(@"ViewController.blocksForChangedKeys[@\"unlock\"]()");
                                      [self.unlockSwitch setOn:EPPZ_Cloud_BoolForKey("unlock") animated:YES];
                                  },
                                  @"hint" : ^{
                                      NSLog(@"ViewController.blocksForChangedKeys[@\"hint\"]()");
                                      [self.hintSwitch setOn:EPPZ_Cloud_BoolForKey("hint") animated:YES];
                                  },
                                  @"solve" : ^{
                                      NSLog(@"ViewController.locksForChangedKeys[@\"solve\"]()");
                                      [self.solveSwitch setOn:EPPZ_Cloud_BoolForKey("solve") animated:YES];
                                  }
                                  };
    [self initializeCloud];
}

-(IBAction)unlockSwitchDidChange:(UISwitch*) sender
{
    NSLog(@"[ViewController unlockSwitchDidChange:]");
    
    EPPZ_Cloud_SetBoolForKey(sender.isOn, "unlock");
    EPPZ_Cloud_Synchronize();
}

-(IBAction)hintSwitchDidChange:(UISwitch*) sender
{
    NSLog(@"[ViewController hintSwitchDidChange:]");
    
    EPPZ_Cloud_SetBoolForKey(sender.isOn, "hint");
    EPPZ_Cloud_Synchronize();
}

-(IBAction)solveSwitchDidChange:(UISwitch*) sender
{
    NSLog(@"[ViewController solveSwitchDidChange:]");
    
    EPPZ_Cloud_SetBoolForKey(sender.isOn, "solve");
    EPPZ_Cloud_Synchronize();
}


@end
