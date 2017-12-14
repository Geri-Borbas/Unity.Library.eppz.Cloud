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


extern void EPPZ_Cloud_InitializeWithGameObjectName(NSString* gameObjectName);
extern bool EPPZ_Cloud_Synchronize(void);

extern NSString* EPPZ_Cloud_StringForKey(NSString* key);
extern void EPPZ_Cloud_SetStringForKey(NSString* value, NSString* key);

extern float EPPZ_Cloud_FloatForKey(NSString* key);
extern void EPPZ_Cloud_SetFloatForKey(float value, NSString* key);

extern int EPPZ_Cloud_IntForKey(NSString* key);
extern void EPPZ_Cloud_SetIntForKey(int value, NSString* key);

extern bool EPPZ_Cloud_BoolForKey(NSString* key);
extern void EPPZ_Cloud_SetBoolForKey(bool value, NSString* key);

