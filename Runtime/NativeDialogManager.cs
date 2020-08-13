using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MS.NativeDialog
{
    public class DialogDisplayOptions{
        public string title = "Default Title";
        public string message = "Default Message";

        public UnityAction onPositive;
        public UnityAction onNegative;
        public UnityAction onNeutral;

        public string negativeLabel;
        public string positiveLabel;
        public string neutralLabel;

    }
    public static class NativeDialogManager{

        public static void Display(DialogDisplayOptions options){
            List<string> labels = new List<string>();
            List<UnityAction> actions = new List<UnityAction>();

            var buttonCount = 0;

            if(!string.IsNullOrEmpty(options.positiveLabel)){
                buttonCount ++;
                labels.Add(options.positiveLabel);
                actions.Add(options.onPositive);
            }
            if(!string.IsNullOrEmpty(options.negativeLabel)){
                buttonCount ++;
                labels.Add(options.negativeLabel);
                actions.Add(options.onNegative);
            }

            if(!string.IsNullOrEmpty(options.neutralLabel)){
                buttonCount ++;
                labels.Add(options.neutralLabel);
                actions.Add(options.onNeutral);
            }

            if(labels.Count == 0){
                labels.Add("OK");
                actions.Add(null);
            }
            #if UNITY_EDITOR
                bool res = false;
                if(labels.Count == 1){
                    res = UnityEditor.EditorUtility.DisplayDialog(options.title,options.message,labels[0]);
                    if(res){
                        if(actions[0] != null){
                            actions[0]();
                        }
                    }
                }else if(buttonCount == 2){
                    res = UnityEditor.EditorUtility.DisplayDialog(options.title,options.message,
                    labels[0],labels[1]);
                    if(res){
                        if(actions[0] != null){
                            actions[0]();
                        }
                    }else{
                        if(actions[1] != null){
                            actions[1]();
                        }
                    }
                }else if(buttonCount == 3){
                    var which = UnityEditor.EditorUtility.DisplayDialogComplex(options.title,options.message,
                    labels[0],labels[1],labels[2]);
                    var actionIdx = which;
                    if(actionIdx >= 0 && actionIdx < actions.Count){
                        if(actions[actionIdx] != null){
                            actions[actionIdx]();
                        }
                    }
                }

            #elif UNITY_ANDROID 
                var builder = new Android.AlertDialog.Builder().SetTitle(options.title)
                .SetMessage(options.message);
                if(options.negativeLabel != null){
                    builder.SetNegativeButton(options.negativeLabel,(dialog,which)=>{
                        if(options.onNegative != null){
                            options.onNegative();
                        }
                    });
                }
                if(options.positiveLabel != null){
                    builder.SetPositiveButton(options.positiveLabel,(dialog,which)=>{
                        if(options.onPositive != null){
                            options.onPositive();
                        }
                    });
                }
                if(options.neutralLabel != null){
                    builder.SetNeutralButton(options.neutralLabel,(dialog,which)=>{
                        if(options.onNeutral != null){
                            options.onNeutral();
                        }
                    });
                }
                builder.Show();
            #elif UNITY_IOS
                iOS.AlertDialog.Display(new iOS.AlertDialog.DisplayOptions(){
                    title = options.title,
                    message = options.message,
                    negativeButtonTitle = options.negativeLabel,
                    positiveButtonTitle = options.positiveLabel,
                    neutralButtonTitle = options.neutralLabel,
                    onPositiveClick = options.onPositive,
                    onNegativeClick = options.onNegative,
                    onNeutralClick = options.onNeutral,
                });
            #endif
        }
    }
}

