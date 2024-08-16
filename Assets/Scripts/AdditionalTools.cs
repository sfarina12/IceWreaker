using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AdditionalTools
{
    public class at
    {
        public void dl(Object stuffToPrint) { Debug.Log(stuffToPrint); }

        public void udr(Vector3 start, Vector3 dir) { Debug.DrawRay(start, dir, Color.red); }
        public void udr(Vector3 start, Vector3 dir,Color color) { Debug.DrawRay(start, dir, color); }

        public void udl(Vector3 start, Vector3 end) { Debug.DrawLine(start, end, Color.red); }
        public void udl(Vector3 start, Vector3 end,Color color) { Debug.DrawLine(start, end, color); }
    }
}