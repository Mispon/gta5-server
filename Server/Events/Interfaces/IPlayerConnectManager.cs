using GrandTheftMultiplayer.Server.Elements;

namespace gta_mp_server.Events.Interfaces {
    internal interface IPlayerConnectManager {
        /// <summary>
        /// ���������� ������� ������
        /// </summary>
        void OnPlayerConnected(Client player);
    }
}