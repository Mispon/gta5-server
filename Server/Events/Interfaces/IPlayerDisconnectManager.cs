using GrandTheftMultiplayer.Server.Elements;

namespace gta_mp_server.Events.Interfaces {
    internal interface IPlayerDisconnectManager {
        /// <summary>
        /// ���������� ������ ������
        /// </summary>
        void OnPlayerDisconnect(Client player, string reason);
    }
}