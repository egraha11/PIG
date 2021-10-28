using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Pig.Models
{
    public class MySession
    {

        public ISession session;

        public MySession(ISession sess)
        {
            session = sess;
        }


        public Player GetPlayer(string key) => session.getObject<Player>(key);
        public void SetPlayer(Player player, string key) => session.setObject(key, player);

        public void RemovePlayer(string key) => session.Remove(key);

    }
}
