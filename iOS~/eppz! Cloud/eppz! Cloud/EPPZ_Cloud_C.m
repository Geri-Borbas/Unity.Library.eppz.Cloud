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

#import "EPPZ_Cloud_C.h"
#import "UnityString.m"


extern void _EPPZ_Cloud_InitializeWithGameObjectName(NSString* gameObjectName)
{ [[EPPZ_Cloud instance] initializeWithGameObjectName:UnityStringFromNSString(gameObjectName)]; }

extern bool _EPPZ_Cloud_Synchronize(void)
{ return [[EPPZ_Cloud instance] synchronize]; }

extern NSString* _EPPZ_Cloud_StringForKey(NSString* key)
{ return NSStringFromUnityString([[EPPZ_Cloud instance] stringForKey:UnityStringFromNSString(key)]); }

extern void _EPPZ_Cloud_SetStringForKey(NSString* value, NSString* key)
{ [[EPPZ_Cloud instance] setString:UnityStringFromNSString(value) forKey:UnityStringFromNSString(key)]; }

extern float _EPPZ_Cloud_FloatForKey(NSString* key)
{ return [[EPPZ_Cloud instance] floatForKey:UnityStringFromNSString(key)]; }

extern void _EPPZ_Cloud_SetFloatForKey(float value, NSString* key)
{ [[EPPZ_Cloud instance] setFloat:value forKey:UnityStringFromNSString(key)]; }

extern int _EPPZ_Cloud_IntForKey(NSString* key)
{ return [[EPPZ_Cloud instance] intForKey:UnityStringFromNSString(key)]; }

extern void _EPPZ_Cloud_SetIntForKey(int value, NSString* key)
{ [[EPPZ_Cloud instance] setInt:value forKey:UnityStringFromNSString(key)]; }

extern bool _EPPZ_Cloud_BoolForKey(NSString* key)
{ return [[EPPZ_Cloud instance] boolForKey:UnityStringFromNSString(key)]; }

extern void _EPPZ_Cloud_SetBoolForKey(bool value, NSString* key)
{ [[EPPZ_Cloud instance] setBool:value forKey:UnityStringFromNSString(key)]; }



