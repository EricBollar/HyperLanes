﻿using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public static class SaveSystem
{
    public static void Save() {
    	BinaryFormatter formatter = new BinaryFormatter();
    	string path = Application.persistentDataPath + "/game.data";
    	FileStream stream = new FileStream(path, FileMode.Create);

    	GameData data = new GameData();

    	formatter.Serialize(stream, data);
    	stream.Close();
    }

    public static GameData Load() {
    	string path = Application.persistentDataPath + "/game.data";
    	if (File.Exists(path)) {
    		BinaryFormatter formatter = new BinaryFormatter();
    		FileStream stream = new FileStream(path, FileMode.Open);

    		GameData data = formatter.Deserialize(stream) as GameData;
    		stream.Close();

    		return data;
		} else {
			Debug.LogError("File not found: " + path);
			return null;
		}
    }
}
