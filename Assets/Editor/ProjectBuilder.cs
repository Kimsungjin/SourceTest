
using UnityEngine;
using UnityEditor;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

class ProjectBuilder {
	static string[] SCENES = FindEnabledEditorScenes();
	static string APP_NAME = "TitanWarrior";

	[MenuItem ("Jenkins Build/Build Android")]
	static void PerformAndroidBuild ()
	{	
		string strCurrentDir = Path.GetFullPath(".");
		char chSep = Path.DirectorySeparatorChar;
		string strTargetPath = strCurrentDir + chSep +"AndroidTemp";		
		if( Directory.Exists( strTargetPath ) == false )
		{
				Directory.CreateDirectory( strTargetPath );
		}
		GenericBuild(SCENES, strTargetPath, BuildTarget.Android ,BuildOptions.AcceptExternalModificationsToPlayer);
	}
    private static string[] FindEnabledEditorScenes() {
		List<string> EditorScenes = new List<string>();
		foreach(EditorBuildSettingsScene scene in EditorBuildSettings.scenes) {
			if (!scene.enabled) continue;
			EditorScenes.Add(scene.path);
		}
		return EditorScenes.ToArray();
	}
	
	static void GenericBuild(string[] scenes, string target_filename, BuildTarget build_target, BuildOptions build_options)
	{
		EditorUserBuildSettings.SwitchActiveBuildTarget(build_target);
		string res = BuildPipeline.BuildPlayer(scenes, target_filename, build_target, build_options);
		//if (res.Length > 0) {
		//    throw new Exception("BuildPlayer failure: " + res);
		//}
	}
}