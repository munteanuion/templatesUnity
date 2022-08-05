using GoogleMobileAds.Api;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsMainScript : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener, IUnityAdsInitializationListener
{
    [Header("Global Settings")]
    [SerializeField] private bool _isTestMode = true;
    [SerializeField] private WhatTesting _whatTesting;
    private enum WhatTesting { All, UnityAds, Admob};

    #region Admob Settings

    [Header("Admob Settings(with / symbol)")]
    [SerializeField] string interstitialUnitId = "ca-app-pub-9485263456585023/5444368294";

    #endregion

    #region Unity Ads Settings

    [Header("Unity Ads Settings")]
    [SerializeField] private string _androidGameId = "4844142";
    [SerializeField] private string _iOSGameId = "4844143";
    private string _gameId;

    #endregion

    //////////////////////////////////////////////////////////////////////
   
    #region Functions Initialize Unity Ads 

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _isTestMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
    #endregion

    //////////////////////////////////////////////////////////////////////

    #region  Unity Ads (and in awake initialize all ads)

    private string _androidAdUnitId = "Interstitial_Android";
    private string _iOsAdUnitId = "Interstitial_iOS";
    private string _adUnitId;

    void Awake()
    {
        #region  Initialize Ads

        InitializeAds();//unity initialize
        MobileAds.Initialize(InitializationStatus => { });//admob initialize

        #endregion

        // Get the Ad Unit ID for the current platform:
        _adUnitId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOsAdUnitId
            : _androidAdUnitId;
    }

    // Load content to the Ad Unit:
    public void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // Show the loaded content in the Ad Unit:
    public void ShowAdUnity()
    {
        // Note that if the ad content wasn't previously loaded, this method will fail
        Debug.Log("Showing Ad: " + _adUnitId);
        Advertisement.Show(_adUnitId, this);
    }

    // Implement Load Listener and Show Listener interface methods: 
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        // Optionally execute code if the Ad Unit successfully loads content.
    }

    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {adUnitId} - {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to load, such as attempting to try again.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Optionally execute code if the Ad Unit fails to show, such as loading another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        LoadAd();
    }

    #endregion

    //////////////////////////////////////////////////////////////////////

    #region  Admob

    private InterstitialAd _interstitialAd;
    private AdRequest _adRequest;
    private string interstitialUnitIdTest = "ca-app-pub-3940256099942544/1033173712";/*unit code test ads*/
    
    private void OnEnable()
    {
        if(_isTestMode == true)
            _interstitialAd = new InterstitialAd(interstitialUnitIdTest);
        else
            _interstitialAd = new InterstitialAd(interstitialUnitId);
        _adRequest = new AdRequest.Builder().Build();
        _interstitialAd.LoadAd(_adRequest);
    }

    #endregion

    //////////////////////////////////////////////////////////////////////

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.A))
            ShowAd();

    }

    public void ShowAd()
    {
        if (_isTestMode)
        {
            if (_interstitialAd.IsLoaded() && 
                ( _whatTesting.Equals(WhatTesting.Admob) | _whatTesting.Equals(WhatTesting.All))
                )
            {
                _interstitialAd.Show();
                _interstitialAd.LoadAd(_adRequest);
            }
            else if ( _whatTesting.Equals(WhatTesting.UnityAds) | _whatTesting.Equals(WhatTesting.All) )
                ShowAdUnity();
        }
        else
        {
            if (_interstitialAd.IsLoaded())
                _interstitialAd.Show();
            else
                ShowAdUnity();
        }
    }
}
