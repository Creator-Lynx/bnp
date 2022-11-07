using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class VersionSetter : MonoBehaviour
{

    void Start()
    {
        Text txt = GetComponent<Text>();
        if(txt) txt.text = 'v' + Application.version;

        TextMeshProUGUI txtPro = GetComponent<TextMeshProUGUI>();
        if(txtPro) txtPro.text = 'v' + Application.version;

    }


}
