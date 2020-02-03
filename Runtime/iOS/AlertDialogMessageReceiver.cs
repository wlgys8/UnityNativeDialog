using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_IOS

namespace MS.NativeDialog.iOS
{
    public class AlertDialogMessageReceiver : MonoBehaviour{
        // Start is called before the first frame update

        public void OnUnityNativeDialogsAction(string message){
            AlertDialog.ResolveAction(message);
        }

        [RuntimeInitializeOnLoadMethod]
        private static void Initialize(){
            var receiver = new GameObject("_UnityNativeDialogs").AddComponent<AlertDialogMessageReceiver>();
            Object.DontDestroyOnLoad(receiver);
        }
    }
}

#endif


