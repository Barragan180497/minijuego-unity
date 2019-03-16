using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class LogicalAds : MonoBehaviour
{
    public static LogicalAds instance;

    private InterstitialAd interstitialAd;
    [SerializeField] private string appID = "ca-app-pub-1144018783337205~3887053515";
    [SerializeField] private string interstitialID = "ca-app-pub-1144018783337205/9714095264";

    void Start()
    {
        PedirInterstitial();
    }

    private void Awake()
    {
        MobileAds.Initialize(appID);

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void PedirInterstitial()
    {
        interstitialAd = new InterstitialAd(interstitialID);
        AdRequest pedir = new AdRequest.Builder().Build();
        interstitialAd.LoadAd(pedir);
    }

    public void MostrarInterstitial()
    {
        interstitialAd.Show();
        interstitialAd.Destroy();
        PedirInterstitial();
    }
}