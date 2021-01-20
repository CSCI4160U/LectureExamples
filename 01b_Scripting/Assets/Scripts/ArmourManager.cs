using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmourManager : MonoBehaviour {
    public Armour armour = null;

    private string savePath;

    private void Awake() {
        savePath = Application.persistentDataPath + "/armour.json";
        Debug.Log(savePath);

        this.armour = new Armour();
    }

    [ContextMenu("Save")]
    public void Save() {
        LoaderSaver.SaveArmourAsJSON(savePath, armour);
    }

    [ContextMenu("Load")]
    public void Load() {
        this.armour = LoaderSaver.LoadArmourFromJSON(savePath);
    }
}
