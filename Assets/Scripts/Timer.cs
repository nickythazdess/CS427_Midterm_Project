using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    private TextMeshPro textMesh;
    private float disappearTimer;
    public static Timer Create(int time) {
        Vector3 position = GameObject.Find("Player").transform.position;
        Transform timePopupTransform = Instantiate(GameAssets.i.pfTimePopup, new Vector3(position.x, position.y + 1f, 0), Quaternion.identity);
        Timer timePopup = timePopupTransform.GetComponent<Timer>();
        timePopup.Setup(time);
        return timePopup;
    }

    
    private void Awake() {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int timeAmount) {
        textMesh.SetText(timeAmount.ToString());
        disappearTimer = 1f;
    }

    private void Update() {
        Vector3 position = GameObject.Find("Player").transform.position;
        transform.position = new Vector3(position.x, position.y + 1f, 0);
        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0) {
            Destroy(gameObject);
        }
    }
}
