using GraduationProject.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace GraduationProject.Services.Interfaces
{
    public interface ISignalrService
    {
        IEnumerable<string> GetConnectionsByUserId(List<string> usersIds);
        string AddNewConnections(SignalRConnection con);
        int RemoveConnections(SignalRConnection con);


    }
}
