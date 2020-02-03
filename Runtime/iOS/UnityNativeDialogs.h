
#ifndef UnityNativeDialogs_h
#define UnityNativeDialogs_h

#import <Foundation/Foundation.h>
@interface UnityNativeDialogs : NSObject<UIAlertViewDelegate> {}

+ (int) displayAlertDialogWith: (const char*) title
                       message:(const char*) message
             positiveButtonTitle:(const char*) positiveButtonTitle
              negativeButtonTitle:(const char*) negativeButtonTitle
              neutralButtonTitle:(const char*) neutralButtonTitle;
@end

#endif 



