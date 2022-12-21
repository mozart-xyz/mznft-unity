# mznft-unity
Mozart NFT Unity SDK client code
C# documentation is here: https://mozart-xyz.github.io/mznft-unity/

# Setup
Clone this repo to a directory on your disk
Install Unity hub from unity.com
**BE SURE TO USE Unity 2019.4.40f1 LTS** so that you do not change any version related things in the project.  If your project is another version its ok, this plugin should work with anything above 2019.x

Open Unity Hub and click Open Project and navigate to the base directory of the repo
The project should load



# Usage
This system was architected to be as easy as possible for drag and drop usage.
Before you can use it you will need to obtain a GameID from your Mozart Dashboard and configure it in the project like this:
 ![Configure Settings](/Assets/MozartSDK/docs/img/step0_configure_mozart_settings.png)
 
The MozartSDK folder contains a Demo scene to view an example of how things work together.
The Components folder contains a list of Mozart Widgets ready to be dragged into Unity GUI.
 ![Components list](/Assets/MozartSDK/docs/img/components.png)

**Everything requires the MozartManager component added to the scene to work**, and the system is designed in such a way that no additional set up is required.
 ![Add Manager](/Assets/MozartSDK/docs/img/step2_add_manager.png)

You simply drag a MozartManager onto the scene, and then drag as many additional components as you want.  They will automatically bind to one another when enabled.  

It is important that you **only create one MozartManager - it is a singleton**, and it will persist across scenes with DoNotDestroy.

# Add Canvas, or use existing canvas
 ![Configure Settings](/Assets/MozartSDK/docs/img/step1_add_canvas.png)
All components are UGUI components and must be placed on a canvas to function.


# Login
**Inventory, Store, and Purchase will not work unless you are logged in**.  Drag a MozartLoginWidget onto your canvas and after you click Login you will be able to freely use these other components.

![Configure Settings](/Assets/MozartSDK/docs/img/step3_add_login_widget.png)

If you want to detect when Login is complete you can use code to monitor the IsLoggedIn boolean on MozartManager, or you can add a hook into the onLoginCompleteEvent delegate on MozartManager.

# Funds Widget
**This widget allows you to show your existing game funds and add more funds using the plus icon.  These components can easily be skinned by changing the prefab**
 ![Configure Settings](/Assets/MozartSDK/docs/img/step4_add_funds_widget.png)
 
 # Locked Item Selector
 **The locked item selector allows you let a user select an item only if they own it.  If they don't own it, it will appear locked and will not be clickable.***
 ![Configure Settings](/Assets/MozartSDK/docs/img/step5_add_locked_item_selector.png)
 To configure this button you can just set the item name field in the inspector to match an item in your item template catalog as shown below
  ![Configure Settings](/Assets/MozartSDK/docs/img/step8_configure_buy_button.png)
 
 # Instant buy button
 **Mozart has a no-code buy button that helps the user one-click purchase an item***
 ![Configure Settings](/Assets/MozartSDK/docs/img/step6_add_instant_buy_button.png)
 This buy button is configured by setting the Item Template ID that is listed on the configuration UI when create the item template
 ![Configure Settings](/Assets/MozartSDK/docs/img/step7_configure_buy_button.png)
 Now when a user clicks the buy button, they will automatically be charged and the item will be sent to their inventory.  If they do not have enough funds, it will take them to the add funds page.

