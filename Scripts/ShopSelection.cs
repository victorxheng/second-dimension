using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
//using Sdkbox;

public class ShopSelection : MonoBehaviour {
    public TextMeshProUGUI DescriptionText;

    public List<int[]> priceChart = new List<int[]>();
    public List<int[]> purchaseChart = new List<int[]>();

    string Tier = "Tier";

    public TextMeshProUGUI refillHealthText;

    public GameObject healthRefillGameObject;
    public GameObject purchaseButton;
    public TextMeshProUGUI purchaseText;
    //  public IAP iap;
    
    public AdManager am;
    public GameObject restorePurchases;
    public TextMeshProUGUI cashText;


    private void Awake()
    {
        cashText.text = "$"+PlayerPrefs.GetInt("cashAmount",0);
       /*
        PlayerPrefs.SetInt("Tier1", 0);
        PlayerPrefs.SetInt("Tier2", 0);
        PlayerPrefs.SetInt("Tier3", 0);
        PlayerPrefs.SetInt("Tier4", 0);
        PlayerPrefs.SetInt("Tier5", 0);

        PlayerPrefs.SetInt("maxHealth", 10);
        PlayerPrefs.SetInt("fireRate", 12);
        PlayerPrefs.SetInt("bulletSpeed", 20);
        PlayerPrefs.SetInt("moveSpeed", 12);
        PlayerPrefs.SetInt("cashDrop", 0);*/
        
       //PlayerPrefs.SetInt("cashAmount", 10000);
        int[] price = new int[] { 15,20,25,30,35,40,50,60,80,100,120,150,180,220,260,300,350,400,450,500,560,620,680,750,870,0 }; //i = 0 (refill health), this is redundant
        priceChart.Add(price);
        int[] purchase = new int[] { 1 }; //i = 0 (refill health) (null) // this is redundant
        purchaseChart.Add(purchase);

        price = new int[] { 216 ,326,664,921,1348,1825,2405,2985,3775,4810,5923,7450,8970,10594,12480,13986, 15084, 16124,17236,18434,19636,20802,21015,22306,24060,25920,27825,29880,32009,34560,37800,39980,41902,44560,0}; //i = 1 (max health) formulated with equation, this is redundant
        priceChart.Add(price);
        purchase = new int[] { 11,12,13,14,15,16,17,18,19,20,21,22,23,24,26,28,30,32,34,36,38,40,42,44,46,48,50,52,54,56,58,60,62,64,66,68,70,72,74,76,78,80,83,86,89,92,96,100,0}; //i = 1 (max health)
        purchaseChart.Add(purchase);

        price = new int[] { 250, 350, 500, 750, 1000,1200,1500,1800,2200,2700,3300,4000,4800,5600,7000,9000,12000,0 }; //i = 2 (fire rate) this is redundant
        priceChart.Add(price);
        purchase = new int[] { 13, 14, 15, 16, 17, 18,20,22,24,26,28,30,32,34,36,38,40,43,46,49,52,55,58,62,66,70,74,78,82,86,90,95,100,0 }; //i = 2 (fire rate)
        purchaseChart.Add(purchase);

        price = new int[] { 324, 446, 550, 600, 850,1000,1200,1500,1800,2200, 2600,3000,3500,4000,0 }; //i = 3 (bullet speed)
        priceChart.Add(price);
        purchase = new int[] { 21, 22, 23, 24, 25, 26,27,28,29,30,31,32,33,34,35,36,37,38,39,40,0}; //i = 3 (bullet speed)
        purchaseChart.Add(purchase);

        price = new int[] { 280, 300, 500, 800, 1400, 2400,4000,7000,0 }; //i = 4 (move rate)
        priceChart.Add(price);
        purchase = new int[] { 13, 14, 15, 16, 17, 18,19,20,21,22,23,24,25,26,27,28,29,30,0 }; //i = 4 (move rate)
        purchaseChart.Add(purchase);

        price = new int[] {985, 1990, 4785, 11525, 25200, 48950, 98580, 180680,0 }; //i = 5 (cash)
        priceChart.Add(price);
        purchase = new int[] { 1, 2, 3, 4, 5,6,7,8,0 }; //i = 5 (cash rate)
        purchaseChart.Add(purchase);

        IAPManager.Instance.InitializeIAPManager(InitializeResultCallback);
    }
    private void InitializeResultCallback(IAPOperationStatus status, string message, List<StoreProduct> shopProducts)
    {
        if (status == IAPOperationStatus.Success)
        {           //IAP was successfully initialized //loop through all products     
            for (int i = 0; i < shopProducts.Count; i++)
            {
                if (shopProducts[i].productName == "Remove Ads")
                { //if active variable is true, means that user had bought that product //so enable access   
                    if (shopProducts[i].active)
                    {
                        onPurchase();
                    }
                }
            }
        }
    }

