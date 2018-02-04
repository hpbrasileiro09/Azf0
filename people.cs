
namespace FunctionApp1
{

    public class People
    {
        public People(string _name, int _order)
        {
            name = _name;
            order = _order;
        }
        public string name { get; set; }
        public int order { get; set; }
    }
}