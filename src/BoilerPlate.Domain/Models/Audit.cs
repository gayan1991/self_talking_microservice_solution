using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Domain.Models
{
    public class Audit
    {
        public Guid Id { get; set; }
        public string Table { get; set; }
        public string Record { get; set; }

        public Audit() { }

        public Audit(string tableName, string recordedObj)
        {
            Id = Guid.NewGuid();
            Table = tableName;
            Record = recordedObj;
        }
    }
}
