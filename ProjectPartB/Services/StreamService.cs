using ProjectPartB.Entities;
using System;
using System.Collections.Generic;
namespace ProjectPartB.Services
{
    class StreamService : Stream
    {
        private readonly string query = "SELECT * FROM C_Streams";
        public List<Stream> SetList() => GetAll<Stream>(query);




    }
}
