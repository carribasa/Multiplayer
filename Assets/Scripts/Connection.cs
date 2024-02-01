using Photon.Pun;
using Photon.Realtime;
using UnityEngine;

public class Connection : MonoBehaviourPunCallbacks
{
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings(); // Instanciamos la conexion
        PhotonNetwork.AutomaticallySyncScene = true; // Hacemos que se sincronice al cambiar de escena
    }

    void Update()
    {
        if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount > 1) // Si hay mas de un jugador y es Master Client, pasamos a la escena del juego
        {
            PhotonNetwork.LoadLevel(1);
            Destroy(this);
        }
    }

    // Controlar accion del boton
    public void ButtonConnect()
    {
        RoomOptions options = new RoomOptions() { MaxPlayers = 2 }; // Establecemos opciones de la sala
        PhotonNetwork.JoinOrCreateRoom("room1", options, TypedLobby.Default); // Creamos o nos unimos a la sala
    }

    // Al unirnos imprimimos nombre de la sala y n� de players
    public override void OnJoinedRoom()
    {
        Debug.Log("Conectado a la sala " + PhotonNetwork.CurrentRoom.Name);
        Debug.Log("Hay " + PhotonNetwork.CurrentRoom.PlayerCount + " players");
    }

    // Al conectarnos a Master imprimimos mensaje
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado a Master");
    }
}
