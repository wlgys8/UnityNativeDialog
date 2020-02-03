using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;
using UnityEngine.Events;

#if UNITY_IOS

namespace MS.NativeDialog.iOS
{
    public static class AlertExternMethods{

        [DllImport("__Internal")]
        public static extern int UnityDisplayAlertDialog(string title,string message,
        string positiveButtonTitle,string negativeButtonTitle,string neutralButtonTitle);
    }
    public class AlertDialog{

        private static DisplayOptions _currentOptions;
        public static void Display(DisplayOptions options){
            _currentOptions = options;
            AlertExternMethods.UnityDisplayAlertDialog(options.title,options.message,
            options.positiveButtonTitle,options.negativeButtonTitle,options.neutralButtonTitle);
        }

        public static void ResolveAction(string actionName){
            if(actionName == "Negative"){
                if(_currentOptions != null && _currentOptions.onNegativeClick != null){
                    _currentOptions.onNegativeClick();
                }
            }else if(actionName == "Positive"){
                if(_currentOptions != null && _currentOptions.onPositiveClick != null){
                    _currentOptions.onPositiveClick();
                }
            }else if(actionName == "Neutral"){
                 if(_currentOptions != null && _currentOptions.onNeutralClick != null){
                    _currentOptions.onNeutralClick();
                }               
            }
            _currentOptions = null;
        }

        public class DisplayOptions{
            public string title;
            public string message;
            public string negativeButtonTitle;
            public string positiveButtonTitle;
            public string neutralButtonTitle;

            public UnityAction onNegativeClick;
            public UnityAction onPositiveClick;
            public UnityAction onNeutralClick;

        }

    }
}

#endif

