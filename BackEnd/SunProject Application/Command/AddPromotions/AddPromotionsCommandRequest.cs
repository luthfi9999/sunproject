using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SunProject_Application.Command.AddPromotions
{
    public class AddPromotionsCommandRequest : IRequest<AddPromotionsCommandResponse>
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public double Value { get; set; }
        public IFormFile ItemList { get; set; }
        public List<string> Stores { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
