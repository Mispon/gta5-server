using GrandTheftMultiplayer.Server.Elements;

namespace gta_mp_server.Managers.Work.Police.Interfaces {
    internal interface IPoliceManager {
        /// <summary>
        /// ���������������� ���������� �������� �����������
        /// </summary>
        void Initialize();

        /// <summary>
        /// ���������� ������������� ������
        /// </summary>
        Client GetAttachedPlayer(Client player);

        /// <summary>
        /// ����������� ������ � ������������
        /// </summary>
        void AttachPrisoner(Client policeman, Client prisoner, bool withData = false);

        /// <summary>
        /// ���������� ������ �� ������������
        /// </summary>
        void DetachPrisoner(Client policeman, Client prisoner, bool withData = false);
    }
}