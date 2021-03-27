using RedisService;
using StackExchange.Redis;

namespace SqlService
{
    //public class Repository
    //{
    //    public static bool UpdateValue(string serviceNum, long value)
    //    {
    //        var serviceName = $"Service{serviceNum}";
    //        RedisCache.Instance.Add(serviceName, value.ToString(), When.Always);
    //        return true;



    //        //using (var db = new TestRabbitEntities())
    //        //{
    //        //    var testObj = db.Test.FirstOrDefault();
    //        //    var current = testObj.Value;
    //        //    testObj.Value = testObj.Value + 1;
    //        //    db.Entry(testObj).State = EntityState.Modified;
    //        //    try
    //        //    {
    //        //        db.SaveChanges();
    //        //        //Console.WriteLine("Mevcut Olan => {0} - Update => {1}", current, testObj.Value);
    //        //        return true;
    //        //    }
    //        //    catch (DbUpdateConcurrencyException ex)
    //        //    {
    //        //        //var value = ex.Entries.Single();
    //        //        //value.OriginalValues.SetValues(value.GetDatabaseValues());
    //        //        //db.SaveChanges();
    //        //        //Console.WriteLine("DbUpdateConcurrencyException");
    //        //        return false;
    //        //    }
    //        //}
    //    }
    //}
}