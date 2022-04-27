using System;

namespace Data
{
    public abstract class DataAbstractAPI
    {
        public static DataAbstractAPI CreateAPI()
        { 
            return new DataAPI(); 
        }
    }
    internal class DataAPI : DataAbstractAPI
    {

    }
}
