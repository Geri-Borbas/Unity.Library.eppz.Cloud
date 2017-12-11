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

#import "EPPZ_Cloud_C++.h"


#if __cplusplus
extern "C" {
#endif
    
    
    void EPPZ_Cloud_InitializeWithGameObjectName(const char* gameObjectName)
    { [[EPPZ_Cloud instance] initializeWithGameObjectName:gameObjectName]; }
    
    bool EPPZ_Cloud_Synchronize(void)
    { return [[EPPZ_Cloud instance] synchronize]; }
    
    bool EPPZ_Cloud_BoolForKey(const char* key)
    { return [[EPPZ_Cloud instance] boolForKey:key]; }
    
    void EPPZ_Cloud_SetBoolForKey(bool value, const char* key)
    { [[EPPZ_Cloud instance] setBool:value forKey:key]; }
        
    
#if __cplusplus
}
#endif


