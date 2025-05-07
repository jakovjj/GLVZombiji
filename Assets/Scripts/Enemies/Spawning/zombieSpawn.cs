using UnityEngine;

[CreateAssetMenu(fileName = "zombieSpawn", menuName = "Scriptable Objects/zombieSpawn")]
public class zombieSpawn : ScriptableObject
{
        public int cnt;
        public float downtime;
        public GameObject zombieType;
    
}
