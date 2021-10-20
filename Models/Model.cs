using Models.Interface;
using System.ComponentModel;

namespace Models
{
    public abstract class Model : IModel
    {
        [DisplayName("Id")]
        public long? id { get; set; }
    }
}
