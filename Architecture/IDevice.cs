
struct DeviceCommand
{
};

// Ids will be built and attributed by the MasterDevice
using DeviceId = uint;


interface IDevice
{
    DeviceId getDeviceId();
    void setDeviceId(DeviceId newId);
    void processCommand(DeviceCommand command);
};

interface IMasterDevice
{
    // Top level management of the registered devices list
    list<IDevice*> detectNearbyDevices();
    map<DeviceId, IDevice*> * getRegisteredDevices();

    // Send a command to every registered device
    void broadcastCommand(DeviceCommand command);

    // Send a command to a specific device
    void sendCommand(DeviceCommand command, DeviceId id);
};


interface ITeamDevice :  IDevice
{
};


interface IPlayerDevice :  IDevice
{
};