using System;
using System.Collections.Generic;
using System.Text;

namespace BoilerPlate.Service.DTO.Response
{
    public class SuccessDTO
    {
        protected string Title { get; private set; }
        protected object Tag { get; private set; }

        public SuccessDTO(string title) : this(title, null)
        { }

        public SuccessDTO(string title, object tag)
        {
            Title = title;
            Tag = tag;
        }
    }
}
