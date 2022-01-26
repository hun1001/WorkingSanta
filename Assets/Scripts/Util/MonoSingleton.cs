using UnityEngine;

public class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T>
{
    private static T instance = null;
    private static object locker = new object();
    public static T Instance{
        get{
            lock (locker)
            {
                if (instance == null)
                {
                    instance = FindObjectOfType(typeof(T)) as T;
                    if (instance == null)
                    {
                        instance = new GameObject(typeof(T).ToString(), typeof(T)).GetComponent<T>();
                    }

                    // DontDestroyOnLoad(instance);
                }
            }
            
            return instance;
        }
    }

    public static bool IsNull()
    {
        return instance == null;
    }

    private void Awake(){
        instance = this as T;
    }
}