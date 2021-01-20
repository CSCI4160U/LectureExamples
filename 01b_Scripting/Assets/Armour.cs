using UnityEngine;

[System.Serializable]
public class Armour {
    public string chest;
    public string legs;
    public string head;

    public Armour() {
        this.chest = "none";
        this.legs = "none";
        this.head = "none";
    }

    public Armour(string chest, string legs, string head) {
        this.chest = chest;
        this.legs = legs;
        this.head = head;
    }
}
