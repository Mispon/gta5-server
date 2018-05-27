using System.Collections.Generic;
using gta_mp_server.Models.Utils;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;
using VehicleInfo = gta_mp_database.Entity.Vehicle;

namespace gta_mp_server.Managers.Vehicles.Interfaces {
    internal interface IVehicleManager {
        /// <summary>
        /// ������� ������������ ���������
        /// </summary>
        Vehicle CreateVehicle(CommonVehicle info);

        /// <summary>
        /// ������� ��������� ������
        /// </summary>
        Vehicle CreateVehicle(VehicleInfo vehicleInfo, Vector3 position, Vector3 rotation, int dimension = 0);

        /// <summary>
        /// ����� ��������� �� �������������� ���������
        /// </summary>
        Vehicle GetInstanceById(long vehicleId);

        /// <summary>
        /// ���������� ��������� ���������� �����
        /// </summary>
        void UpdateVehicles();

        /// <summary>
        /// ������������ ��������� ������� ����������
        /// </summary>
        void RestorePosition(Vehicle vehicle);

        /// <summary>
        /// ���������� ��������� ������
        /// </summary>
        Vehicle GetNearestVehicle(Client player, float range, string key = null);

        /// <summary>
        /// ���������� ��������� ���������
        /// </summary>
        List<Vehicle> GetAfkVehicles(int afkMinutes);
    }
}