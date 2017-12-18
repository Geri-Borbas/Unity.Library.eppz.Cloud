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

using System.IO;
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;


public class BuildPostProcessor
{


	[PostProcessBuildAttribute(1)]
	public static void OnPostProcessBuild(BuildTarget target, string buildPath)
	{
		if (target == BuildTarget.iOS)
		{
			// Read.
			string projectPath = PBXProject.GetPBXProjectPath(buildPath);
			PBXProject project = new PBXProject();
			project.ReadFromString(File.ReadAllText(projectPath));
			string targetName = PBXProject.GetUnityTargetName();
			string targetGUID = project.TargetGuidByName(targetName);

			AddCapabilities(project, targetGUID, targetName, buildPath);

			// Write.
			File.WriteAllText(projectPath, project.WriteToString());
		}
	}

	static void AddCapabilities(PBXProject project, string targetGUID, string targetName, string buildPath)
	{
		// Copy entitlements in place.
		TextAsset entitlementsAsset = (TextAsset)AssetDatabase.LoadAssetAtPath("Assets/eppz! Cloud.xml", typeof(TextAsset));
		string entitlementsAssetFilePath = AssetDatabase.GetAssetPath(entitlementsAsset);
        var entitlementsAssetFileName = Path.GetFileName(entitlementsAssetFilePath);
        var builtEntitlementsFilePath = Path.Combine(
			buildPath,
			Path.Combine(
				targetName,
				Path.ChangeExtension(entitlementsAssetFileName, "entitlements")
			)
		);
        FileUtil.CopyFileOrDirectory(entitlementsAssetFilePath, builtEntitlementsFilePath);

		// Add (with entitlements location).
		project.AddCapability(targetGUID, PBXCapabilityType.iCloud, builtEntitlementsFilePath);
	}
}