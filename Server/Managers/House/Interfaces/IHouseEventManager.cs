using gta_mp_database.Enums;
using GrandTheftMultiplayer.Shared;

namespace gta_mp_server.Managers.House.Interfaces {
    internal interface IHouseEventManager {
        /// <summary>
        /// ����� ������� �� �����
        /// </summary>
        void OnPlayerWentToEnter(NetHandle entity, long houseId);

        /// <summary>
        /// ����� ������ �� ����� / ������
        /// </summary>
        void OnPlayerAway(NetHandle entity);

        /// <summary>
        /// ���������� ���������
        /// </summary>
        void OnPlayerEnterWardrobe(NetHandle entity, HouseType type);

        /// <summary>
        /// ���������� ���������
        /// </summary>
        void OnPlayerEnterStorage(NetHandle entity, HouseType type);

        /// <summary>
        /// ���������� ������ �� ����
        /// </summary>
        void OnPlayerExit(NetHandle entity, long houseId);

        /// <summary>
        /// ����� ������� �� ����� � �����
        /// </summary>
        void OnPlayerWentToGarageEnter(NetHandle entity, long houseId);

        /// <summary>
        /// ���������� ������ �� ������
        /// </summary>
        void OnPlayerExitGarage(NetHandle entity, long houseId);
    }
}