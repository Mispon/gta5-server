using GrandTheftMultiplayer.Server.Elements;

namespace gta_mp_server.Events.Interfaces {
    internal interface IPlayerRespawnManager {
        /// <summary>
        /// ���������� �������� ������
        /// </summary>
        void OnPlayerRespawn(Client player);
    }
}