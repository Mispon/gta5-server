using System.Collections.Generic;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared;
using Vehicle = gta_mp_database.Entity.Vehicle;

namespace gta_mp_server.Managers.Vehicles.Interfaces {
    internal interface IVehicleInfoManager {
        /// <summary>
        /// ���������� ���������� � ���������� ������ �� ��������������
        /// </summary>
        Vehicle GetInfoById(Client player, long vehicleId);

        /// <summary>
        /// ���������� ���������� � ���������� ������ �� ��������
        /// </summary>
        Vehicle GetInfoByHandle(Client player, NetHandle handle);

        /// <summary>
        /// ���������� ���������� � ���������� �� ��������
        /// </summary>
        Vehicle GetInfoByHandle(NetHandle handle);

        /// <summary>
        /// ���������� ��������� ������
        /// </summary>
        List<Vehicle> GetPlayerVehicles(Client player);

        /// <summary>
        /// ���������� ��������� ������
        /// </summary>
        List<Vehicle> GetPlayerVehicles(long accountId);

        /// <summary>
        /// ���������� ���������� � ���������� ������
        /// </summary>
        void SetInfo(Client player, Vehicle vehicleInfo);

        /// <summary>
        /// ���������� ���������� � ���������� ������
        /// </summary>
        void SetInfo(Vehicle vehicleInfo);

        /// <summary>
        /// ������� ��������� ������
        /// </summary>
        void RemoveVehicle(Client player, long vehicleId);

        /// <summary>
        /// ������������ ���� ���������
        /// </summary>
        void ParkAllVehicles();
    }
}