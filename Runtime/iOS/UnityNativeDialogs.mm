
#import <Foundation/Foundation.h>
#import "UnityNativeDialogs.h"
#import "UnityInterface.h"

extern void UnitySendMessage(const char *, const char *, const char *);

extern "C"{
    
    int UnityDisplayAlertDialog (const char* title,const char* message,const char* positiveButtonTitle,const char* negativeButtonTitle,const char* neutralButtonTitle){
        return [UnityNativeDialogs displayAlertDialogWith:title message:message positiveButtonTitle:positiveButtonTitle 
            negativeButtonTitle:negativeButtonTitle
            neutralButtonTitle:neutralButtonTitle];
    }
}


@implementation UnityNativeDialogs

+ (int) displayAlertDialogWith: (const char*) title
         message:(const char*) message
         positiveButtonTitle:(const char*) positiveButtonTitle
         negativeButtonTitle:(const char*) negativeButtonTitle
         neutralButtonTitle:(const char*) neutralButtonTitle{
    NSString* nsTitle = [NSString stringWithUTF8String:title];
    NSString* nsMessage = [NSString stringWithUTF8String:message];
    UIAlertController* alert = [UIAlertController alertControllerWithTitle:nsTitle message:nsMessage preferredStyle:UIAlertControllerStyleAlert];
    
    if(positiveButtonTitle){
        NSString* buttonTitle = [NSString stringWithUTF8String:positiveButtonTitle];
        UIAlertAction *actionOther = [UIAlertAction actionWithTitle:buttonTitle style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
            //Default Action
            UnitySendMessage("_UnityNativeDialogs", "OnUnityNativeDialogsAction", "Positive");
        }];
        [alert addAction:actionOther];
    }
    
    if(negativeButtonTitle){
        NSString* buttonTitle = [NSString stringWithUTF8String:negativeButtonTitle];
        UIAlertAction *actionNegative = [UIAlertAction actionWithTitle:buttonTitle style:UIAlertActionStyleDestructive handler:^(UIAlertAction * _Nonnull action) {
                //Cancel Action
            UnitySendMessage("_UnityNativeDialogs", "OnUnityNativeDialogsAction", "Negative");
            }];
        [alert addAction:actionNegative];
    }

    if(neutralButtonTitle){
        NSString* buttonTitle = [NSString stringWithUTF8String:neutralButtonTitle];
        UIAlertAction *actionOther = [UIAlertAction actionWithTitle:buttonTitle style:UIAlertActionStyleDefault handler:^(UIAlertAction * _Nonnull action) {
            //Default Action
            UnitySendMessage("_UnityNativeDialogs", "OnUnityNativeDialogsAction", "Neutral");
        }];
        [alert addAction:actionOther];
    }
    [UnityGetGLViewController() presentViewController:alert animated:TRUE completion:nil];
    return 0;
}
@end

