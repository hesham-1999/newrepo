using System.Collections.ObjectModel;
using weatherForm.DTO;

namespace api1.Model
{
    public class xmlmodel
    {
        public Collection<Weateher>  Weatehers  { get; set; } = new Collection<Weateher>();
    }
}
