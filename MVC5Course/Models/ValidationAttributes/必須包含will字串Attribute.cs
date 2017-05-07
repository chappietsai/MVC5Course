using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.ValidationAttributes
{
    public class 必須包含will字串Attribute: DataTypeAttribute
    {
        public 必須包含will字串Attribute():base(DataType.Text)
        {
            //ErrorMessage

        }

        public override bool IsValid(object value)
        {

            var str = (string)value;
            return str.Contains("will");
        }

    }
}