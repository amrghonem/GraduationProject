using GraduationProject.Infrastructure;
using GraduationProject.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using GraduationProject.Data;
using System.Linq;

namespace GraduationProject.Services.Implementation
{
    public class SignalrService : ISignalrService
    {
        private IRepository<SignalRConnection> _signalrRepo;

        public SignalrService(IRepository<SignalRConnection> singalrRepo)
        {
            _signalrRepo = singalrRepo;
        }

        public string AddNewConnections(SignalRConnection con)
        {
            return _signalrRepo.Insert(con).ConnectionId;
        }

        public int RemoveConnections(SignalRConnection con)
        {
            var Connection = _signalrRepo.GetAll().SingleOrDefault(c => c.ConnectionId == con.ConnectionId && c.ApplicationUserId == con.ApplicationUserId);
            return _signalrRepo.Delete(Connection);
        }

        public IEnumerable<string> GetConnectionsByUserId(List<string> usersIds)
        {
            return  _signalrRepo.GetAll().Where(s => usersIds.Contains(s.ApplicationUserId))
                .Select(u => u.ConnectionId).ToList();
        }
    }
}
