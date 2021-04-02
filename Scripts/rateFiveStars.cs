using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rateFiveStars : MonoBehaviour {

#if UNITY_ANDROID
    public void onClick()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.victorcheng.seconddimension&hl=en");
    }
#elif UNITY_IPHONE
    public void onClick()
    {
        Application.OpenURL("https://itunes.apple.com/us/app/second-dimension/id1439695203");
    }
#else    
    public void onClick()
    {
        Application.OpenURL("http://onelink.to/wmbqe5");
    }
#endif
}
