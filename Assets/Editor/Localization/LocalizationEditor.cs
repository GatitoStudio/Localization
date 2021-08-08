using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using UnityEngine;

public class LocalizationEditor 
{
	[UnityEditor.MenuItem("Localization/GenerateJSON")]
	public static void GenerateJSON()
	{
		ProcessStartInfo startInfo = new ProcessStartInfo();
		startInfo.FileName = Directory.GetCurrentDirectory()+"\\Localization\\LocalizationConverter.exe";
		UnityEngine.Debug.Log(Directory.GetCurrentDirectory() + "\\Localization\\LocalizationConverter.exe");
		startInfo.WorkingDirectory = Directory.GetCurrentDirectory()+"\\Localization"; 
		Process.Start(startInfo);
	}
}
