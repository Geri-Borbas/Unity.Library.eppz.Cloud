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

#import <Foundation/Foundation.h>
#import <UIKit/UIKit.h>


extern void UnitySendMessage(const char* gameObjectName, const char* methodName, const char* message);


@protocol EPPZ_Cloud_Delegate
-(void)cloudDidChange:(NSString*) userInfoJSON;
@end


@interface EPPZ_Cloud : NSObject


+(EPPZ_Cloud*)instance;

-(void)initializeWithGameObjectName:(const char*) gameObjectName;
-(BOOL)synchronize;

-(const char*)stringForKey:(const char*) key;
-(void)setString:(const char*) value forKey:(const char*) key;

-(float)floatForKey:(const char*) key;
-(void)setFloat:(float) value forKey:(const char*) key;

-(int)intForKey:(const char*) key;
-(void)setInt:(int) value forKey:(const char*) key;

-(BOOL)boolForKey:(const char*) key;
-(void)setBool:(BOOL) value forKey:(const char*) key;

+(void)setDelegate:(id<EPPZ_Cloud_Delegate>) delegate;


@end
