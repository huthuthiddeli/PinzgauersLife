using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WithDrawMoney : MonoBehaviour, IInteractable
{
    public Canvas bankCanvas;
    
    Player player;
    Button acceptButton;
    TMP_InputField inputField;


    void Awake()
    {
        this.bankCanvas.enabled = false;
        this.acceptButton = this.bankCanvas.GetComponentInChildren<Button>();
        this.inputField = this.bankCanvas.GetComponentInChildren<TMP_InputField>();

        if (this.acceptButton is not null)
        {
            this.acceptButton.onClick.AddListener(HandleClick);
        }


    }


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void Interact(GameObject obj)
    {
        Cursor.lockState = CursorLockMode.None;
        this.player = obj.GetComponent<Player>();

        if(player is not null)
        {
            PrepHUD();
            DisplayHUD();
        }
    }

    private void PrepHUD()
    {
        TMP_Text welcomeText = this.bankCanvas.GetComponentInChildren<TMP_Text>();
        welcomeText.text = $"Welcome {this.player.playerName} your balance is: {this.player.moneyBank}€";
    }

    private void DisplayHUD()
    {
        this.bankCanvas.enabled = true;

        while (Input.GetKeyDown(KeyCode.Escape))
        {
            this.bankCanvas.enabled = false;
            Cursor.lockState = CursorLockMode.Locked;
        }

    }
    
    private void HandleClick()
    {
        if (this.inputField is null)
        {
            Debug.LogError("InputField not found in the bank canvas.");
            return;
        }

        string inputText = inputField.text;
        float value;

        if (!float.TryParse(inputText, out value))
        {
            DisplayError("Invalid input. Please enter a valid number.");
            return;
        }

        // WHEN NEGATIVE, WITHDRAW FROM BANK
        if (float.IsNegative(value))
        {
            if(this.player.moneyBank < Mathf.Abs(value))
            {
                DisplayError("Insufficient funds in bank.");
                return;
            }

            player.moneyBank += value;
            player.money -= value;
        }
        // ELSE ADD TO BANK
        else
        {
            if(this.player.money < value)
            {
                DisplayError("Insufficient funds in wallet.");
                return;
            }

            player.money -= value;
            player.moneyBank += value;
        }

        this.bankCanvas.enabled = false;
        Cursor.lockState = CursorLockMode.Locked;
    }


    private void DisplayError(string error)
    {
        Debug.LogError(error);
    }
}
