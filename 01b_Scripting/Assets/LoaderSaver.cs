using UnityEngine;
using System.IO;

public class LoaderSaver : MonoBehaviour {
    public static void SaveArmourAsJSON(string savePath, Armour armour) {
        string json = JsonUtility.ToJson(armour);
        File.WriteAllText(savePath, json);
    }

    public static Armour LoadArmourFromJSON(string savePath) {
        if (File.Exists(savePath)) {
            string json = File.ReadAllText(savePath);
            Armour armour = JsonUtility.FromJson<Armour>(json);
            return armour;
        } else {
            Debug.LogError("Unable to load from file: " + savePath);
        }
        return null;
    }
}