    public void openShop()
    {
        DescriptionText.text = "WELCOME TO THE SHOP!\n\nTHIS IS WHERE CASH IS SPENT \n \nCLICK AROUND TO VIEW AND PURCHASE UPGRADES";
        cashText.text = "$" + PlayerPrefs.GetInt("cashAmount", 0);

        PlayerPrefs.SetInt("currentShopIndex", 0);
        purchaseButton.SetActive(false);
        restorePurchases.SetActive(false);
    }

    public void inAppPurchaseShow()
    {
        if(PlayerPrefs.GetInt("removeAds", 0) == 1)
        {
            purchaseText.text = "POP-UP ADS REMOVED";
            //set text to price 
            DescriptionText.text = "REMOVE ADS\n\nTHANK YOU FOR SUPPORTING THE DEVELOPER!";

        }
        else
        {
            purchaseText.text = "$3.99 (USD)";//set text to price
            DescriptionText.text = "REMOVE ADS\n\nREMOVE POP-UP ADS BY SUPPORTING THE DEVELOPER! HELP MAKE A DIFFERENCE IN SOMEONE'S LIFE FOR A PRICE LESS THAN COFFEE!\n\nREMOVES POP-ADS BUT STILL PROVIDES REWARD-ADS (FOR REVIVES AND CASH)";
        }
        PlayerPrefs.SetInt("currentShopIndex", 6);
        purchaseButton.SetActive(true);

#if UNITY_IOS
        restorePurchases.SetActive(true);
#elif UNITY_EDITOR
            restorePurchases.SetActive(true);
#else            
            restorePurchases.SetActive(false);
#endif
        
    }

    public void refillHealthShow()
    {
        Awake();
        int currentTier = PlayerPrefs.GetInt(Tier + "1", 0);//check for tier
        int currentPrice =currentTier*currentTier*5 + 15; //get price from list using tier
        refillHealthText.text = currentPrice + " CASH";//set text to price
    }
    public Animator healthAnimation;
    public void refillHealthClick()
    {
        int currentTier = PlayerPrefs.GetInt(Tier + "1", 0);//check for tier
        int currentPrice =currentTier*currentTier*5 + 15; //get price from list using tier

        if(PlayerPrefs.GetInt("cashAmount", 0) < currentPrice) //checks for enough cash
        {
            refillHealthText.text = "NOT ENOUGH CASH";
        }
        else
        {
            PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) - currentPrice);//subtract price from cash
            
