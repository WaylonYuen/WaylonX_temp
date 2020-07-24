using System;
using System.Collections.Generic;
using Waylong.Users;

namespace Waylong.Architecture.Server {

    public interface IServer {
        List<User> Users { get; }

    }

}
