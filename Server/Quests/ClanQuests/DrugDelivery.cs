﻿using System;
using gta_mp_server.Clan.Interfaces;
using gta_mp_server.Constant;
using gta_mp_server.Global;
using gta_mp_server.Helpers;
using gta_mp_server.Helpers.Interfaces;
using gta_mp_server.Managers.Player.Interfaces;
using gta_mp_server.Quests.Interfaces;
using gta_mp_server.Quests.QuestsData;
using GrandTheftMultiplayer.Server.Elements;
using GrandTheftMultiplayer.Server.Managers;
using GrandTheftMultiplayer.Shared;

namespace gta_mp_server.Quests.ClanQuests {
    /// <summary>
    /// Задание рэкета
    /// </summary>
    internal class DrugDelivery : ClanQuest {
        private const string SHAPE_INDEX = "DrugDeliveryShapeIndex";
        private const string ON_TARGET_POINT = "OnDrugDeliveryPoint";

        private readonly IPointCreator _pointCreator;

        public DrugDelivery() {}
        public DrugDelivery(IPlayerInfoManager playerInfoManager, IClanManager clanManager, IPointCreator pointCreator)
            : base(playerInfoManager, clanManager) {
            _pointCreator = pointCreator;
        }

        /// <summary>
        /// Инициализирует квест
        /// </summary>
        public override void Initialize() {
            ClientEventHandler.Add(ClientEvent.FINISH_DRUG_DELIVERY_POINT, OnFinishDelivery);
            foreach (var npc in DrugDealers.Npcs) {
                _pointCreator.CreatePed(npc.Hash, npc.Position, npc.Rotation);
                var shape = API.createCylinderColShape(npc.ShapePosition, 1.5f, 2f);
                shape.setData(SHAPE_INDEX, npc.Index);
                shape.onEntityEnterColShape += (colShape, entity) => 
                    ProcessShapeEvent(shape, entity, player => player.setSyncedData(ON_TARGET_POINT, true));
                shape.onEntityExitColShape += (colShape, entity) => 
                    ProcessShapeEvent(shape, entity, player => player.resetSyncedData(ON_TARGET_POINT));
            }
        }

        /// <summary>
        /// Обрабатывает события точки рэкета
        /// </summary>
        private void ProcessShapeEvent(ColShape shape, NetHandle playerHandle, Action<Client> action) {
            var player = API.getPlayerFromHandle(playerHandle);
            if (!player.hasData(SHAPE_INDEX) || (int) shape.getData(SHAPE_INDEX) != (int) player.getData(SHAPE_INDEX)) return;
            action(player);
        }

        /// <summary>
        /// Направляет на место задания
        /// </summary>
        public override void ShowTarget(Client player) {
            var questNpc = DrugDealers.Npcs[ActionHelper.Random.Next(DrugDealers.Npcs.Count)];
            player.setData(PlayerData.CLAN_QUEST, true);
            player.setData(SHAPE_INDEX, questNpc.Index);
            API.triggerClientEvent(player, ServerEvent.SHOW_CLAN_QUEST_TARGET, questNpc.ShapePosition, true);
            API.triggerClientEvent(player, ServerEvent.SHOW_SUBTITLE, "Отвезите ~y~накротики ~w~дилеру");
        }

        /// <summary>
        /// Обработчик завершения рэкета
        /// </summary>
        private void OnFinishDelivery(Client player, object[] args) {
            PlayActionAnimation(player);
            player.resetSyncedData(ON_TARGET_POINT);
            player.resetData(SHAPE_INDEX);
            SetQuestReward(player);
            API.triggerClientEvent(player, ServerEvent.HIDE_CLAN_QUEST_POINTS);
        }
    }
}