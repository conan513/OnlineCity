﻿using Model;
using System;
using System.Collections.Generic;
using Transfer;

namespace ServerOnlineCity.Model
{
    [Serializable]
    public class PlayerServer
    {
        public Player Public;

        public string Pass;

        public bool IsAdmin;

        public Guid DiscordToken;

        public Chat PublicChat
        {
            get { return Chats[0]; }
        }

        public List<Chat> Chats;

        public static List<ChatPost> PublicPosts = new List<ChatPost>();

        public DateTime SaveDataPacketTime;

        public byte[] SaveDataPacket
        {
            get
            {
                return Repository.Get.LoadPlayerData(Public.Login);
            }
            set
            {
                Repository.Get.SavePlayerData(Public.Login, value);
            }
        }
        
        public DateTime LastUpdateTime;

        public List<ModelMailTrade> Mails = new List<ModelMailTrade>();

        [NonSerialized]
        public AttackServer AttackData;

        private PlayerServer()
        { }

        public PlayerServer(string login)
        {
            Public = new Player()
            {
                Login = login
            };

            var publicChat = new Chat()
            {
                Id = 1,
                Name = "Public",
                OwnerLogin = login,
                OwnerMaker = false,
                PartyLogin = new List<string>() { login, "system" },
                Posts = PublicPosts
            };
            Chats = new List<Chat>() { publicChat };
        }        
    }
}
