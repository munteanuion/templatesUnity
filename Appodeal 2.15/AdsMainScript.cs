//using GoogleMobileAds.Api;
using UnityEngine;
using AppodealAds.Unity.Api;
using AppodealAds.Unity.Common;
using ConsentManager.ConsentManagerDemo.Scripts;

public class AdsMainScript : MonoBehaviour, IRewardedVideoAdListener
{
    [SerializeField] private bool _isTestingMode = true;
    [SerializeField] private string APP_KEY_APPODEAL_ANDROID;
    private AppodealDemoScriptEdited _consentManager = new AppodealDemoScriptEdited();

    private void Awake()
    {
        _consentManager.appKey = APP_KEY_APPODEAL_ANDROID;
        _consentManager.appPackageName = Application.identifier;
        if (PlayerPrefs.GetString("isGDPR/CCPA") == null)
        {
            _consentManager.RequestConsentInfoUpdate();
            _consentManager.LoadConsentForm();
            _consentManager.ShowFormAsDialog();
        }
    }

    ///////////////////////////////////////////////////////////////////////////////////////
    /*
    // start admob
    private InterstitialAd interstitialAd;
    private string interstitialUnitIdTest = "ca-app-pub-3940256099942544/1033173712";//unit code test ads
    private string interstitialUnitId = "ca-app-pub-9485263456585023/5444368294";//real unit code ads
    
    private void OnEnable()
    {
        if(_isTestAds == true)
            interstitialAd = new InterstitialAd(interstitialUnitIdTest);
        else
            interstitialAd = new InterstitialAd(interstitialUnitId);
        AdRequest adRequest = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(adRequest);
    }
    // finish admob
     
        void Awake()
    {
        //MobileAds.Initialize(InitializationStatus => { });//admob initialize
    }
    */
    ///////////////////////////////////////////////////////////////////////////////////////

    // start appodeal
    private void Start()
    {
        InitializeAppodeal(_isTestingMode);
    }

    private void InitializeAppodeal(bool isTesting)
    {
        Appodeal.setTesting(isTesting);
        //Controlam daca este acces la locatie
        Appodeal.disableLocationPermissionCheck();
        //Punem in mod mut daca telefonul e pe silentios
        Appodeal.muteVideosIfCallsMuted(true);
        Appodeal.initialize(APP_KEY_APPODEAL_ANDROID, Appodeal.INTERSTITIAL | Appodeal.REWARDED_VIDEO, PlayerPrefs.GetString("isGDPR/CCPA") != null );
        //Ascultam interfata reclamelor cu reward video
        Appodeal.setRewardedVideoCallbacks(this);
    }
    // finish appodeal

    ///////////////////////////////////////////////////////////////////////////////////////

    public void ShowInterPageAd()
    {
        if ((Appodeal.isLoaded(Appodeal.INTERSTITIAL) || Appodeal.canShow(Appodeal.INTERSTITIAL)) &&
            !Appodeal.isPrecache(Appodeal.INTERSTITIAL))
        {
            Appodeal.show(Appodeal.INTERSTITIAL);
        }
        else
            Appodeal.cache(Appodeal.INTERSTITIAL);
        /*
        else if (interstitialAd.IsLoaded())
            interstitialAd.Show();*/
    }

    public void ShowRewardedAd()
    {
        if (Appodeal.isLoaded(Appodeal.REWARDED_VIDEO) || Appodeal.canShow(Appodeal.REWARDED_VIDEO) &&
            !Appodeal.isPrecache(Appodeal.REWARDED_VIDEO))
        {
            Appodeal.show(Appodeal.REWARDED_VIDEO);
        }
        else
            Appodeal.cache(Appodeal.REWARDED_VIDEO);
    }

    #region RewardedVideosCallback

    //cea mai folosita -> cand userul a finisat pana la capat reclama
    public void onRewardedVideoFinished(double amount, string name)
    {
        throw new System.NotImplementedException();
    }

    //se executa cand reclama e incarcata
    public void onRewardedVideoLoaded(bool precache)
    {
        throw new System.NotImplementedException();
    }

    //se executa cand reclama nu e incarcata
    public void onRewardedVideoFailedToLoad()
    {
        throw new System.NotImplementedException();
    }

    //se executa cand sunt probleme cu afisarea reclamei
    public void onRewardedVideoShowFailed()
    {
        throw new System.NotImplementedException();
    }

    //se executa cand reclama sa aratat
    public void onRewardedVideoShown()
    {
        throw new System.NotImplementedException();
    }

    //se executa cand reclama a fost inchisa
    public void onRewardedVideoClosed(bool finished)
    {
        throw new System.NotImplementedException();
    }

    //reclama nu se poate arata
    public void onRewardedVideoExpired()
    {
        throw new System.NotImplementedException();
    }

    //se executa cand sa facut click pe reclama
    public void onRewardedVideoClicked()
    {
        throw new System.NotImplementedException();
    }

    #endregion
}
