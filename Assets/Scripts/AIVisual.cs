using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
[ExecuteInEditMode]
public class AIVisual : MonoBehaviour
{
    private NavigationScript self;
    void OnDrawGizmosSelected()
    {
        self = this.gameObject.GetComponent<NavigationScript>();

        if (self != null)
        {
            Gizmos.color = new Color(0.5f, 0.05f, 0.75f, 1f);
            Vector3 origin = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            foreach (Transform t in self.patrolpath)
            {
                if (t == null) continue;
                Vector3 end = new Vector3(t.position.x, t.position.y, origin.z);
                Gizmos.DrawLine(origin, t.position);
                origin = t.position;
            }
            return;
        }


    }
}
