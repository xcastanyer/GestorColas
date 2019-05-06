using System;
using System.Configuration;

namespace Gestor.Cola
{
    [ConfigurationCollection(typeof(Cola), AddItemName = "cola")]
    public class ColaCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "cola";

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMapAlternate;
            }
        }
        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }

        protected override bool IsElementName(string elementName)
        {
            return elementName.Equals(PropertyName,
              StringComparison.InvariantCultureIgnoreCase);
        }

        public override bool IsReadOnly()
        {
            return false;
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new Cola();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((Cola)(element)).NombreCola;
        }

        public Cola this[int idx]
        {
            get { return (Cola)BaseGet(idx); }
        }
    }
}
