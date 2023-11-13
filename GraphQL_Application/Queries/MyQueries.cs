using GraphQL_Application.Models;
using System.Collections;

namespace GraphQL_Application.Queries
{
    //[ExtendObjectType("Query")]
    public class MyQueries
    {
        public MyModel GetMyModel() { return new MyModel(); }

        public IEnumerable<MyModel> GetMyModels() 
        { 
            return new List<MyModel>
            {
                new MyModel{MyName="Mahima", TimeStamp= DateTime.UtcNow},
                new MyModel{MyName="Safan", TimeStamp= DateTime.UtcNow},
                new MyModel{MyName="Niranjan", TimeStamp= DateTime.UtcNow}
            };
        }

        public MyModel GetMyModel1(string name)
        {
            return new MyModel
            {
                MyName = name
            };
        }
    }
}
