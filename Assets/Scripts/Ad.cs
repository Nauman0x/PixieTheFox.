using System;
using UnityEngine;
using UnityEngine.UI;
using GoogleMobileAds.Api;

public class Ad : MonoBehaviour
{
    private InterstitialAd interstitialAd;

    // Replace with your ad unit IDs
    private string interstitialAdUnitId = "ca-app-pub-2139226141230278~5270571800";

    // Reference to the Close Ad button (assign in Unity Inspector)
    public Button closeAdButton;


    void Start()
    {
        // Initialize the Google Mobile Ads SDK
        MobileAds.Initialize(initStatus => { });

        // Load and show the interstitial ad at the start
        LoadInterstitialAd();

        // Hide the Close Ad button initially
        if (closeAdButton != null)
        {
            closeAdButton.gameObject.SetActive(false);
            closeAdButton.onClick.AddListener(CloseInterstitialAd);
        }
    }

    /// <summary>
    /// Load and show the interstitial ad.
    /// </summary>
    private void LoadInterstitialAd()
    {
        // Clean up any existing ad before loading a new one
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;
        }

        Debug.Log("Loading the interstitial ad.");
        AdRequest adRequest = new AdRequest();

        InterstitialAd.Load(interstitialAdUnitId, adRequest, (InterstitialAd ad, LoadAdError error) =>
        {
            if (error != null || ad == null)
            {
                // Load the banner ad if interstitial fails to load
                return;
            }

            interstitialAd = ad;

            // Show the interstitial ad when loaded
            ShowInterstitialAd();
        });
    }

    /// <summary>
    /// Show the interstitial ad if it is loaded.
    /// </summary>
    private void ShowInterstitialAd()
    {
        if (interstitialAd != null && interstitialAd.CanShowAd())
        {
            Debug.Log("Showing interstitial ad.");
            interstitialAd.Show();

            // Show the Close Ad button when the interstitial ad is displayed
            if (closeAdButton != null)
            {
                closeAdButton.gameObject.SetActive(true);
            }

            // Subscribe to the ad closed event to load banner ad afterwards
            interstitialAd.OnAdFullScreenContentClosed += HandleOnAdClosed;
        }
        else
        {
            // Load the banner ad if interstitial is not ready
        }
    }

   

    /// <summary>
    /// Handle the event when the interstitial ad is closed.
    /// </summary>
    private void HandleOnAdClosed()
    {
        Debug.Log("Interstitial ad closed.");

        // Load and show the banner ad after the interstitial ad is closed

        // Hide the Close Ad button
        if (closeAdButton != null)
        {
            closeAdButton.gameObject.SetActive(false);
        }

        // Set the interstitial ad to null to prevent reuse
        interstitialAd = null;

        // Ensure the game time scale is reset to 1
        Time.timeScale = 1;

        // Re-enable player controls if they were disabled
        /*if (playerController != null)
        {
            playerController.enabled = true;
        }*/
    }

    /// <summary>
    /// Close the interstitial ad when the Close Ad button is clicked.
    /// </summary>
    private void CloseInterstitialAd()
    {
        Debug.Log("Closing interstitial ad.");

        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
            interstitialAd = null;

            // Hide the Close Ad button
            if (closeAdButton != null)
            {
                closeAdButton.gameObject.SetActive(false);
            }

            // Load the banner ad after closing the interstitial ad

            // Ensure the game time scale is reset to 1
            Time.timeScale = 1;

            // Re-enable player controls if they were disabled
            /* if (playerController != null)
             {
                 playerController.enabled = true;
             }*/
        }
    }

    void OnDestroy()
    {
       
        if (interstitialAd != null)
        {
            interstitialAd.Destroy();
        }
    }
}

