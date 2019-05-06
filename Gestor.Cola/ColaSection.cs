
using System.Configuration;

namespace Gestor.Cola
{
    public class ColaSection : ConfigurationSection
    {
        [ConfigurationProperty("colas", IsDefaultCollection = true)]
        public ColaCollection Colas
        {
            get { return (ColaCollection)this["colas"]; }
            set
            {
                this["colas"] = value;
            }
        }
    }
}
