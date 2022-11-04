using System;
using System.Collections;
using Team8.Unemployment.Utility;
using UnityEngine;

namespace Team8.Unemployment.Global
{
    public class CursorManager : SingletonMonoBehaviour<CursorManager>
    {
        public Texture2D cursorDummy;
        public Texture2D cursorFull;
        public float delayClick;

        private void Start()
        {
            Cursor.SetCursor(null,Vector2.zero, CursorMode.Auto);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Cursor.SetCursor(cursorFull,Vector2.zero, CursorMode.Auto);
                StartCoroutine(AfterClick(delayClick));
            }
        }

        IEnumerator AfterClick(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            Cursor.SetCursor(null,Vector2.zero, CursorMode.Auto);
        }
    }
}