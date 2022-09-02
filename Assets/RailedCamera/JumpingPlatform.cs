using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpingPlatform : MonoBehaviour
{
    public float JumpSpeed = 0.5f;
    public Transform[] Points;
    //private Vector2 gizmoPos;    

    private void OnCollisionEnter(Collision other)
    {
        if (other.collider.tag == "Player")
        {
            StartCoroutine(MovePlayer(other.transform));
        }
    }

    private IEnumerator MovePlayer(Transform player)
    {
        var t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * JumpSpeed;

            player.position = Mathf.Pow(1 - t, 3) * Points[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * Points[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * Points[2].position +
                Mathf.Pow(t, 3) * Points[3].position;

            yield return new WaitForEndOfFrame();
        }


        yield break;
    }

    private void OnDrawGizmos()
    {
        for (float t = 0; t <= 1; t += 0.05f)
        {
            var gizmoPos = Mathf.Pow(1 - t, 3) * Points[0].position +
                3 * Mathf.Pow(1 - t, 2) * t * Points[1].position +
                3 * (1 - t) * Mathf.Pow(t, 2) * Points[2].position +
                Mathf.Pow(t, 3) * Points[3].position;

            Gizmos.DrawSphere(gizmoPos, 0.25f);
        }

        Gizmos.DrawLine(Points[0].position, Points[1].position);
        Gizmos.DrawLine(Points[2].position, Points[3].position);
    }
}