            PlayerPrefs.SetInt("playerHealth", PlayerPrefs.GetInt("maxHealth", 10));// change to max health
            PlayerPrefs.SetInt("waveHealth", PlayerPrefs.GetInt("playerHealth", 10));
            healthAnimation.SetInteger("HealthPos", Mathf.Abs(0));
        }
    }

    public void buttonClick(int i)
    {
        int currentTier = PlayerPrefs.GetInt(Tier+i, 0);//check for tier
        int[] currentPriceChart = priceChart[i]; //get price list
        int[] currentPurchaseChart = purchaseChart[i]; //get purchase list

        int currentPrice;
        int currentPurchase = currentPurchaseChart[currentTier]; //get purchase from list using tier

        if (i == 1) currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.6);
        else if (i == 2) currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.4);
        else if (i == 3) currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.12);
        else if (i == 4) { currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.5);currentPrice = currentPrice - 200;}
        else { currentPrice = currentPriceChart[currentTier]; currentPrice = currentPrice + 300; }
        currentPrice = currentPrice - 300;
        
        if (currentPrice == 0)PlayerPrefs.SetInt("currentShopIndex", 0);
        else PlayerPrefs.SetInt("currentShopIndex", i);
        purchaseButton.SetActive(true);
        restorePurchases.SetActive(false);

        switch (i)
        {
            case 1:
                if (currentPurchase != 0) {
                    DescriptionText.text = "MAXIMUM HEALTH: \n\nINCREASE THE MAXIMUM AMOUNT OF HEALTH YOU CAN HAVE \n\nCURRENT TIER: " + (currentTier + 1) + "\n\nNEXT TIER COST: $" + currentPrice + "\n\nCURRENT MAX HEALTH: " + PlayerPrefs.GetInt("maxHealth", 10) + " HEALTH";
                    purchaseText.text = currentPrice + " CASH (+" +(currentPurchase-PlayerPrefs.GetInt("maxHealth", 10)) + " MAX HEALTH)";
                }
                else
                {
                    DescriptionText.text = "MAXIMUM HEALTH: \n\nINCREASE THE MAXIMUM AMOUNT OF HEALTH YOU CAN HAVE \n\nCURRENT TIER: " + (currentTier + 1) + " (MAXED OUT)" + "\n\nCURRENT MAX HEALTH: " + PlayerPrefs.GetInt("maxHealth", 10) + " HEALTH";
                   purchaseText.text = "MAX TIER REACHED";
                }
                break;
            case 2:
                if (currentPurchase != 0)
                {
                    DescriptionText.text = "FIRE RATE: \n\nINCREASE THE RATE AT WHICH YOU CAN SHOOT\n\nCURRENT TIER: " + (currentTier + 1) + "\n\nNEXT TIER COST: $" + currentPrice + "\n\nCURRENT RATE: " + PlayerPrefs.GetInt("fireRate", 12);
                    purchaseText.text = currentPrice + " CASH (+" + (currentPurchase - PlayerPrefs.GetInt("fireRate", 12)) + " RATE)";
                }
                else
                {
                    DescriptionText.text = "FIRE RATE: \n\nINCREASE THE RATE AT WHICH YOU CAN SHOOT\n\nCURRENT TIER: " + (currentTier + 1) + " (MAXED OUT)" + "\n\nCURRENT RATE: " + PlayerPrefs.GetInt("fireRate", 12);
                    purchaseText.text = "MAX TIER REACHED";
                }
                break;
            case 3:
                if (currentPurchase != 0)
                {
                    DescriptionText.text = "BULLET TRAVEL SPEED: \n\nINCREASE HOW FAST YOUR BULLETS TRAVELS \n\n(NOT RECOMMENDED FOR EVERYONE)\n\nCURRENT TIER: " + (currentTier + 1) + "\n\nNEXT TIER COST: $" + currentPrice + "\n\nCURRENT SPEED: " + PlayerPrefs.GetInt("bulletSpeed", 20);
                    purchaseText.text = currentPrice + " CASH (+" + (currentPurchase - PlayerPrefs.GetInt("bulletSpeed", 20)) + " SPEED)";
                }
                else
                {
                    DescriptionText.text = "BULLET TRAVEL SPEED: \n\nINCREASE HOW FAST YOUR BULLETS TRAVELS \n\n(NOT RECOMMENDED FOR EVERYONE)\n\nCURRENT TIER: " + (currentTier + 1) +" (MAXED OUT)" + "\n\nCURRENT SPEED: " + PlayerPrefs.GetInt("bulletSpeed", 20);
                    purchaseText.text = "MAX TIER REACHED";
                }
                break;
            case 4:
                if (currentPurchase != 0)
                {
                    DescriptionText.text = "MOVEMENT SPEED: \n\nINCREASE HOW FAST YOU TRAVEL\n\nCURRENT TIER: " + (currentTier + 1) + "\n\nNEXT TIER COST: $" + currentPrice + "\n\nCURRENT SPEED: " + PlayerPrefs.GetInt("moveSpeed", 12);
                    purchaseText.text = currentPrice + " CASH (+" + (currentPurchase - PlayerPrefs.GetInt("moveSpeed", 12)) + " SPEED)";
                }
                else
                {
                    DescriptionText.text = "MOVEMENT SPEED: \n\nINCREASE HOW FAST YOU TRAVEL\n\nCURRENT TIER: " + (currentTier + 1) + " (MAXED OUT)" + "\n\nCURRENT SPEED: " + PlayerPrefs.GetInt("moveSpeed", 12);
                    purchaseText.text = "MAX TIER REACHED";
                }
                break;
            case 5:
                if (currentPurchase != 0)
                {
                    DescriptionText.text = "CASH DROP RATE: \n\nINCREASE HOW HOW MUCH CASH IS DROPPED\n\nCURRENT TIER: " + (currentTier + 1) + "\n\nNEXT TIER COST: $" + currentPrice + "\n\nCURRENT INCREASE: " + PlayerPrefs.GetInt("cashDrop", 0);
                    purchaseText.text = currentPrice + " CASH (+" + (currentPurchase - PlayerPrefs.GetInt("cashDrop", 0)) + " INCREASE)";
                }
                else
                {
                    DescriptionText.text = "CASH DROP RATE: \n\nINCREASE HOW HOW MUCH CASH IS DROPPED\n\nCURRENT TIER: " + (currentTier + 1) + " (MAXED OUT)" + "\n\nCURRENT INCREASE: " + PlayerPrefs.GetInt("cashDrop", 0);
                    purchaseText.text = "MAX TIER REACHED";
                }
                break;
                
        }
    }

    public void purchaseClick()
    {
        int index = PlayerPrefs.GetInt("currentShopIndex", 0);
        if (index != 6)
        {
            if (index != 0)
            {

                int currentTier = PlayerPrefs.GetInt(Tier + index, 0);//check for tier
                int[] currentPriceChart = priceChart[index]; //get price list
                int[] currentPurchaseChart = purchaseChart[index]; //get purchase list


                int currentPrice;
                int currentPurchase = currentPurchaseChart[currentTier]; //get purchase from list using tier

                if (index == 1) currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.6);
                else if (index == 2) currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.4);
                else if (index == 3) currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.12);
                else if (index == 4) { currentPrice = System.Convert.ToInt32(currentPurchase * currentPurchase * currentPurchase * 0.5); currentPrice = currentPrice - 200; }
                else { currentPrice = currentPriceChart[currentTier]; currentPrice = currentPrice + 300; }
                currentPrice = currentPrice - 300;

                if (currentPurchase > 0)
                {
                    if (PlayerPrefs.GetInt("cashAmount", 0) < currentPrice)
                    {
                        purchaseText.text = "NOT ENOUGH CASH";
                    }
                    else
                    {
                        switch (index)
                        {
                            case 1://max health
                                PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) - currentPrice);
                                PlayerPrefs.SetInt("maxHealth", currentPurchase);
                                PlayerPrefs.SetInt("playerHealth", PlayerPrefs.GetInt("maxHealth", 10));
                                PlayerPrefs.SetInt(Tier + index, currentTier + 1);//increase tier
                                PlayerPrefs.SetInt(Tier + (index - 1), currentTier + 1);//increase tier
                                buttonClick(index);
                                break;

                            case 2://fire rate
                                PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) - currentPrice);
                                PlayerPrefs.SetInt("fireRate", currentPurchase);
                                PlayerPrefs.SetInt(Tier + index, currentTier + 1);//increase tier
                                buttonClick(index);
                                break;

                            case 3://bullet speed
                                PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) - currentPrice);
                                PlayerPrefs.SetInt("bulletSpeed", currentPurchase);
                                PlayerPrefs.SetInt(Tier + index, currentTier + 1);//increase tier
                                buttonClick(index);
                                break;

                            case 4://moveSpeed
                                PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) - currentPrice);
                                PlayerPrefs.SetInt("moveSpeed", currentPurchase);
                                PlayerPrefs.SetInt(Tier + index, currentTier + 1);//increase tier
                                buttonClick(index);
                                break;

                            case 5://cashDrop
                                PlayerPrefs.SetInt("cashAmount", PlayerPrefs.GetInt("cashAmount", 0) - currentPrice);
                                PlayerPrefs.SetInt("cashDrop", currentPurchase);
                                PlayerPrefs.SetInt(Tier + index, currentTier + 1);//increase tier
                                buttonClick(index);
                                break;

                        }
                    }
                    cashText.text = "$" + PlayerPrefs.GetInt("cashAmount", 0);
                }
            }
        }
        else
        {

            if (PlayerPrefs.GetInt("removeAds", 0) == 0)
            {
                IAPManager.Instance.BuyProduct(ShopProductNames.RemoveAds, ProductBoughtCallback);
            }
        }
        // automatically called after one product is bought 
        // this is an example of product bought callback 
        
    }


    private void ProductBoughtCallback(IAPOperationStatus status, string message, StoreProduct product)
    {
        if (status == IAPOperationStatus.Success)
        {
            onPurchase();
        }
    }

    public void onPurchase()
    {
        PlayerPrefs.SetInt("removeAds", 1);
    }
    private void restorePurchase()
    {
        PlayerPrefs.SetInt("removeAds", 0);
        inAppPurchaseShow();
    }
    public void RestorePurchases()
    {
        IAPManager.Instance.RestorePurchases(ProductRestoredCallback);
    }
    private void ProductRestoredCallback(IAPOperationStatus status, string message, StoreProduct product)
    {
        if (status == IAPOperationStatus.Success)
        {
                restorePurchase();            
        }
    }


}
