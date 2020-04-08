using Mirror;

public static class MirrorHelpers
{ 
    public static Player GetClientLocalPlayer(NetworkIdentity objectIdentity)
    {
        Player player = null;
        if (objectIdentity.isClient)
        {
            player = NetworkClient.connection.identity.GetComponent<Player>();
        }
        return player;
    }

}
