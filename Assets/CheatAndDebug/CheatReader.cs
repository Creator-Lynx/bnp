using UnityEngine;
using UnityEngine.Events;
//[ExecuteAlways]
public class CheatReader : MonoBehaviour
{
    [SerializeField]
    Cheat[] cheats;

    void Start()
    {
        for (int i = 0; i < cheats.Length; i++)
        {
            cheats[i].code = String2KeyCodeArray(cheats[i].Name);
        }
    }


    void Update()
    {
        for (int i = 0; i < cheats.Length; i++)
        {
            //if (Input.GetKeyDown(cheats[0].code[cheats[0].progress]))
            //{
            //    cheats[0].progress++;
            //    if (cheats[0].progress >= cheats[0].code.Length)
            //    {
            //        cheats[0].method.Execute();
            //        cheats[0].progress = 0;
            //    }
            //}
            if (Input.GetKeyDown(cheats[i].code[cheats[i].progress]))
            {
                cheats[i].progress++;
                if (cheats[i].progress >= cheats[i].code.Length)
                {
                    cheats[i].Execute();
                    cheats[i].progress = 0;
                }
            }
        }

    }

    /// <summary>
    /// ONLY GET LOWCASE CHAR!!!
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    private KeyCode Char2KeyCode(char c)
    {
        const int KeyCodeA = 97;
        const int CharA = (int)'a';
        int curChar = (int)c - CharA;
        return (KeyCode)(KeyCodeA + curChar);
    }
    private KeyCode[] String2KeyCodeArray(string s)
    {
        KeyCode[] result = new KeyCode[s.Length];
        for (int i = 0; i < s.Length; i++)
        {
            result[i] = Char2KeyCode(s[i]);
        }
        return result;
    }

}

[System.Serializable]
public class Cheat
{
    public int progress = 0;
    public KeyCode[] code;
    public string Name;
    public UnityEvent cheat;
    public void Execute() => cheat.Invoke();
}
