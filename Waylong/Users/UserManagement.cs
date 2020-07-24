using System;
using System.Collections.Generic;

namespace Waylong.Users {

    public class UserManagement {

        #region Property
        //public Dictionary<ConnectionChannel, ILinkInfo> ConnectionList { get; }    //網路連線資料表

        public List<IUser> UserList { get; }
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public UserManagement() {
            UserList = new List<IUser>();
        }
    }

}
