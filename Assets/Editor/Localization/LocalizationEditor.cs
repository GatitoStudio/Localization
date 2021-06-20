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
		startInfo.FileName = Directory.GetCurrentDirectory()+"\\Localization\\Debug\\LocalizationConverter.exe";
		UnityEngine.Debug.Log(Directory.GetCurrentDirectory() + "\\Localization/Debug\\LocalizationConverter.exe");
		startInfo.WorkingDirectory = Directory.GetCurrentDirectory()+"\\Localization\\Debug"; 
		Process.Start(startInfo);
	}
}
