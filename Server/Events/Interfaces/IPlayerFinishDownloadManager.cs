using GrandTheftMultiplayer.Server.Elements;

namespace gta_mp_server.Events.Interfaces {
    public interface IPlayerFinishDownloadManager {
        /// <summary>
        /// ���������� ���������� �������� ������ �������
        /// </summary>
        void OnFinishDownload(Client player);
    }
}