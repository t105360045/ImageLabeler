using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageLabeler
{
    class Attribute
    {
        public enum AttributeEnum
        {
            Mountain,
            Cat
        }

        public AttributeEnum attributeEnum;

        public override string ToString()
        {
            if (this.attributeEnum.Equals(AttributeEnum.Cat))
            {
                return "Cat";
            }
            if (this.attributeEnum.Equals(AttributeEnum.Mountain))
            {
                return "Mountain";
            }
            return "";
        }
    }
}
