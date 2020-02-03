using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace  MS.NativeDialog.Android
{
    public class AlertDialog{

        public const string JavaClickListener = "android.content.DialogInterface$OnClickListener";
        public const string JavaDialogInterfaceName = "android.content.DialogInterface";
        public const string JavaClassName = "android.app.AlertDialog";
        private static AndroidJavaObject _activity;
        private static AndroidJavaObject activity{
            get{
                if(_activity == null){
                    using(var clazz  = new AndroidJavaClass("com.unity3d.player.UnityPlayer")){
                        _activity = clazz.GetStatic<AndroidJavaObject>("currentActivity");
                    }
                }
                return _activity;
            }
        }

        private static Dictionary<int,AlertDialog> _dialogs = new Dictionary<int, AlertDialog>();

        private UnityAction<AlertDialog> _onDismiss;
        private AndroidJavaObject _jAlertDialog;
        public AlertDialog(AndroidJavaObject jAlertDialog){
            _jAlertDialog = jAlertDialog;
            jAlertDialog.Call("setOnDismissListener",new OnDismissListener((jDialog)=>{
                OnDismiss();
            }));
        }

        private void OnDismiss(){
            var hash = _jAlertDialog.Call<int>("hashCode");
            _dialogs.Remove(hash);
            if(_onDismiss != null){
                _onDismiss(this);
            }
        }

        public void Dismiss(){
            _jAlertDialog.Call("dismiss");
        }

        public void SetCanceledOnTouchOutside(bool cancel){
            _jAlertDialog.Call("setCanceledOnTouchOutside",cancel);
        }

        public void SetOnDismissListener(UnityAction<AlertDialog> onDismiss){
            _onDismiss = onDismiss;
        }

        public class Builder{

            private AndroidJavaObject _javaBuilder;
            public Builder(){
                _javaBuilder = new AndroidJavaObject(JavaClassName + "$Builder",activity);
            }


            public Builder SetTitle(string title){
                _javaBuilder.Call<AndroidJavaObject>("setTitle",title);
                return this;
            }

            public Builder SetMessage(string message){
                _javaBuilder.Call<AndroidJavaObject>("setMessage",message);
                return this;
            }

            public Builder SetNegativeButton(string label,UnityAction<AlertDialog,int> listener){
                _javaBuilder.Call<AndroidJavaObject>("setNegativeButton",label,new OnClickListener((AndroidJavaObject rawDialog, int which)=>{
                    var hash = rawDialog.Call<int>("hashCode");
                    var dialog = _dialogs[hash];
                    listener(dialog,which);
                }));
                return this;
            }

            public Builder SetPositiveButton(string label,UnityAction<AlertDialog,int> listener){
                _javaBuilder.Call<AndroidJavaObject>("setPositiveButton",label,new OnClickListener((AndroidJavaObject rawDialog, int which)=>{
                    var hash = rawDialog.Call<int>("hashCode");
                    var dialog = _dialogs[hash];
                    listener(dialog,which);
                }));
                return this;
            }

            public Builder SetNeutralButton(string label,UnityAction<AlertDialog,int> listener){
                  _javaBuilder.Call<AndroidJavaObject>("setNeutralButton",label,new OnClickListener((AndroidJavaObject rawDialog, int which)=>{
                    var hash = rawDialog.Call<int>("hashCode");
                    var dialog = _dialogs[hash];
                    listener(dialog,which);
                }));     
                return this;         
            }

            public AlertDialog Show(){
                var jAlertDialog = _javaBuilder.Call<AndroidJavaObject>("show");
                var dialog = new AlertDialog(jAlertDialog);
                int hash = jAlertDialog.Call<int>("hashCode");
                _dialogs.Add(hash,dialog);
                // Debug.LogFormat("AlertDialog Show ,hash = {0}",hash);
                return dialog;
            }
        }

        public class OnClickListener:AndroidJavaProxy{

            private UnityAction<AndroidJavaObject, int> _action;

            public OnClickListener(UnityAction<AndroidJavaObject, int> action):base(JavaClickListener){
                _action = action;
            }

            public void onClick(AndroidJavaObject dialog,int which){
                _action(dialog,which);
            }
        }

        public class OnDismissListener:AndroidJavaProxy{

            private UnityAction<AndroidJavaObject> _onDismiss;
            public OnDismissListener(UnityAction<AndroidJavaObject> action):base(JavaDialogInterfaceName + "$OnDismissListener"){
                _onDismiss = action;
            }

            public void onDismiss(AndroidJavaObject dialog){
                if(_onDismiss != null){
                    _onDismiss(dialog);
                }
            }
        }
    }

}

