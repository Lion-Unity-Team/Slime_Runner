using UnityEngine;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [SerializeField] private GameObject menuUI;  

    public bool IsMenuOpen { get; private set; }
    public event Action<bool> OnMenuToggle;

    private bool lastState;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    private void Start()
    {
        if (menuUI != null)
        {
            lastState = menuUI.activeSelf;
            IsMenuOpen = lastState;
        }
    }

    private void Update()
    {
        if (menuUI == null) return;

        bool currentState = menuUI.activeSelf;
        if (currentState != lastState)
        {
            lastState = currentState;
            IsMenuOpen = currentState;
            OnMenuToggle?.Invoke(currentState);
        }
    }

    public void SetMenuState(bool active)
    {
        if (menuUI != null)
            menuUI.SetActive(active);

        IsMenuOpen = active;
        OnMenuToggle?.Invoke(active);
    }
}
