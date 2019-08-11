using System;

namespace Navtrack.DataAccess.Model
{
    public class Connection
    {
        public int Id { get; set; }
        public DateTime OpenedAt { get; set; }
        public DateTime? ClosedAt { get; set; }
        public string RemoteEndPoint { get; set; }
    }
}