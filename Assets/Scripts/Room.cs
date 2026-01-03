using UnityEngine;

public class Room : MonoBehaviour
{
    [SerializeField] private ResponseZone _responseZone;
    [SerializeField] private SirenAlert _sirenAlert;

    private void OnEnable()
    {
        if (_responseZone != null)
        {
            _responseZone.PlayerEntered += TurnOnSiren;
            _responseZone.PlayerExited += TurnOffSiren;
        }
    }

    private void OnDisable()
    {
        if( _responseZone != null )
        {
        _responseZone.PlayerEntered -= TurnOnSiren;
        _responseZone.PlayerExited -= TurnOffSiren;
        }
    }

    private void TurnOnSiren()
    {
        if(_sirenAlert != null)
        _sirenAlert.IncreaseVolume();
    }

    private void TurnOffSiren()
    {
        if (_sirenAlert != null)
            _sirenAlert.DecreaseVolume();
    }
}
