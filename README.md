# UnityNativeDialog
 Display native dialog in unity (support Android&iOS) 

# Import To Unity

add to Packages/manifest.json:

    "com.ms.nativedialog":"https://github.com/wlgys8/UnityNativeDialog.git" 

# How to Use

    using MS.NativeDialog;
    ....

    //display dialog with one button
    NativeDialogManager.Display(new DialogDisplayOptions(){
        title = title,
        message = message,
        positiveLabel = positiveLabel,
        onPositive = onPositive, //optional
    });
    
    //display dialog with two button
    NativeDialogManager.Display(new DialogDisplayOptions(){
        title = title,
        message = message,
        positiveLabel = positiveLabel,
        negativeLabel = negativeLabel,
        onPositive = onPositive,
        onNegative = onNegative,
    });

    //display dialog with three button
    NativeDialogManager.Display(new DialogDisplayOptions(){
        title = title,
        message = message,
        positiveLabel = positiveLabel,
        negativeLabel = negativeLabel,
        neutralLabel = neutralLabel,
        onPositive = onPositive,
        onNegative = onNegative,
        onNeutral = onNeutral,
    });
