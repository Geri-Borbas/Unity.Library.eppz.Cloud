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


extern "C"
{
    
    
    void EPPZ_Cloud_InitializeWithGameObjectName(const char* gameObjectName);
    bool EPPZ_Cloud_Synchronize(void);
    
    const char* EPPZ_Cloud_StringForKey(const char* key);
    void EPPZ_Cloud_SetStringForKey(const char* value, const char* key);
    
    float EPPZ_Cloud_FloatForKey(const char* key);
    void EPPZ_Cloud_SetFloatForKey(float value, const char* key);
    
    int EPPZ_Cloud_IntForKey(const char* key);
    void EPPZ_Cloud_SetIntForKey(int value, const char* key);
    
    bool EPPZ_Cloud_BoolForKey(const char* key);
    void EPPZ_Cloud_SetBoolForKey(bool value, const char* key);
    
    
}
