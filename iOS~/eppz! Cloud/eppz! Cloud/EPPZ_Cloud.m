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

#import "EPPZ_Cloud.h"
#import "UnityString.m"


EPPZ_Cloud* _instance = nil;
const char* EPPZ_Cloud_CloudDidChange = "_CloudDidChange"; // Pass `userInfo` dictionary as JSON string.


@interface EPPZ_Cloud ()
@property (nonatomic, strong) NSString *gameObjectName;
@property (nonatomic, weak) id<EPPZ_Cloud_Delegate> delegate;
@property (nonatomic, weak) NSUbiquitousKeyValueStore *keyValueStore;
@end


@implementation EPPZ_Cloud


#pragma mark - Singleton

+(EPPZ_Cloud*)instance
{
    if (_instance == nil) { _instance = [self new]; }
    return _instance;
}


#pragma mark - Features

+(void)setDelegate:(id<EPPZ_Cloud_Delegate>) delegate;
{ [self instance].delegate = delegate; }

-(void)initializeWithGameObjectName:(const char*) gameObjectName;
{
    self.gameObjectName = NSStringFromUnityString(gameObjectName);
    // NSLog(@"[EPPZ_Cloud initializeWithGameObjectName:`%@`]", self.gameObjectName);
    
    // Cloud.
    self.keyValueStore = [NSUbiquitousKeyValueStore defaultStore];
    [[NSNotificationCenter defaultCenter] addObserver:self
                                             selector:@selector(cloudDidChange:)
                                                 name:NSUbiquitousKeyValueStoreDidChangeExternallyNotification
                                               object:self.keyValueStore];
}

-(BOOL)synchronize
{
    // NSLog(@"[EPPZ_Cloud synchronize]");
    BOOL synchronized = [self.keyValueStore synchronize];
    // NSLog(@"synchronized:%@", (synchronized) ? @"YES" : @"NO");
    return synchronized;
}

-(void)cloudDidChange:(NSNotification*) notification
{
    // NSLog(@"[EPPZ_Cloud cloudDidChange:]");
    
    NSError *error;
    NSData *userInfoJSONData = [NSJSONSerialization dataWithJSONObject:notification.userInfo
                                                               options:0
                                                                 error:&error];
    NSString *userInfoJSON = [[NSString alloc] initWithData:userInfoJSONData encoding:NSUTF8StringEncoding];
    
    // To Unity.
    UnitySendMessage(UnityStringFromNSString(self.gameObjectName), EPPZ_Cloud_CloudDidChange, UnityStringFromNSString(userInfoJSON));
    
    /*
    NSLog(@"[EPPZ_Cloud UnitySendMessage(%@, %@, %@)]",
          self.gameObjectName,
          NSStringFromUnityString(EPPZ_Cloud_CloudDidChange),
          userInfoJSON);
    */
    
    // To Sandbox app if any.
    if (self.delegate != nil) [self.delegate cloudDidChange:userInfoJSON];
}


#pragma mark - Key-value store

-(const char*)stringForKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud stringForKey:`%@`]", NSStringFromUnityString(key));
    return UnityStringFromNSString([self.keyValueStore stringForKey:NSStringFromUnityString(key)]);
}

-(void)setString:(const char*) value forKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud setString:%@ forKey:`%@`]", NSStringFromUnityString(value), NSStringFromUnityString(key));
    [self.keyValueStore setString:NSStringFromUnityString(value) forKey:NSStringFromUnityString(key)];
}

-(float)floatForKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud floatForKey:`%@`]", NSStringFromUnityString(key));
    return (float)[self.keyValueStore doubleForKey:NSStringFromUnityString(key)];
}

-(void)setFloat:(float) value forKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud setFloat:%f forKey:`%@`]", value, NSStringFromUnityString(key));
    [self.keyValueStore setDouble:(double)value forKey:NSStringFromUnityString(key)];
}

-(int)intForKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud intForKey:`%@`]", NSStringFromUnityString(key));
    return (int)[self.keyValueStore longLongForKey:NSStringFromUnityString(key)];
}

-(void)setInt:(int) value forKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud setInt:%d forKey:`%@`]", value, NSStringFromUnityString(key));
    [self.keyValueStore setLongLong:(long long)value forKey:NSStringFromUnityString(key)];
}

-(BOOL)boolForKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud boolForKey:`%@`]", NSStringFromUnityString(key));
    return [self.keyValueStore boolForKey:NSStringFromUnityString(key)];
}

-(void)setBool:(BOOL) value forKey:(const char*) key
{
    // NSLog(@"[EPPZ_Cloud setBool:%@ forKey:`%@`]", (value) ? @"YES" : @"NO", NSStringFromUnityString(key));
    [self.keyValueStore setBool:value forKey:NSStringFromUnityString(key)];
}


@end
