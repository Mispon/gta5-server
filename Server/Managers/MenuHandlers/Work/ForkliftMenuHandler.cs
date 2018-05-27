using System;
using System.Collections.Generic;
using gta_mp_data.Enums;
using gta_mp_server.Constant;
using gta_mp_server.Global;
using gta_mp_server.Helpers;
using gta_mp_server.Managers.MenuHandlers.Interfaces;
using gta_mp_server.Managers.Player.Interfaces;
using gta_mp_server.Managers.Work.Forklift;
using gta_mp_server.Managers.Work.Interfaces;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Shared.Math;

namespace gta_mp_server.Managers.MenuHandlers.Work {
    /// <summary>
    /// ���������� ���� ��������� ����������
    /// </summary>
    internal class ForkliftMenuHandler : BaseWorkMenu {
        private const int MIN_LEVEL = 2;

        private readonly IWorkEquipmentManager _workEquipmentManager;

        public ForkliftMenuHandler() {}
        public ForkliftMenuHandler(IPlayerInfoManager playerInfoManager, IWorkInfoManager workInfoManager,
            IWorkEquipmentManager workEquipmentManager) : base(playerInfoManager, workInfoManager) {
            _workEquipmentManager = workEquipmentManager;
        }

        /// <summary>
        /// ���������������� ����
        /// </summary>
        public override void Initialize() {
            ClientEventHandler.Add(ClientEvent.WORK_ON_FORKLIFT, StartWork);
            ClientEventHandler.Add(ClientEvent.FORKLIFT_SALARY, GetSalary);
        }

        /// <summary>
        /// ������ ������ �� ����������
        /// </summary>
        private void StartWork(Client player, object[] args) {
            WorkInfoManager.CreateInfoIfNeed(player, WorkType.Forklift);
            if (!CanWork(player, MIN_LEVEL) || HasActiveWork(player)) {
                return;
            }
            WorkInfoManager.SetActivity(player, WorkType.Forklift, true);
            API.sendNotificationToPlayer(player, "�� ������ ������ ~b~��������� ����������");
            player.setData(WorkData.IS_FORKLIFT, true);
            ShowForkliftPoints(player);
            _workEquipmentManager.SetEquipment(player);
            API.triggerClientEvent(player, ServerEvent.HIDE_FORKLIFT_MENU);
        }

        /// <summary>
        /// �������� ��������� ������� ��������
        /// </summary>
        private void ShowForkliftPoints(Client player) {
            var takePosition = GetRandomPosition(ForkliftDataGetter.TakePositions);
            player.setData(ForkliftManager.FORKLIFT_TAKE_KEY, string.Format(ForkliftManager.FORKLIFT_TAKE_VALUE, takePosition.Item1));
            API.triggerClientEvent(player, ServerEvent.SHOW_TAKE_LOADER_POINT, takePosition.Item2);
            var putPosition = GetRandomPosition(ForkliftDataGetter.PutPositions);
            player.setData(ForkliftManager.FORKLIFT_PUT_KEY, string.Format(ForkliftManager.FORKLIFT_PUT_VALUE, putPosition.Item1));
            API.triggerClientEvent(player, ServerEvent.SHOW_PUT_LOADER_POINT, putPosition.Item2);
        }

        /// <summary>
        /// ���������� ��������� �������
        /// Item1 - ���������� �����
        /// Item2 - ����������
        /// </summary>
        private static Tuple<int, Vector3> GetRandomPosition(IReadOnlyList<Vector3> positions) {
            var index = ActionHelper.Random.Next(positions.Count);
            return new Tuple<int, Vector3>(index, positions[index]);
        }

        /// <summary>
        /// �������� ��������
        /// </summary>
        private void GetSalary(Client player, object[] objects) {
            var activeWork = WorkInfoManager.GetActiveWork(player);
            bool TypeChecker() => activeWork.Type == WorkType.Forklift;
            if (!WorkIsCorrect(player, activeWork, TypeChecker)) {
                return;
            }
            WorkInfoManager.SetActivity(player, activeWork.Type, false);
            API.resetEntityData(player, WorkData.IS_FORKLIFT);
            player.resetData(ForkliftManager.FORKLIFT_TAKE_KEY);
            player.resetData(ForkliftManager.FORKLIFT_PUT_KEY);
            PlayerInfoManager.SetPlayerClothes(player);
            PayOut(player, activeWork);
            API.triggerClientEvent(player, ServerEvent.HIDE_LOADER_POINTS);
            API.triggerClientEvent(player, ServerEvent.HIDE_FORKLIFT_MENU);
        }
    }
}