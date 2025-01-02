using UnityEngine;

public class WinLose : MonoBehaviour
{
    public static WinLose Instance;

    private void Awake()
    {
        if ((Instance != null) && (Instance != this))
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(this.gameObject);
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

}
